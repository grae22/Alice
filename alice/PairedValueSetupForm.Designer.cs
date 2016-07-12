namespace alice
{
  partial class PairedValueSetupForm
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
      this.deleteBtn = new System.Windows.Forms.Button();
      this.varList = new System.Windows.Forms.ListBox();
      this.addBtn = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.varValueTxt = new System.Windows.Forms.TextBox();
      this.varNameTxt = new System.Windows.Forms.TextBox();
      this.cancelBtn = new System.Windows.Forms.Button();
      this.okBtn = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add( this.deleteBtn );
      this.groupBox1.Controls.Add( this.varList );
      this.groupBox1.Controls.Add( this.addBtn );
      this.groupBox1.Controls.Add( this.label2 );
      this.groupBox1.Controls.Add( this.label1 );
      this.groupBox1.Controls.Add( this.varValueTxt );
      this.groupBox1.Controls.Add( this.varNameTxt );
      this.groupBox1.Location = new System.Drawing.Point( 13, 13 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new System.Windows.Forms.Padding( 10 );
      this.groupBox1.Size = new System.Drawing.Size( 603, 355 );
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      // 
      // deleteBtn
      // 
      this.deleteBtn.Location = new System.Drawing.Point( 510, 309 );
      this.deleteBtn.Name = "deleteBtn";
      this.deleteBtn.Size = new System.Drawing.Size( 80, 23 );
      this.deleteBtn.TabIndex = 4;
      this.deleteBtn.Text = "Delete";
      this.deleteBtn.UseVisualStyleBackColor = true;
      this.deleteBtn.Click += new System.EventHandler( this.deleteBtn_Click );
      // 
      // varList
      // 
      this.varList.FormattingEnabled = true;
      this.varList.HorizontalScrollbar = true;
      this.varList.Location = new System.Drawing.Point( 16, 78 );
      this.varList.Name = "varList";
      this.varList.Size = new System.Drawing.Size( 574, 225 );
      this.varList.Sorted = true;
      this.varList.TabIndex = 3;
      this.varList.SelectedIndexChanged += new System.EventHandler( this.varList_SelectedIndexChanged );
      // 
      // addBtn
      // 
      this.addBtn.Location = new System.Drawing.Point( 510, 44 );
      this.addBtn.Name = "addBtn";
      this.addBtn.Size = new System.Drawing.Size( 80, 23 );
      this.addBtn.TabIndex = 2;
      this.addBtn.Text = "Add / Update";
      this.addBtn.UseVisualStyleBackColor = true;
      this.addBtn.Click += new System.EventHandler( this.addBtn_Click );
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point( 13, 49 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 37, 13 );
      this.label2.TabIndex = 3;
      this.label2.Text = "Value:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 13, 23 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 38, 13 );
      this.label1.TabIndex = 2;
      this.label1.Text = "Name:";
      // 
      // varValueTxt
      // 
      this.varValueTxt.Location = new System.Drawing.Point( 57, 46 );
      this.varValueTxt.Name = "varValueTxt";
      this.varValueTxt.Size = new System.Drawing.Size( 447, 20 );
      this.varValueTxt.TabIndex = 1;
      this.varValueTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.varValueTxt_KeyPress );
      // 
      // varNameTxt
      // 
      this.varNameTxt.Location = new System.Drawing.Point( 57, 20 );
      this.varNameTxt.Name = "varNameTxt";
      this.varNameTxt.Size = new System.Drawing.Size( 447, 20 );
      this.varNameTxt.TabIndex = 0;
      // 
      // cancelBtn
      // 
      this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelBtn.Location = new System.Drawing.Point( 536, 385 );
      this.cancelBtn.Name = "cancelBtn";
      this.cancelBtn.Size = new System.Drawing.Size( 80, 23 );
      this.cancelBtn.TabIndex = 6;
      this.cancelBtn.Text = "Cancel";
      this.cancelBtn.UseVisualStyleBackColor = true;
      this.cancelBtn.Click += new System.EventHandler( this.cancelBtn_Click );
      // 
      // okBtn
      // 
      this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okBtn.Location = new System.Drawing.Point( 450, 385 );
      this.okBtn.Name = "okBtn";
      this.okBtn.Size = new System.Drawing.Size( 80, 23 );
      this.okBtn.TabIndex = 5;
      this.okBtn.Text = "OK";
      this.okBtn.UseVisualStyleBackColor = true;
      this.okBtn.Click += new System.EventHandler( this.okBtn_Click );
      // 
      // PairedValueSetupForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelBtn;
      this.ClientSize = new System.Drawing.Size( 628, 420 );
      this.ControlBox = false;
      this.Controls.Add( this.okBtn );
      this.Controls.Add( this.cancelBtn );
      this.Controls.Add( this.groupBox1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "PairedValueSetupForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "<set by code>";
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox varValueTxt;
    private System.Windows.Forms.TextBox varNameTxt;
    private System.Windows.Forms.Button addBtn;
    private System.Windows.Forms.Button deleteBtn;
    private System.Windows.Forms.ListBox varList;
    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.Button okBtn;
  }
}