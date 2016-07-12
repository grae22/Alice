namespace alice
{
  partial class mainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( mainForm ) );
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.showProjectsBtn = new System.Windows.Forms.Button();
      this.clearFilterBtn = new System.Windows.Forms.Button();
      this.filterTxtBox = new System.Windows.Forms.TextBox();
      this.deleteProjectBtn = new System.Windows.Forms.Button();
      this.copyProjectBtn = new System.Windows.Forms.Button();
      this.newProjectBtn = new System.Windows.Forms.Button();
      this.projectList = new System.Windows.Forms.ListBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.archivedProjectChkBox = new System.Windows.Forms.CheckBox();
      this.shortcutTree = new System.Windows.Forms.TreeView();
      this.manageProfilesBtn = new System.Windows.Forms.Button();
      this.profileCbo = new System.Windows.Forms.ComboBox();
      this.moveShortcutDownBtn = new System.Windows.Forms.Button();
      this.moveShortcutUpBtn = new System.Windows.Forms.Button();
      this.editShortcutBtn = new System.Windows.Forms.Button();
      this.deleteShortcutBtn = new System.Windows.Forms.Button();
      this.copyShortcutBtn = new System.Windows.Forms.Button();
      this.newShortcutBtn = new System.Windows.Forms.Button();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.globalCommonValueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newProjectWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newContainerProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.scheduledTasksToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
      this.scheduledTasksToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add( this.showProjectsBtn );
      this.groupBox1.Controls.Add( this.clearFilterBtn );
      this.groupBox1.Controls.Add( this.filterTxtBox );
      this.groupBox1.Controls.Add( this.deleteProjectBtn );
      this.groupBox1.Controls.Add( this.copyProjectBtn );
      this.groupBox1.Controls.Add( this.newProjectBtn );
      this.groupBox1.Controls.Add( this.projectList );
      this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.groupBox1.Location = new System.Drawing.Point( 12, 27 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new System.Windows.Forms.Padding( 10, 10, 10, 32 );
      this.groupBox1.Size = new System.Drawing.Size( 262, 659 );
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Projects:";
      // 
      // showProjectsBtn
      // 
      this.showProjectsBtn.Location = new System.Drawing.Point( 159, 23 );
      this.showProjectsBtn.Name = "showProjectsBtn";
      this.showProjectsBtn.Size = new System.Drawing.Size( 90, 21 );
      this.showProjectsBtn.TabIndex = 7;
      this.showProjectsBtn.Text = "Show All";
      this.showProjectsBtn.UseVisualStyleBackColor = true;
      this.showProjectsBtn.Click += new System.EventHandler( this.showProjectsBtn_Click );
      // 
      // clearFilterBtn
      // 
      this.clearFilterBtn.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.clearFilterBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.clearFilterBtn.Location = new System.Drawing.Point( 129, 23 );
      this.clearFilterBtn.Name = "clearFilterBtn";
      this.clearFilterBtn.Size = new System.Drawing.Size( 20, 20 );
      this.clearFilterBtn.TabIndex = 6;
      this.clearFilterBtn.Text = "x";
      this.clearFilterBtn.UseVisualStyleBackColor = true;
      this.clearFilterBtn.Click += new System.EventHandler( this.clearFilterBtn_Click );
      // 
      // filterTxtBox
      // 
      this.filterTxtBox.Location = new System.Drawing.Point( 10, 23 );
      this.filterTxtBox.Name = "filterTxtBox";
      this.filterTxtBox.Size = new System.Drawing.Size( 117, 20 );
      this.filterTxtBox.TabIndex = 4;
      this.filterTxtBox.TextChanged += new System.EventHandler( this.filterTxtBox_TextChanged );
      this.filterTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.filterTxtBox_KeyPress );
      // 
      // deleteProjectBtn
      // 
      this.deleteProjectBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.deleteProjectBtn.Location = new System.Drawing.Point( 207, 625 );
      this.deleteProjectBtn.Name = "deleteProjectBtn";
      this.deleteProjectBtn.Size = new System.Drawing.Size( 42, 24 );
      this.deleteProjectBtn.TabIndex = 3;
      this.deleteProjectBtn.Text = "Del";
      this.deleteProjectBtn.UseVisualStyleBackColor = true;
      this.deleteProjectBtn.Click += new System.EventHandler( this.deleteProjectBtn_Click );
      // 
      // copyProjectBtn
      // 
      this.copyProjectBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.copyProjectBtn.Location = new System.Drawing.Point( 79, 625 );
      this.copyProjectBtn.Name = "copyProjectBtn";
      this.copyProjectBtn.Size = new System.Drawing.Size( 63, 24 );
      this.copyProjectBtn.TabIndex = 2;
      this.copyProjectBtn.Text = "Copy";
      this.copyProjectBtn.UseVisualStyleBackColor = true;
      this.copyProjectBtn.Click += new System.EventHandler( this.copyProjectBtn_Click );
      // 
      // newProjectBtn
      // 
      this.newProjectBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.newProjectBtn.Location = new System.Drawing.Point( 10, 625 );
      this.newProjectBtn.Name = "newProjectBtn";
      this.newProjectBtn.Size = new System.Drawing.Size( 63, 24 );
      this.newProjectBtn.TabIndex = 1;
      this.newProjectBtn.Text = "New";
      this.newProjectBtn.UseVisualStyleBackColor = true;
      this.newProjectBtn.Click += new System.EventHandler( this.newProjectBtn_Click );
      // 
      // projectList
      // 
      this.projectList.Font = new System.Drawing.Font( "Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.projectList.FormattingEnabled = true;
      this.projectList.HorizontalScrollbar = true;
      this.projectList.IntegralHeight = false;
      this.projectList.ItemHeight = 16;
      this.projectList.Location = new System.Drawing.Point( 10, 51 );
      this.projectList.Name = "projectList";
      this.projectList.Size = new System.Drawing.Size( 239, 568 );
      this.projectList.Sorted = true;
      this.projectList.TabIndex = 0;
      this.projectList.SelectedIndexChanged += new System.EventHandler( this.projectList_SelectedIndexChanged );
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add( this.archivedProjectChkBox );
      this.groupBox2.Controls.Add( this.shortcutTree );
      this.groupBox2.Controls.Add( this.manageProfilesBtn );
      this.groupBox2.Controls.Add( this.profileCbo );
      this.groupBox2.Controls.Add( this.moveShortcutDownBtn );
      this.groupBox2.Controls.Add( this.moveShortcutUpBtn );
      this.groupBox2.Controls.Add( this.editShortcutBtn );
      this.groupBox2.Controls.Add( this.deleteShortcutBtn );
      this.groupBox2.Controls.Add( this.copyShortcutBtn );
      this.groupBox2.Controls.Add( this.newShortcutBtn );
      this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.groupBox2.Location = new System.Drawing.Point( 280, 27 );
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Padding = new System.Windows.Forms.Padding( 10, 35, 50, 35 );
      this.groupBox2.Size = new System.Drawing.Size( 356, 659 );
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Shortcuts:";
      // 
      // archivedProjectChkBox
      // 
      this.archivedProjectChkBox.AutoSize = true;
      this.archivedProjectChkBox.Location = new System.Drawing.Point( 160, 26 );
      this.archivedProjectChkBox.Name = "archivedProjectChkBox";
      this.archivedProjectChkBox.Size = new System.Drawing.Size( 68, 17 );
      this.archivedProjectChkBox.TabIndex = 9;
      this.archivedProjectChkBox.Text = "Archived";
      this.archivedProjectChkBox.UseVisualStyleBackColor = true;
      this.archivedProjectChkBox.CheckedChanged += new System.EventHandler( this.archivedProjectChkBox_CheckedChanged );
      // 
      // shortcutTree
      // 
      this.shortcutTree.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.shortcutTree.HideSelection = false;
      this.shortcutTree.Location = new System.Drawing.Point( 13, 51 );
      this.shortcutTree.Name = "shortcutTree";
      this.shortcutTree.ShowLines = false;
      this.shortcutTree.ShowPlusMinus = false;
      this.shortcutTree.ShowRootLines = false;
      this.shortcutTree.Size = new System.Drawing.Size( 328, 568 );
      this.shortcutTree.TabIndex = 3;
      this.shortcutTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler( this.shortcutTree_NodeMouseDoubleClick );
      this.shortcutTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler( this.shortcutTree_NodeMouseClick );
      // 
      // manageProfilesBtn
      // 
      this.manageProfilesBtn.Location = new System.Drawing.Point( 100, 23 );
      this.manageProfilesBtn.Name = "manageProfilesBtn";
      this.manageProfilesBtn.Size = new System.Drawing.Size( 40, 21 );
      this.manageProfilesBtn.TabIndex = 6;
      this.manageProfilesBtn.Text = "...";
      this.manageProfilesBtn.UseVisualStyleBackColor = true;
      this.manageProfilesBtn.Click += new System.EventHandler( this.manageProfilesBtn_Click );
      // 
      // profileCbo
      // 
      this.profileCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.profileCbo.FormattingEnabled = true;
      this.profileCbo.Location = new System.Drawing.Point( 13, 23 );
      this.profileCbo.Name = "profileCbo";
      this.profileCbo.Size = new System.Drawing.Size( 81, 21 );
      this.profileCbo.TabIndex = 5;
      this.profileCbo.SelectedIndexChanged += new System.EventHandler( this.profileCbo_SelectedIndexChanged );
      // 
      // moveShortcutDownBtn
      // 
      this.moveShortcutDownBtn.Location = new System.Drawing.Point( 310, 23 );
      this.moveShortcutDownBtn.Name = "moveShortcutDownBtn";
      this.moveShortcutDownBtn.Size = new System.Drawing.Size( 31, 21 );
      this.moveShortcutDownBtn.TabIndex = 8;
      this.moveShortcutDownBtn.Text = "↓";
      this.moveShortcutDownBtn.UseVisualStyleBackColor = true;
      this.moveShortcutDownBtn.Click += new System.EventHandler( this.moveShortcutDownBtn_Click );
      // 
      // moveShortcutUpBtn
      // 
      this.moveShortcutUpBtn.Location = new System.Drawing.Point( 273, 23 );
      this.moveShortcutUpBtn.Name = "moveShortcutUpBtn";
      this.moveShortcutUpBtn.Size = new System.Drawing.Size( 31, 21 );
      this.moveShortcutUpBtn.TabIndex = 7;
      this.moveShortcutUpBtn.Text = "↑";
      this.moveShortcutUpBtn.UseVisualStyleBackColor = true;
      this.moveShortcutUpBtn.Click += new System.EventHandler( this.moveShortcutUpBtn_Click );
      // 
      // editShortcutBtn
      // 
      this.editShortcutBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.editShortcutBtn.Location = new System.Drawing.Point( 148, 625 );
      this.editShortcutBtn.Name = "editShortcutBtn";
      this.editShortcutBtn.Size = new System.Drawing.Size( 63, 24 );
      this.editShortcutBtn.TabIndex = 3;
      this.editShortcutBtn.Text = "Edit";
      this.editShortcutBtn.UseVisualStyleBackColor = true;
      this.editShortcutBtn.Click += new System.EventHandler( this.editShortcutBtn_Click );
      // 
      // deleteShortcutBtn
      // 
      this.deleteShortcutBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.deleteShortcutBtn.Location = new System.Drawing.Point( 298, 625 );
      this.deleteShortcutBtn.Name = "deleteShortcutBtn";
      this.deleteShortcutBtn.Size = new System.Drawing.Size( 43, 24 );
      this.deleteShortcutBtn.TabIndex = 4;
      this.deleteShortcutBtn.Text = "Del";
      this.deleteShortcutBtn.UseVisualStyleBackColor = true;
      this.deleteShortcutBtn.Click += new System.EventHandler( this.deleteShortcutBtn_Click );
      // 
      // copyShortcutBtn
      // 
      this.copyShortcutBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.copyShortcutBtn.Location = new System.Drawing.Point( 79, 625 );
      this.copyShortcutBtn.Name = "copyShortcutBtn";
      this.copyShortcutBtn.Size = new System.Drawing.Size( 63, 24 );
      this.copyShortcutBtn.TabIndex = 2;
      this.copyShortcutBtn.Text = "Copy";
      this.copyShortcutBtn.UseVisualStyleBackColor = true;
      this.copyShortcutBtn.Click += new System.EventHandler( this.copyShortcutBtn_Click );
      // 
      // newShortcutBtn
      // 
      this.newShortcutBtn.ForeColor = System.Drawing.SystemColors.ControlText;
      this.newShortcutBtn.Location = new System.Drawing.Point( 10, 625 );
      this.newShortcutBtn.Name = "newShortcutBtn";
      this.newShortcutBtn.Size = new System.Drawing.Size( 63, 24 );
      this.newShortcutBtn.TabIndex = 1;
      this.newShortcutBtn.Text = "New";
      this.newShortcutBtn.UseVisualStyleBackColor = true;
      this.newShortcutBtn.Click += new System.EventHandler( this.newShortcutBtn_Click );
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.settingsMenu,
            this.toolsToolStripMenuItem} );
      this.menuStrip.Location = new System.Drawing.Point( 0, 0 );
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size( 648, 24 );
      this.menuStrip.TabIndex = 2;
      this.menuStrip.Text = "menuStrip1";
      // 
      // settingsMenu
      // 
      this.settingsMenu.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.globalCommonValueMenuItem} );
      this.settingsMenu.Name = "settingsMenu";
      this.settingsMenu.Size = new System.Drawing.Size( 61, 20 );
      this.settingsMenu.Text = "Settings";
      // 
      // globalCommonValueMenuItem
      // 
      this.globalCommonValueMenuItem.Name = "globalCommonValueMenuItem";
      this.globalCommonValueMenuItem.Size = new System.Drawing.Size( 199, 22 );
      this.globalCommonValueMenuItem.Text = "Global Common Values";
      this.globalCommonValueMenuItem.Click += new System.EventHandler( this.globalCommonValueMenuItem_Click );
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.newProjectWizardToolStripMenuItem,
            this.newContainerProjectToolStripMenuItem,
            this.scheduledTasksToolStripMenuItem,
            this.scheduledTasksToolStripMenuItem1} );
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new System.Drawing.Size( 48, 20 );
      this.toolsToolStripMenuItem.Text = "Tools";
      // 
      // newProjectWizardToolStripMenuItem
      // 
      this.newProjectWizardToolStripMenuItem.Name = "newProjectWizardToolStripMenuItem";
      this.newProjectWizardToolStripMenuItem.Size = new System.Drawing.Size( 193, 22 );
      this.newProjectWizardToolStripMenuItem.Text = "New Project";
      this.newProjectWizardToolStripMenuItem.Click += new System.EventHandler( this.newProjectWizardToolStripMenuItem_Click );
      // 
      // newContainerProjectToolStripMenuItem
      // 
      this.newContainerProjectToolStripMenuItem.Name = "newContainerProjectToolStripMenuItem";
      this.newContainerProjectToolStripMenuItem.Size = new System.Drawing.Size( 193, 22 );
      this.newContainerProjectToolStripMenuItem.Text = "New Container Project";
      this.newContainerProjectToolStripMenuItem.Click += new System.EventHandler( this.newContainerProjectToolStripMenuItem_Click );
      // 
      // scheduledTasksToolStripMenuItem
      // 
      this.scheduledTasksToolStripMenuItem.Name = "scheduledTasksToolStripMenuItem";
      this.scheduledTasksToolStripMenuItem.Size = new System.Drawing.Size( 190, 6 );
      // 
      // scheduledTasksToolStripMenuItem1
      // 
      this.scheduledTasksToolStripMenuItem1.Name = "scheduledTasksToolStripMenuItem1";
      this.scheduledTasksToolStripMenuItem1.Size = new System.Drawing.Size( 193, 22 );
      this.scheduledTasksToolStripMenuItem1.Text = "Scheduled Tasks";
      this.scheduledTasksToolStripMenuItem1.Click += new System.EventHandler( this.scheduledTasksToolStripMenuItem1_Click );
      // 
      // mainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 648, 698 );
      this.Controls.Add( this.groupBox2 );
      this.Controls.Add( this.groupBox1 );
      this.Controls.Add( this.menuStrip );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
      this.KeyPreview = true;
      this.MainMenuStrip = this.menuStrip;
      this.MaximizeBox = false;
      this.Name = "mainForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Alice";
      this.Load += new System.EventHandler( this.mainForm_Load );
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler( this.mainForm_FormClosed );
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.mainForm_KeyPress );
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout( false );
      this.groupBox2.PerformLayout();
      this.menuStrip.ResumeLayout( false );
      this.menuStrip.PerformLayout();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ListBox projectList;
    private System.Windows.Forms.Button newProjectBtn;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button copyProjectBtn;
    private System.Windows.Forms.Button deleteProjectBtn;
    private System.Windows.Forms.Button deleteShortcutBtn;
    private System.Windows.Forms.Button copyShortcutBtn;
    private System.Windows.Forms.Button newShortcutBtn;
    private System.Windows.Forms.Button editShortcutBtn;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem settingsMenu;
    private System.Windows.Forms.ToolStripMenuItem globalCommonValueMenuItem;
    private System.Windows.Forms.Button moveShortcutDownBtn;
    private System.Windows.Forms.Button moveShortcutUpBtn;
    private System.Windows.Forms.ComboBox profileCbo;
    private System.Windows.Forms.Button manageProfilesBtn;
    private System.Windows.Forms.TreeView shortcutTree;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newProjectWizardToolStripMenuItem;
    private System.Windows.Forms.TextBox filterTxtBox;
    private System.Windows.Forms.Button clearFilterBtn;
    private System.Windows.Forms.Button showProjectsBtn;
    private System.Windows.Forms.CheckBox archivedProjectChkBox;
    private System.Windows.Forms.ToolStripMenuItem newContainerProjectToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator scheduledTasksToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem scheduledTasksToolStripMenuItem1;
  }
}

