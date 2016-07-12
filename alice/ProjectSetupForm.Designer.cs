namespace alice
{
  partial class ProjectSetupForm
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.projectNameTxt = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.okBtn = new System.Windows.Forms.Button();
      this.cancelBtn = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add( this.projectNameTxt );
      this.groupBox1.Controls.Add( this.label1 );
      this.groupBox1.Location = new System.Drawing.Point( 12, 12 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size( 279, 70 );
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "General:";
      // 
      // projectNameTxt
      // 
      this.projectNameTxt.Location = new System.Drawing.Point( 61, 31 );
      this.projectNameTxt.Name = "projectNameTxt";
      this.projectNameTxt.Size = new System.Drawing.Size( 202, 20 );
      this.projectNameTxt.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 17, 34 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 38, 13 );
      this.label1.TabIndex = 0;
      this.label1.Text = "Name:";
      // 
      // okBtn
      // 
      this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okBtn.Location = new System.Drawing.Point( 133, 88 );
      this.okBtn.Name = "okBtn";
      this.okBtn.Size = new System.Drawing.Size( 75, 23 );
      this.okBtn.TabIndex = 1;
      this.okBtn.Text = "OK";
      this.okBtn.UseVisualStyleBackColor = true;
      // 
      // cancelBtn
      // 
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point( 216, 88 );
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size( 75, 23 );
      this.cancelBtn.TabIndex = 2;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      // 
      // ProjectSetupForm
      // 
      this.AcceptButton = this.okBtn;
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelBtn;
      this.ClientSize = new System.Drawing.Size( 303, 124 );
      this.ControlBox = false;
      this.Controls.Add( this.cancelBtn );
      this.Controls.Add( this.okBtn );
      this.Controls.Add( this.groupBox1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "ProjectSetupForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Project Setup";
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox projectNameTxt;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button okBtn;
    private System.Windows.Forms.Button cancelBtn;
  }
}