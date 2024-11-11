namespace Jimmaphy.AdvancedProductDesignExtractor;

partial class Extractor
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Extractor));
        OutputBrowser = new Button();
        InputBrowser = new Button();
        InputDirectory = new TextBox();
        OutputDirectory = new TextBox();
        InfoInput = new Label();
        InfoOutput = new Label();
        DetailToggle = new LinkLabel();
        Starter = new Button();
        Settings = new GroupBox();
        StatusIndicator = new Label();
        Logs = new TextBox();
        FolderBrowser = new FolderBrowserDialog();
        ShowAbout = new LinkLabel();
        Settings.SuspendLayout();
        SuspendLayout();
        // 
        // OutputBrowser
        // 
        OutputBrowser.Location = new Point(329, 53);
        OutputBrowser.Name = "OutputBrowser";
        OutputBrowser.Size = new Size(75, 23);
        OutputBrowser.TabIndex = 3;
        OutputBrowser.Text = "Browse...";
        OutputBrowser.UseVisualStyleBackColor = true;
        OutputBrowser.Click += SelectOutput;
        // 
        // InputBrowser
        // 
        InputBrowser.Location = new Point(329, 22);
        InputBrowser.Name = "InputBrowser";
        InputBrowser.Size = new Size(75, 23);
        InputBrowser.TabIndex = 1;
        InputBrowser.Text = "Browse...";
        InputBrowser.UseVisualStyleBackColor = true;
        InputBrowser.Click += SelectInput;
        // 
        // InputDirectory
        // 
        InputDirectory.Location = new Point(114, 22);
        InputDirectory.Name = "InputDirectory";
        InputDirectory.Size = new Size(209, 23);
        InputDirectory.TabIndex = 0;
        InputDirectory.Leave += ValidateSpecifiedDirectory;
        // 
        // OutputDirectory
        // 
        OutputDirectory.Location = new Point(114, 53);
        OutputDirectory.Name = "OutputDirectory";
        OutputDirectory.Size = new Size(209, 23);
        OutputDirectory.TabIndex = 2;
        OutputDirectory.Leave += ValidateSpecifiedDirectory;
        // 
        // InfoInput
        // 
        InfoInput.AutoSize = true;
        InfoInput.Location = new Point(10, 26);
        InfoInput.Name = "InfoInput";
        InfoInput.Size = new Size(85, 15);
        InfoInput.TabIndex = 0;
        InfoInput.Text = "Input directory";
        // 
        // InfoOutput
        // 
        InfoOutput.AutoSize = true;
        InfoOutput.Location = new Point(10, 56);
        InfoOutput.Name = "InfoOutput";
        InfoOutput.Size = new Size(96, 15);
        InfoOutput.TabIndex = 0;
        InfoOutput.Text = "Output Directory";
        // 
        // DetailToggle
        // 
        DetailToggle.AutoSize = true;
        DetailToggle.Location = new Point(22, 113);
        DetailToggle.Name = "DetailToggle";
        DetailToggle.Size = new Size(72, 15);
        DetailToggle.TabIndex = 4;
        DetailToggle.TabStop = true;
        DetailToggle.Text = "More details";
        DetailToggle.LinkClicked += ToggleDetails;
        // 
        // Starter
        // 
        Starter.Location = new Point(341, 109);
        Starter.Name = "Starter";
        Starter.Size = new Size(75, 23);
        Starter.TabIndex = 5;
        Starter.Text = "Start";
        Starter.UseVisualStyleBackColor = true;
        Starter.Click += RunExtraction;
        // 
        // Settings
        // 
        Settings.Controls.Add(OutputDirectory);
        Settings.Controls.Add(OutputBrowser);
        Settings.Controls.Add(InputBrowser);
        Settings.Controls.Add(InfoOutput);
        Settings.Controls.Add(InputDirectory);
        Settings.Controls.Add(InfoInput);
        Settings.Location = new Point(12, 12);
        Settings.Name = "Settings";
        Settings.Size = new Size(410, 89);
        Settings.TabIndex = 8;
        Settings.TabStop = false;
        Settings.Text = "Settings";
        // 
        // StatusIndicator
        // 
        StatusIndicator.AutoSize = true;
        StatusIndicator.ForeColor = SystemColors.InfoText;
        StatusIndicator.Location = new Point(126, 113);
        StatusIndicator.Name = "StatusIndicator";
        StatusIndicator.Size = new Size(93, 15);
        StatusIndicator.TabIndex = 0;
        StatusIndicator.Text = "Ready for action";
        // 
        // Logs
        // 
        Logs.BorderStyle = BorderStyle.None;
        Logs.ForeColor = SystemColors.ControlDarkDark;
        Logs.Location = new Point(22, 144);
        Logs.Multiline = true;
        Logs.Name = "Logs";
        Logs.ReadOnly = true;
        Logs.Size = new Size(394, 205);
        Logs.TabIndex = 0;
        Logs.TabStop = false;
        Logs.Text = "Ready for action";
        Logs.Visible = false;
        Logs.WordWrap = false;
        // 
        // FolderBrowser
        // 
        FolderBrowser.RootFolder = Environment.SpecialFolder.UserProfile;
        // 
        // ShowAbout
        // 
        ShowAbout.AutoSize = true;
        ShowAbout.Location = new Point(300, 362);
        ShowAbout.Name = "ShowAbout";
        ShowAbout.Size = new Size(116, 15);
        ShowAbout.TabIndex = 9;
        ShowAbout.TabStop = true;
        ShowAbout.Text = "About ADP Extractor";
        ShowAbout.Visible = false;
        ShowAbout.LinkClicked += About;
        // 
        // Extractor
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CausesValidation = false;
        ClientSize = new Size(434, 141);
        Controls.Add(ShowAbout);
        Controls.Add(Logs);
        Controls.Add(StatusIndicator);
        Controls.Add(Settings);
        Controls.Add(Starter);
        Controls.Add(DetailToggle);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "Extractor";
        Text = "APD Extractor";
        Settings.ResumeLayout(false);
        Settings.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button OutputBrowser;
    private Button InputBrowser;
    private TextBox InputDirectory;
    private TextBox OutputDirectory;
    private Label InfoInput;
    private Label InfoOutput;
    private LinkLabel DetailToggle;
    private Button Starter;
    private GroupBox Settings;
    private Label StatusIndicator;
    private TextBox Logs;
    private FolderBrowserDialog FolderBrowser;
    private LinkLabel ShowAbout;
}
