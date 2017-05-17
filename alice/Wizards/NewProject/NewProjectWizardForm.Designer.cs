namespace alice
{
  partial class NewProjectWizardForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.cancelBtn = new System.Windows.Forms.Button();
      this.okBtn = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.uiVisualStudioBinPath = new System.Windows.Forms.ComboBox();
      this.uiVisualStudioVersion = new System.Windows.Forms.ComboBox();
      this.label16 = new System.Windows.Forms.Label();
      this.uiManagerOnly = new System.Windows.Forms.CheckBox();
      this.label15 = new System.Windows.Forms.Label();
      this.sourceFolderTxt = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.worldSpecLbl = new System.Windows.Forms.Label();
      this.worldSpecBrowse = new System.Windows.Forms.Button();
      this.label14 = new System.Windows.Forms.Label();
      this.dfltBenchPlatformCbo = new System.Windows.Forms.ComboBox();
      this.dfltSimPlatformCbo = new System.Windows.Forms.ComboBox();
      this.dfltManagerPlatformCbo = new System.Windows.Forms.ComboBox();
      this.specLbl = new System.Windows.Forms.Label();
      this.specBrowse = new System.Windows.Forms.Button();
      this.label13 = new System.Windows.Forms.Label();
      this.worldFolderLbl = new System.Windows.Forms.Label();
      this.worldFolderBrowse = new System.Windows.Forms.Button();
      this.label12 = new System.Windows.Forms.Label();
      this.modelFolderLbl = new System.Windows.Forms.Label();
      this.modelFolderBrowseBtn = new System.Windows.Forms.Button();
      this.label10 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.dfltBenchBuildCbo = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.dfltSimBuildCbo = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.dfltManagerBuildCbo = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.initDataRemove = new System.Windows.Forms.Button();
      this.initDataAdd = new System.Windows.Forms.Button();
      this.initDataLst = new System.Windows.Forms.ListBox();
      this.label4 = new System.Windows.Forms.Label();
      this.projectNameTxt = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.masterConfigLbl = new System.Windows.Forms.Label();
      this.masterConfigBrowse = new System.Windows.Forms.Button();
      this.solutionCbo = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // cancelBtn
      // 
      this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point(514, 542);
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size(63, 24);
      this.cancelBtn.TabIndex = 6;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      // 
      // okBtn
      // 
      this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okBtn.Location = new System.Drawing.Point(401, 542);
      this.okBtn.Name = "okBtn";
      this.okBtn.Size = new System.Drawing.Size(107, 24);
      this.okBtn.TabIndex = 5;
      this.okBtn.Text = "Create Project";
      this.okBtn.UseVisualStyleBackColor = true;
      this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.uiVisualStudioBinPath);
      this.groupBox1.Controls.Add(this.uiVisualStudioVersion);
      this.groupBox1.Controls.Add(this.label16);
      this.groupBox1.Controls.Add(this.uiManagerOnly);
      this.groupBox1.Controls.Add(this.label15);
      this.groupBox1.Controls.Add(this.sourceFolderTxt);
      this.groupBox1.Controls.Add(this.label11);
      this.groupBox1.Controls.Add(this.worldSpecLbl);
      this.groupBox1.Controls.Add(this.worldSpecBrowse);
      this.groupBox1.Controls.Add(this.label14);
      this.groupBox1.Controls.Add(this.dfltBenchPlatformCbo);
      this.groupBox1.Controls.Add(this.dfltSimPlatformCbo);
      this.groupBox1.Controls.Add(this.dfltManagerPlatformCbo);
      this.groupBox1.Controls.Add(this.specLbl);
      this.groupBox1.Controls.Add(this.specBrowse);
      this.groupBox1.Controls.Add(this.label13);
      this.groupBox1.Controls.Add(this.worldFolderLbl);
      this.groupBox1.Controls.Add(this.worldFolderBrowse);
      this.groupBox1.Controls.Add(this.label12);
      this.groupBox1.Controls.Add(this.modelFolderLbl);
      this.groupBox1.Controls.Add(this.modelFolderBrowseBtn);
      this.groupBox1.Controls.Add(this.label10);
      this.groupBox1.Controls.Add(this.label9);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.dfltBenchBuildCbo);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.dfltSimBuildCbo);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.dfltManagerBuildCbo);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.initDataRemove);
      this.groupBox1.Controls.Add(this.initDataAdd);
      this.groupBox1.Controls.Add(this.initDataLst);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.projectNameTxt);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.masterConfigLbl);
      this.groupBox1.Controls.Add(this.masterConfigBrowse);
      this.groupBox1.Controls.Add(this.solutionCbo);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(6, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(571, 521);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      // 
      // uiVisualStudioBinPath
      // 
      this.uiVisualStudioBinPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.uiVisualStudioBinPath.FormattingEnabled = true;
      this.uiVisualStudioBinPath.Location = new System.Drawing.Point(373, 349);
      this.uiVisualStudioBinPath.Name = "uiVisualStudioBinPath";
      this.uiVisualStudioBinPath.Size = new System.Drawing.Size(185, 21);
      this.uiVisualStudioBinPath.Sorted = true;
      this.uiVisualStudioBinPath.TabIndex = 32;
      // 
      // uiVisualStudioVersion
      // 
      this.uiVisualStudioVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.uiVisualStudioVersion.FormattingEnabled = true;
      this.uiVisualStudioVersion.Location = new System.Drawing.Point(169, 349);
      this.uiVisualStudioVersion.Name = "uiVisualStudioVersion";
      this.uiVisualStudioVersion.Size = new System.Drawing.Size(185, 21);
      this.uiVisualStudioVersion.Sorted = true;
      this.uiVisualStudioVersion.TabIndex = 31;
      this.uiVisualStudioVersion.SelectedIndexChanged += new System.EventHandler(this.uiVisualStudioVersion_SelectedIndexChanged);
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Location = new System.Drawing.Point(18, 352);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(105, 13);
      this.label16.TabIndex = 38;
      this.label16.Text = "VisualStudio version:";
      // 
      // uiManagerOnly
      // 
      this.uiManagerOnly.AutoSize = true;
      this.uiManagerOnly.Location = new System.Drawing.Point(382, 259);
      this.uiManagerOnly.Name = "uiManagerOnly";
      this.uiManagerOnly.Size = new System.Drawing.Size(90, 17);
      this.uiManagerOnly.TabIndex = 37;
      this.uiManagerOnly.Text = "Manager-only";
      this.uiManagerOnly.UseVisualStyleBackColor = true;
      this.uiManagerOnly.CheckedChanged += new System.EventHandler(this.uiManagerOnly_CheckedChanged);
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label15.Location = new System.Drawing.Point(256, 26);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(206, 13);
      this.label15.TabIndex = 36;
      this.label15.Text = "Examples: c:\\dev\\stable\\400.125.1\\source\\";
      // 
      // sourceFolderTxt
      // 
      this.sourceFolderTxt.Location = new System.Drawing.Point(108, 22);
      this.sourceFolderTxt.Name = "sourceFolderTxt";
      this.sourceFolderTxt.Size = new System.Drawing.Size(142, 20);
      this.sourceFolderTxt.TabIndex = 35;
      this.sourceFolderTxt.Validating += new System.ComponentModel.CancelEventHandler(this.sourceFolderTxt_Validating);
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(18, 25);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(73, 13);
      this.label11.TabIndex = 34;
      this.label11.Text = "Source folder:";
      // 
      // worldSpecLbl
      // 
      this.worldSpecLbl.AutoEllipsis = true;
      this.worldSpecLbl.Location = new System.Drawing.Point(108, 481);
      this.worldSpecLbl.Name = "worldSpecLbl";
      this.worldSpecLbl.Size = new System.Drawing.Size(416, 13);
      this.worldSpecLbl.TabIndex = 33;
      this.worldSpecLbl.Text = "<<set by code>>";
      // 
      // worldSpecBrowse
      // 
      this.worldSpecBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.worldSpecBrowse.Location = new System.Drawing.Point(530, 475);
      this.worldSpecBrowse.Name = "worldSpecBrowse";
      this.worldSpecBrowse.Size = new System.Drawing.Size(28, 24);
      this.worldSpecBrowse.TabIndex = 36;
      this.worldSpecBrowse.Text = "...";
      this.worldSpecBrowse.UseVisualStyleBackColor = true;
      this.worldSpecBrowse.Click += new System.EventHandler(this.worldSpecBrowse_Click);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(18, 481);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(85, 13);
      this.label14.TabIndex = 31;
      this.label14.Text = "World spec doc:";
      // 
      // dfltBenchPlatformCbo
      // 
      this.dfltBenchPlatformCbo.FormattingEnabled = true;
      this.dfltBenchPlatformCbo.Items.AddRange(new object[] {
            "Win32",
            "x86"});
      this.dfltBenchPlatformCbo.Location = new System.Drawing.Point(278, 311);
      this.dfltBenchPlatformCbo.Name = "dfltBenchPlatformCbo";
      this.dfltBenchPlatformCbo.Size = new System.Drawing.Size(89, 21);
      this.dfltBenchPlatformCbo.TabIndex = 30;
      this.dfltBenchPlatformCbo.Text = "Win32";
      // 
      // dfltSimPlatformCbo
      // 
      this.dfltSimPlatformCbo.FormattingEnabled = true;
      this.dfltSimPlatformCbo.Items.AddRange(new object[] {
            "Win32",
            "x86"});
      this.dfltSimPlatformCbo.Location = new System.Drawing.Point(278, 284);
      this.dfltSimPlatformCbo.Name = "dfltSimPlatformCbo";
      this.dfltSimPlatformCbo.Size = new System.Drawing.Size(89, 21);
      this.dfltSimPlatformCbo.TabIndex = 29;
      this.dfltSimPlatformCbo.Text = "Win32";
      // 
      // dfltManagerPlatformCbo
      // 
      this.dfltManagerPlatformCbo.FormattingEnabled = true;
      this.dfltManagerPlatformCbo.Items.AddRange(new object[] {
            "Win32",
            "x86"});
      this.dfltManagerPlatformCbo.Location = new System.Drawing.Point(278, 257);
      this.dfltManagerPlatformCbo.Name = "dfltManagerPlatformCbo";
      this.dfltManagerPlatformCbo.Size = new System.Drawing.Size(89, 21);
      this.dfltManagerPlatformCbo.TabIndex = 28;
      this.dfltManagerPlatformCbo.Text = "x86";
      // 
      // specLbl
      // 
      this.specLbl.AutoEllipsis = true;
      this.specLbl.Location = new System.Drawing.Point(108, 451);
      this.specLbl.Name = "specLbl";
      this.specLbl.Size = new System.Drawing.Size(416, 13);
      this.specLbl.TabIndex = 27;
      this.specLbl.Text = "<<set by code>>";
      // 
      // specBrowse
      // 
      this.specBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.specBrowse.Location = new System.Drawing.Point(530, 445);
      this.specBrowse.Name = "specBrowse";
      this.specBrowse.Size = new System.Drawing.Size(28, 24);
      this.specBrowse.TabIndex = 35;
      this.specBrowse.Text = "...";
      this.specBrowse.UseVisualStyleBackColor = true;
      this.specBrowse.Click += new System.EventHandler(this.specBrowse_Click);
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(18, 451);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(56, 13);
      this.label13.TabIndex = 25;
      this.label13.Text = "Spec doc:";
      // 
      // worldFolderLbl
      // 
      this.worldFolderLbl.AutoEllipsis = true;
      this.worldFolderLbl.Location = new System.Drawing.Point(108, 421);
      this.worldFolderLbl.Name = "worldFolderLbl";
      this.worldFolderLbl.Size = new System.Drawing.Size(416, 13);
      this.worldFolderLbl.TabIndex = 24;
      this.worldFolderLbl.Text = "<<set by code>>";
      // 
      // worldFolderBrowse
      // 
      this.worldFolderBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.worldFolderBrowse.Location = new System.Drawing.Point(530, 415);
      this.worldFolderBrowse.Name = "worldFolderBrowse";
      this.worldFolderBrowse.Size = new System.Drawing.Size(28, 24);
      this.worldFolderBrowse.TabIndex = 34;
      this.worldFolderBrowse.Text = "...";
      this.worldFolderBrowse.UseVisualStyleBackColor = true;
      this.worldFolderBrowse.Click += new System.EventHandler(this.worldFolderBrowse_Click);
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(18, 421);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(67, 13);
      this.label12.TabIndex = 22;
      this.label12.Text = "World folder:";
      // 
      // modelFolderLbl
      // 
      this.modelFolderLbl.AutoEllipsis = true;
      this.modelFolderLbl.Location = new System.Drawing.Point(108, 391);
      this.modelFolderLbl.Name = "modelFolderLbl";
      this.modelFolderLbl.Size = new System.Drawing.Size(413, 13);
      this.modelFolderLbl.TabIndex = 21;
      this.modelFolderLbl.Text = "<<set by code>>";
      // 
      // modelFolderBrowseBtn
      // 
      this.modelFolderBrowseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.modelFolderBrowseBtn.Location = new System.Drawing.Point(530, 385);
      this.modelFolderBrowseBtn.Name = "modelFolderBrowseBtn";
      this.modelFolderBrowseBtn.Size = new System.Drawing.Size(28, 24);
      this.modelFolderBrowseBtn.TabIndex = 33;
      this.modelFolderBrowseBtn.Text = "...";
      this.modelFolderBrowseBtn.UseVisualStyleBackColor = true;
      this.modelFolderBrowseBtn.Click += new System.EventHandler(this.modelFolderBrowseBtn_Click);
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(18, 391);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(68, 13);
      this.label10.TabIndex = 19;
      this.label10.Text = "Model folder:";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.ForeColor = System.Drawing.Color.Red;
      this.label9.Location = new System.Drawing.Point(7, 136);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(14, 16);
      this.label9.TabIndex = 18;
      this.label9.Text = "*";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(18, 97);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "MasterConfig:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.ForeColor = System.Drawing.Color.Red;
      this.label8.Location = new System.Drawing.Point(7, 95);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(14, 16);
      this.label8.TabIndex = 17;
      this.label8.Text = "*";
      // 
      // dfltBenchBuildCbo
      // 
      this.dfltBenchBuildCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.dfltBenchBuildCbo.FormattingEnabled = true;
      this.dfltBenchBuildCbo.Items.AddRange(new object[] {
            "Debug",
            "FastDebug",
            "Release"});
      this.dfltBenchBuildCbo.Location = new System.Drawing.Point(169, 311);
      this.dfltBenchBuildCbo.Name = "dfltBenchBuildCbo";
      this.dfltBenchBuildCbo.Size = new System.Drawing.Size(103, 21);
      this.dfltBenchBuildCbo.TabIndex = 16;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(18, 314);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(134, 13);
      this.label7.TabIndex = 15;
      this.label7.Text = "Default Bench build profile:";
      // 
      // dfltSimBuildCbo
      // 
      this.dfltSimBuildCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.dfltSimBuildCbo.FormattingEnabled = true;
      this.dfltSimBuildCbo.Items.AddRange(new object[] {
            "Debug",
            "FastDebug",
            "Release"});
      this.dfltSimBuildCbo.Location = new System.Drawing.Point(169, 284);
      this.dfltSimBuildCbo.Name = "dfltSimBuildCbo";
      this.dfltSimBuildCbo.Size = new System.Drawing.Size(103, 21);
      this.dfltSimBuildCbo.TabIndex = 14;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(18, 287);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(120, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "Default Sim build profile:";
      // 
      // dfltManagerBuildCbo
      // 
      this.dfltManagerBuildCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.dfltManagerBuildCbo.FormattingEnabled = true;
      this.dfltManagerBuildCbo.Items.AddRange(new object[] {
            "Debug",
            "FastDebug",
            "Release"});
      this.dfltManagerBuildCbo.Location = new System.Drawing.Point(169, 257);
      this.dfltManagerBuildCbo.Name = "dfltManagerBuildCbo";
      this.dfltManagerBuildCbo.Size = new System.Drawing.Size(103, 21);
      this.dfltManagerBuildCbo.TabIndex = 12;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(18, 260);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(145, 13);
      this.label5.TabIndex = 11;
      this.label5.Text = "Default Manager build profile:";
      // 
      // initDataRemove
      // 
      this.initDataRemove.Location = new System.Drawing.Point(373, 209);
      this.initDataRemove.Name = "initDataRemove";
      this.initDataRemove.Size = new System.Drawing.Size(64, 24);
      this.initDataRemove.TabIndex = 10;
      this.initDataRemove.Text = "Remove";
      this.initDataRemove.UseVisualStyleBackColor = true;
      this.initDataRemove.Click += new System.EventHandler(this.initDataRemove_Click);
      // 
      // initDataAdd
      // 
      this.initDataAdd.Location = new System.Drawing.Point(373, 179);
      this.initDataAdd.Name = "initDataAdd";
      this.initDataAdd.Size = new System.Drawing.Size(64, 24);
      this.initDataAdd.TabIndex = 9;
      this.initDataAdd.Text = "Add";
      this.initDataAdd.UseVisualStyleBackColor = true;
      this.initDataAdd.Click += new System.EventHandler(this.initDataAdd_Click);
      // 
      // initDataLst
      // 
      this.initDataLst.FormattingEnabled = true;
      this.initDataLst.Location = new System.Drawing.Point(108, 179);
      this.initDataLst.Name = "initDataLst";
      this.initDataLst.Size = new System.Drawing.Size(259, 56);
      this.initDataLst.TabIndex = 8;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(18, 179);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(52, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "InitDatas:";
      // 
      // projectNameTxt
      // 
      this.projectNameTxt.Location = new System.Drawing.Point(108, 135);
      this.projectNameTxt.Name = "projectNameTxt";
      this.projectNameTxt.Size = new System.Drawing.Size(329, 20);
      this.projectNameTxt.TabIndex = 6;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(18, 138);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(72, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Project name:";
      // 
      // masterConfigLbl
      // 
      this.masterConfigLbl.AutoEllipsis = true;
      this.masterConfigLbl.Location = new System.Drawing.Point(191, 97);
      this.masterConfigLbl.Name = "masterConfigLbl";
      this.masterConfigLbl.Size = new System.Drawing.Size(246, 13);
      this.masterConfigLbl.TabIndex = 4;
      this.masterConfigLbl.Text = "<<set by code>>";
      // 
      // masterConfigBrowse
      // 
      this.masterConfigBrowse.Location = new System.Drawing.Point(108, 89);
      this.masterConfigBrowse.Name = "masterConfigBrowse";
      this.masterConfigBrowse.Size = new System.Drawing.Size(67, 28);
      this.masterConfigBrowse.TabIndex = 3;
      this.masterConfigBrowse.Text = "Browse";
      this.masterConfigBrowse.UseVisualStyleBackColor = true;
      this.masterConfigBrowse.Click += new System.EventHandler(this.masterConfigBrowse_Click);
      // 
      // solutionCbo
      // 
      this.solutionCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.solutionCbo.FormattingEnabled = true;
      this.solutionCbo.Location = new System.Drawing.Point(108, 53);
      this.solutionCbo.Name = "solutionCbo";
      this.solutionCbo.Size = new System.Drawing.Size(142, 21);
      this.solutionCbo.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 56);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Solution:";
      // 
      // NewProjectWizardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelBtn;
      this.ClientSize = new System.Drawing.Size(589, 578);
      this.ControlBox = false;
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cancelBtn);
      this.Controls.Add(this.okBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "NewProjectWizardForm";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "New Project Wizard";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.Button okBtn;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox solutionCbo;
    private System.Windows.Forms.Button masterConfigBrowse;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label masterConfigLbl;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox projectNameTxt;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button initDataRemove;
    private System.Windows.Forms.Button initDataAdd;
    private System.Windows.Forms.ListBox initDataLst;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox dfltManagerBuildCbo;
    private System.Windows.Forms.ComboBox dfltBenchBuildCbo;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox dfltSimBuildCbo;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label worldFolderLbl;
    private System.Windows.Forms.Button worldFolderBrowse;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label modelFolderLbl;
    private System.Windows.Forms.Button modelFolderBrowseBtn;
    private System.Windows.Forms.Label specLbl;
    private System.Windows.Forms.Button specBrowse;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.ComboBox dfltManagerPlatformCbo;
    private System.Windows.Forms.ComboBox dfltBenchPlatformCbo;
    private System.Windows.Forms.ComboBox dfltSimPlatformCbo;
    private System.Windows.Forms.TextBox sourceFolderTxt;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label worldSpecLbl;
    private System.Windows.Forms.Button worldSpecBrowse;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.CheckBox uiManagerOnly;
    private System.Windows.Forms.ComboBox uiVisualStudioVersion;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.ComboBox uiVisualStudioBinPath;
  }
}