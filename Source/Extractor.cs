using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System.Diagnostics;
using System.IO.Compression;

namespace Jimmaphy.AdvancedProductDesignExtractor;

public partial class Extractor : Form
{
    private static readonly string[] extensions = { "sldprt", "sldasm", "slddrw" };
    private static readonly string[] archives = { "zip", "rar" };
    private string currentInputPath;
    private string currentOutputPath;

    public Extractor()
    {
        InitializeComponent();
        this.currentInputPath = InputDirectory.Text;
        this.currentOutputPath = OutputDirectory.Text;
    }

    private void Log(string message)
    {
        Logs.Text = message + Environment.NewLine + Logs.Text;
    } 

    private void ToggleDetails(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Logs.Visible = !Logs.Visible;
        StatusIndicator.Visible = !StatusIndicator.Visible;
        ShowAbout.Visible = !ShowAbout.Visible;

        if (this.Height == 180)
        {
            this.Height = 430;
            this.DetailToggle.Text = "Hide Details";
        }
        else
        {
            this.Height = 180;
            this.DetailToggle.Text = "Show Details";
        }
    }

    private void SelectInput(object sender, EventArgs e)
    {
        SelectDirectory(InputDirectory);
    }

    private void SelectOutput(object sender, EventArgs e)
    {
        SelectDirectory(OutputDirectory);
    }

    private void SelectDirectory(TextBox targetDirectoryField)
    {
        DialogResult result = FolderBrowser.ShowDialog();
        if (result == DialogResult.OK)
        {
            targetDirectoryField.Text = FolderBrowser.SelectedPath;
        }
    }

    private void ValidateSpecifiedDirectory(object sender, EventArgs e)
    {
        var specifiedDirectory = (sender as TextBox)?.Text;

        if (!string.IsNullOrEmpty(specifiedDirectory) && !Directory.Exists(specifiedDirectory))
        {
            MessageBox.Show(
                text: "The specified directory does not exist.",
                caption: "Invalid Directory",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Error
            );

            InputDirectory.Text = this.currentInputPath;
            OutputDirectory.Text = this.currentOutputPath;
        }
        else
        {
            this.currentInputPath = InputDirectory.Text;
            this.currentOutputPath = OutputDirectory.Text;
        }
    }

    private void About(object sender, LinkLabelLinkClickedEventArgs e)
    {
        new About().ShowDialog();
    }

    private async void RunExtraction(object sender, EventArgs e)
    {
        if (currentInputPath == null || currentOutputPath == null)
        {
            StatusIndicator.Text = "Specify directories";
            Log("Specify directories");
        }
        else
        {
            StatusIndicator.Text = "Processing...";
            Starter.Enabled = !Starter.Enabled;

            (int, int) results = await Task.Run(() => Extract());

            StatusIndicator.Text = $"Finished ({results.Item1} students, {results.Item2} files)";
            Starter.Enabled = !Starter.Enabled;
            Log($"Finished ({results.Item1} students, {results.Item2} files)");
        }
    }

    private (int, int) Extract()
    {
        int numberOfStudents = 0;
        int numberOfFiles = 0;

        foreach (string file in Directory.GetFiles(InputDirectory.Text))
        {

            if (Path.GetExtension(file).Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                int studentFiles = 0;
                string studentId = Path.GetFileName(file).Split('-')[0];
                string studentDirectory = Path.Combine(OutputDirectory.Text, studentId);

                using (ZipArchive archive = ZipFile.OpenRead(file))
                {
                    List<ZipArchiveEntry> entries = archive.Entries.ToList();
                    foreach (ZipArchiveEntry entry in entries)
                    {
                        string fileExtension = entry.FullName.ToLower().Split('.').Last();

                        if (extensions.Contains(fileExtension) || archives.Contains(fileExtension))
                        {
                            string outputFilePath = Path.Combine(studentDirectory, entry.Name);
                            string outputDirectory = Path.GetDirectoryName(outputFilePath);

                            if (!Directory.Exists(outputDirectory))
                            {
                                Directory.CreateDirectory(outputDirectory);
                            }

                            entry.ExtractToFile(outputFilePath, true);

                            if (extensions.Contains(fileExtension))
                            {
                                studentFiles++;
                            }
                        }
                        
                        if (archives.Contains(fileExtension))
                        {
                            string nestedZipPath = Path.Combine(studentDirectory, entry.Name);

                            using (var nestedStream = entry.Open())
                            using (var nestedFileStream = new FileStream(nestedZipPath, FileMode.Create))
                            {
                                nestedStream.CopyTo(nestedFileStream);
                            }

                            if (fileExtension == "zip")
                            {
                                studentFiles += ExtractNestedZip(nestedZipPath, studentDirectory);
                            }
                            else
                            {
                                studentFiles += ExtractNestedRar(nestedZipPath, studentDirectory);
                            }
                        }
                    }
                }

                Log($"Processed {studentFiles} files for {studentId}");
                numberOfFiles += studentFiles;
                numberOfStudents++;
            }
        }

        return (numberOfStudents, numberOfFiles);
    }

    private int ExtractNestedZip(string zipPath, string outputDirectory)
    {
        int studentFiles = 0;
        using (ZipArchive nestedArchive = ZipFile.OpenRead(zipPath))
        {
            foreach (ZipArchiveEntry nestedEntry in nestedArchive.Entries)
            {
                string nestedFileExtension = nestedEntry.FullName.ToLower().Split('.').Last();

                if (extensions.Contains(nestedFileExtension))
                {
                    string nestedOutputFilePath = Path.Combine(outputDirectory, nestedEntry.Name);
                    string nestedOutputDir = Path.GetDirectoryName(nestedOutputFilePath);

                    if (!Directory.Exists(nestedOutputDir))
                    {
                        Directory.CreateDirectory(nestedOutputDir);
                    }

                    nestedEntry.ExtractToFile(nestedOutputFilePath, true);
                    studentFiles++;
                }
                else if (nestedFileExtension == "zip")
                {
                    string deeperNestedZipPath = Path.Combine(outputDirectory, nestedEntry.FullName);

                    using (var deeperNestedStream = nestedEntry.Open())
                    using (var deeperNestedFileStream = new FileStream(deeperNestedZipPath, FileMode.Create))
                    {
                        deeperNestedStream.CopyTo(deeperNestedFileStream);
                    }

                    ExtractNestedZip(deeperNestedZipPath, outputDirectory);
                }
            }
        }

        File.Delete(zipPath);
        return studentFiles;
    }

    private int ExtractNestedRar(string rarPath, string outputDirectory)
    {
        int studentFiles = 0;
        using (RarArchive nestedArchive = RarArchive.Open(rarPath))
        {
            foreach (var nestedEntry in nestedArchive.Entries.Where(entry => !entry.IsDirectory))
            {
                string nestedFileExtension = nestedEntry.Key.ToLower().Split('.').Last();

                if (extensions.Contains(nestedFileExtension))
                {
                    string nestedOutputFilePath = Path.Combine(outputDirectory, Path.GetFileName(nestedEntry.Key));
                    string nestedOutputDir = Path.GetDirectoryName(nestedOutputFilePath);

                    if (!Directory.Exists(nestedOutputDir))
                    {
                        Directory.CreateDirectory(nestedOutputDir);
                    }

                    nestedEntry.WriteToFile(nestedOutputFilePath, new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
                    studentFiles++;
                }
                else if (nestedFileExtension == "rar")
                {
                    string deeperNestedRarPath = Path.Combine(outputDirectory, nestedEntry.Key);

                    using (var deeperNestedStream = nestedEntry.OpenEntryStream())
                    using (var deeperNestedFileStream = new FileStream(deeperNestedRarPath, FileMode.Create))
                    {
                        deeperNestedStream.CopyTo(deeperNestedFileStream);
                    }

                    ExtractNestedRar(deeperNestedRarPath, outputDirectory);
                }
            }
        }

        File.Delete(rarPath);
        return studentFiles;
    }
}


