namespace alice
{
  partial class ProjectProfileManagementForm
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.commonValuesBtn = new System.Windows.Forms.Button();
      this.nameTxt = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.copyBtn = new System.Windows.Forms.Button();
      this.deleteBtn = new System.Windows.Forms.Button();
      this.newBtn = new System.Windows.Forms.Button();
      this.profileList = new System.Windows.Forms.ListBox();
      this.closeBtn = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add( this.commonValuesBtn );
      this.groupBox1.Controls.Add( this.nameTxt );
      this.groupBox1.Controls.Add( this.label1 );
      this.groupBox1.Controls.Add( this.copyBtn );
      this.groupBox1.Controls.Add( this.deleteBtn );
      this.groupBox1.Controls.Add( this.newBtn );
      this.groupBox1.Controls.Add( this.profileList );
      this.groupBox1.Location = new System.Drawing.Point( 13, 13 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size( 220, 222 );
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Profiles:";
      // 
      // commonValuesBtn
      // 
      this.commonValuesBtn.Location = new System.Drawing.Point( 7, 187 );
      this.commonValuesBtn.Name = "commonValuesBtn";
      this.commonValuesBtn.Size = new System.Drawing.Size( 120, 23 );
      this.commonValuesBtn.TabIndex = 5;
      this.commonValuesBtn.Text = "Common Values";
      this.commonValuesBtn.UseVisualStyleBackColor = true;
      this.commonValuesBtn.Click += new System.EventHandler( this.commonValuesBtn_Click );
      // 
      // nameTxt
      // 
      this.nameTxt.Location = new System.Drawing.Point( 49, 161 );
      this.nameTxt.Name = "nameTxt";
      this.nameTxt.Size = new System.Drawing.Size( 78, 20 );
      this.nameTxt.TabIndex = 4;
      this.nameTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.nameTxt_KeyPress );
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 4, 164 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 38, 13 );
      this.label1.TabIndex = 4;
      this.label1.Text = "Name:";
      // 
      // copyBtn
      // 
      this.copyBtn.Location = new System.Drawing.Point( 134, 49 );
      this.copyBtn.Name = "copyBtn";
      this.copyBtn.Size = new System.Drawing.Size( 75, 23 );
      this.copyBtn.TabIndex = 2;
      this.copyBtn.Text = "Copy";
      this.copyBtn.UseVisualStyleBackColor = true;
      this.copyBtn.Click += new System.EventHandler( this.copyBtn_Click );
      // 
      // deleteBtn
      // 
      this.deleteBtn.Location = new System.Drawing.Point( 134, 78 );
      this.deleteBtn.Name = "deleteBtn";
      this.deleteBtn.Size = new System.Drawing.Size( 75, 23 );
      this.deleteBtn.TabIndex = 3;
      this.deleteBtn.Text = "Delete";
      this.deleteBtn.UseVisualStyleBackColor = true;
      this.deleteBtn.Click += new System.EventHandler( this.deleteBtn_Click );
      // 
      // newBtn
      // 
      this.newBtn.Location = new System.Drawing.Point( 134, 20 );
      this.newBtn.Name = "newBtn";
      this.newBtn.Size = new System.Drawing.Size( 75, 23 );
      this.newBtn.TabIndex = 1;
      this.newBtn.Text = "New";
      this.newBtn.UseVisualStyleBackColor = true;
      this.newBtn.Click += new System.EventHandler( this.newBtn_Click );
      // 
      // profileList
      // 
      this.profileList.FormattingEnabled = true;
      this.profileList.Location = new System.Drawing.Point( 7, 20 );
      this.profileList.Name = "profileList";
      this.profileList.Size = new System.Drawing.Size( 120, 134 );
      this.profileList.TabIndex = 0;
      this.profileList.SelectedIndexChanged += new System.EventHandler( this.profileList_SelectedIndexChanged );
      // 
      // closeBtn
      // 
      this.closeBtn.Location = new System.Drawing.Point( 160, 250 );
      this.closeBtn.Name = "closeBtn";
      this.closeBtn.Size = new System.Drawing.Size( 75, 23 );
      this.closeBtn.TabIndex = 6;
      this.closeBtn.Text = "Save";
      this.closeBtn.UseVisualStyleBackColor = true;
      this.closeBtn.Click += new System.EventHandler( this.closeBtn_Click );
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point( 79, 250 );
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
      // 
      // ProjectProfileManagementForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 247, 281 );
      this.ControlBox = false;
      this.Controls.Add( this.btnCancel );
      this.Controls.Add( this.closeBtn );
      this.Controls.Add( this.groupBox1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "ProjectProfileManagementForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Profile Management";
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button copyBtn;
    private System.Windows.Forms.Button deleteBtn;
    private System.Windows.Forms.Button newBtn;
    private System.Windows.Forms.ListBox profileList;
    private System.Windows.Forms.TextBox nameTxt;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button closeBtn;
    private System.Windows.Forms.Button commonValuesBtn;
    private System.Windows.Forms.Button btnCancel;
  }
}