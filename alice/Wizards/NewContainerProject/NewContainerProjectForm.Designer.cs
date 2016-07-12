namespace alice
{
  partial class NewContainerProjectForm
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
      if( disposing && ( components != null ) )
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.browseBinderArt = new System.Windows.Forms.Button();
      this.binderArtFilenameTxt = new System.Windows.Forms.TextBox();
      this.binderArtLst = new System.Windows.Forms.ListBox();
      this.label8 = new System.Windows.Forms.Label();
      this.binderArtPic = new System.Windows.Forms.PictureBox();
      this.browseSourceFolderBtn = new System.Windows.Forms.Button();
      this.sourceFolderTxt = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.deployedFolderTxt = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.projectFolderTxt = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.profilesCbo = new System.Windows.Forms.ComboBox();
      this.nameTxt = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.masterconfigBrowse = new System.Windows.Forms.Button();
      this.masterconfigTxt = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.typeCbo = new System.Windows.Forms.ComboBox();
      this.createBtn = new System.Windows.Forms.Button();
      this.cancelBtn = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.binderArtPic ) ).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add( this.browseBinderArt );
      this.groupBox1.Controls.Add( this.binderArtFilenameTxt );
      this.groupBox1.Controls.Add( this.binderArtLst );
      this.groupBox1.Controls.Add( this.label8 );
      this.groupBox1.Controls.Add( this.binderArtPic );
      this.groupBox1.Controls.Add( this.browseSourceFolderBtn );
      this.groupBox1.Controls.Add( this.sourceFolderTxt );
      this.groupBox1.Controls.Add( this.label7 );
      this.groupBox1.Controls.Add( this.deployedFolderTxt );
      this.groupBox1.Controls.Add( this.label6 );
      this.groupBox1.Controls.Add( this.projectFolderTxt );
      this.groupBox1.Controls.Add( this.label5 );
      this.groupBox1.Controls.Add( this.label4 );
      this.groupBox1.Controls.Add( this.profilesCbo );
      this.groupBox1.Controls.Add( this.nameTxt );
      this.groupBox1.Controls.Add( this.label3 );
      this.groupBox1.Controls.Add( this.masterconfigBrowse );
      this.groupBox1.Controls.Add( this.masterconfigTxt );
      this.groupBox1.Controls.Add( this.label2 );
      this.groupBox1.Controls.Add( this.label1 );
      this.groupBox1.Controls.Add( this.typeCbo );
      this.groupBox1.Location = new System.Drawing.Point( 12, 12 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size( 627, 407 );
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Set up your Project:";
      // 
      // browseBinderArt
      // 
      this.browseBinderArt.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.browseBinderArt.Location = new System.Drawing.Point( 570, 224 );
      this.browseBinderArt.Name = "browseBinderArt";
      this.browseBinderArt.Size = new System.Drawing.Size( 42, 20 );
      this.browseBinderArt.TabIndex = 11;
      this.browseBinderArt.Text = "...";
      this.browseBinderArt.UseVisualStyleBackColor = true;
      this.browseBinderArt.Click += new System.EventHandler( this.browseBinderArt_Click );
      // 
      // binderArtFilenameTxt
      // 
      this.binderArtFilenameTxt.Enabled = false;
      this.binderArtFilenameTxt.Location = new System.Drawing.Point( 96, 224 );
      this.binderArtFilenameTxt.Name = "binderArtFilenameTxt";
      this.binderArtFilenameTxt.Size = new System.Drawing.Size( 468, 20 );
      this.binderArtFilenameTxt.TabIndex = 10;
      // 
      // binderArtLst
      // 
      this.binderArtLst.FormattingEnabled = true;
      this.binderArtLst.Items.AddRange( new object[] {
            "Centre",
            "Left",
            "Right",
            "Aux",
            "Logon"} );
      this.binderArtLst.Location = new System.Drawing.Point( 96, 250 );
      this.binderArtLst.Name = "binderArtLst";
      this.binderArtLst.Size = new System.Drawing.Size( 58, 82 );
      this.binderArtLst.TabIndex = 9;
      this.binderArtLst.SelectedIndexChanged += new System.EventHandler( this.binderArtLst_SelectedIndexChanged );
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point( 34, 227 );
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size( 56, 13 );
      this.label8.TabIndex = 17;
      this.label8.Text = "Binder Art:";
      // 
      // binderArtPic
      // 
      this.binderArtPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.binderArtPic.Location = new System.Drawing.Point( 161, 250 );
      this.binderArtPic.Name = "binderArtPic";
      this.binderArtPic.Size = new System.Drawing.Size( 187, 140 );
      this.binderArtPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.binderArtPic.TabIndex = 16;
      this.binderArtPic.TabStop = false;
      // 
      // browseSourceFolderBtn
      // 
      this.browseSourceFolderBtn.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.browseSourceFolderBtn.Location = new System.Drawing.Point( 570, 57 );
      this.browseSourceFolderBtn.Name = "browseSourceFolderBtn";
      this.browseSourceFolderBtn.Size = new System.Drawing.Size( 42, 20 );
      this.browseSourceFolderBtn.TabIndex = 2;
      this.browseSourceFolderBtn.Text = "...";
      this.browseSourceFolderBtn.UseVisualStyleBackColor = true;
      this.browseSourceFolderBtn.Click += new System.EventHandler( this.browseSourceFolderBtn_Click );
      // 
      // sourceFolderTxt
      // 
      this.sourceFolderTxt.Enabled = false;
      this.sourceFolderTxt.Location = new System.Drawing.Point( 96, 57 );
      this.sourceFolderTxt.Name = "sourceFolderTxt";
      this.sourceFolderTxt.Size = new System.Drawing.Size( 468, 20 );
      this.sourceFolderTxt.TabIndex = 1;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point( 17, 60 );
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size( 73, 13 );
      this.label7.TabIndex = 13;
      this.label7.Text = "Source folder:";
      // 
      // deployedFolderTxt
      // 
      this.deployedFolderTxt.Enabled = false;
      this.deployedFolderTxt.Location = new System.Drawing.Point( 96, 161 );
      this.deployedFolderTxt.Name = "deployedFolderTxt";
      this.deployedFolderTxt.Size = new System.Drawing.Size( 516, 20 );
      this.deployedFolderTxt.TabIndex = 7;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point( 6, 164 );
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size( 84, 13 );
      this.label6.TabIndex = 11;
      this.label6.Text = "Deployed folder:";
      // 
      // projectFolderTxt
      // 
      this.projectFolderTxt.Enabled = false;
      this.projectFolderTxt.Location = new System.Drawing.Point( 96, 109 );
      this.projectFolderTxt.Name = "projectFolderTxt";
      this.projectFolderTxt.Size = new System.Drawing.Size( 516, 20 );
      this.projectFolderTxt.TabIndex = 5;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point( 18, 112 );
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size( 72, 13 );
      this.label5.TabIndex = 9;
      this.label5.Text = "Project folder:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point( 46, 192 );
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size( 44, 13 );
      this.label4.TabIndex = 8;
      this.label4.Text = "Profiles:";
      // 
      // profilesCbo
      // 
      this.profilesCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.profilesCbo.FormattingEnabled = true;
      this.profilesCbo.Items.AddRange( new object[] {
            "TEST + DEV",
            "TEST ONLY"} );
      this.profilesCbo.Location = new System.Drawing.Point( 96, 189 );
      this.profilesCbo.Name = "profilesCbo";
      this.profilesCbo.Size = new System.Drawing.Size( 121, 21 );
      this.profilesCbo.TabIndex = 8;
      // 
      // nameTxt
      // 
      this.nameTxt.Enabled = false;
      this.nameTxt.Location = new System.Drawing.Point( 96, 135 );
      this.nameTxt.Name = "nameTxt";
      this.nameTxt.Size = new System.Drawing.Size( 252, 20 );
      this.nameTxt.TabIndex = 6;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point( 52, 138 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size( 38, 13 );
      this.label3.TabIndex = 5;
      this.label3.Text = "Name:";
      // 
      // masterconfigBrowse
      // 
      this.masterconfigBrowse.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.masterconfigBrowse.Location = new System.Drawing.Point( 570, 83 );
      this.masterconfigBrowse.Name = "masterconfigBrowse";
      this.masterconfigBrowse.Size = new System.Drawing.Size( 42, 20 );
      this.masterconfigBrowse.TabIndex = 4;
      this.masterconfigBrowse.Text = "...";
      this.masterconfigBrowse.UseVisualStyleBackColor = true;
      this.masterconfigBrowse.Click += new System.EventHandler( this.masterconfigBrowse_Click );
      // 
      // masterconfigTxt
      // 
      this.masterconfigTxt.Enabled = false;
      this.masterconfigTxt.Location = new System.Drawing.Point( 96, 83 );
      this.masterconfigTxt.Name = "masterconfigTxt";
      this.masterconfigTxt.Size = new System.Drawing.Size( 468, 20 );
      this.masterconfigTxt.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point( 18, 86 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 72, 13 );
      this.label2.TabIndex = 2;
      this.label2.Text = "MasterConfig:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 56, 33 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 34, 13 );
      this.label1.TabIndex = 1;
      this.label1.Text = "Type:";
      // 
      // typeCbo
      // 
      this.typeCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.typeCbo.FormattingEnabled = true;
      this.typeCbo.Location = new System.Drawing.Point( 96, 30 );
      this.typeCbo.Name = "typeCbo";
      this.typeCbo.Size = new System.Drawing.Size( 121, 21 );
      this.typeCbo.TabIndex = 0;
      // 
      // createBtn
      // 
      this.createBtn.Location = new System.Drawing.Point( 505, 443 );
      this.createBtn.Name = "createBtn";
      this.createBtn.Size = new System.Drawing.Size( 64, 28 );
      this.createBtn.TabIndex = 1;
      this.createBtn.Text = "Create";
      this.createBtn.UseVisualStyleBackColor = true;
      this.createBtn.Click += new System.EventHandler( this.createBtn_Click );
      // 
      // cancelBtn
      // 
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point( 575, 443 );
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size( 64, 28 );
      this.cancelBtn.TabIndex = 2;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      this.cancelBtn.Click += new System.EventHandler( this.cancelBtn_Click );
      // 
      // NewContainerProjectForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 651, 483 );
      this.ControlBox = false;
      this.Controls.Add( this.cancelBtn );
      this.Controls.Add( this.createBtn );
      this.Controls.Add( this.groupBox1 );
      this.Name = "NewContainerProjectForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "New Container Project";
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.binderArtPic ) ).EndInit();
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox typeCbo;
    private System.Windows.Forms.TextBox masterconfigTxt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button masterconfigBrowse;
    private System.Windows.Forms.TextBox nameTxt;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox profilesCbo;
    private System.Windows.Forms.Button createBtn;
    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.TextBox projectFolderTxt;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox deployedFolderTxt;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button browseSourceFolderBtn;
    private System.Windows.Forms.TextBox sourceFolderTxt;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ListBox binderArtLst;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.PictureBox binderArtPic;
    private System.Windows.Forms.Button browseBinderArt;
    private System.Windows.Forms.TextBox binderArtFilenameTxt;
  }
}