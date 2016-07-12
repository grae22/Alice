namespace alice
{
  partial class ScheduledTasks
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
      this.entryList = new System.Windows.Forms.ListBox();
      this.removeBtn = new System.Windows.Forms.Button();
      this.resetBtn = new System.Windows.Forms.Button();
      this.closeBtn = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.nextRunLbl = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.projectLbl = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.runTimePicker = new System.Windows.Forms.DateTimePicker();
      this.SuspendLayout();
      // 
      // entryList
      // 
      this.entryList.FormattingEnabled = true;
      this.entryList.Location = new System.Drawing.Point( 12, 12 );
      this.entryList.Name = "entryList";
      this.entryList.Size = new System.Drawing.Size( 294, 199 );
      this.entryList.TabIndex = 0;
      this.entryList.SelectedIndexChanged += new System.EventHandler( this.entryList_SelectedIndexChanged );
      // 
      // removeBtn
      // 
      this.removeBtn.Location = new System.Drawing.Point( 312, 12 );
      this.removeBtn.Name = "removeBtn";
      this.removeBtn.Size = new System.Drawing.Size( 112, 28 );
      this.removeBtn.TabIndex = 1;
      this.removeBtn.Text = "Remove";
      this.removeBtn.UseVisualStyleBackColor = true;
      this.removeBtn.Click += new System.EventHandler( this.removeBtn_Click );
      // 
      // resetBtn
      // 
      this.resetBtn.Location = new System.Drawing.Point( 312, 243 );
      this.resetBtn.Name = "resetBtn";
      this.resetBtn.Size = new System.Drawing.Size( 112, 22 );
      this.resetBtn.TabIndex = 3;
      this.resetBtn.Text = "Update Next-Run";
      this.resetBtn.UseVisualStyleBackColor = true;
      this.resetBtn.Click += new System.EventHandler( this.resetBtn_Click );
      // 
      // closeBtn
      // 
      this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.closeBtn.Location = new System.Drawing.Point( 312, 46 );
      this.closeBtn.Name = "closeBtn";
      this.closeBtn.Size = new System.Drawing.Size( 112, 28 );
      this.closeBtn.TabIndex = 2;
      this.closeBtn.Text = "Close";
      this.closeBtn.UseVisualStyleBackColor = true;
      this.closeBtn.Click += new System.EventHandler( this.closeBtn_Click );
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 12, 248 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 55, 13 );
      this.label1.TabIndex = 4;
      this.label1.Text = "Next Run:";
      // 
      // nextRunLbl
      // 
      this.nextRunLbl.AutoSize = true;
      this.nextRunLbl.ForeColor = System.Drawing.Color.Blue;
      this.nextRunLbl.Location = new System.Drawing.Point( 73, 248 );
      this.nextRunLbl.Name = "nextRunLbl";
      this.nextRunLbl.Size = new System.Drawing.Size( 114, 13 );
      this.nextRunLbl.TabIndex = 5;
      this.nextRunLbl.Text = ">>>Next Run Time<<<";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point( 12, 224 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size( 43, 13 );
      this.label3.TabIndex = 8;
      this.label3.Text = "Project:";
      // 
      // projectLbl
      // 
      this.projectLbl.AutoSize = true;
      this.projectLbl.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.projectLbl.ForeColor = System.Drawing.Color.Blue;
      this.projectLbl.Location = new System.Drawing.Point( 73, 222 );
      this.projectLbl.Name = "projectLbl";
      this.projectLbl.Size = new System.Drawing.Size( 150, 16 );
      this.projectLbl.TabIndex = 9;
      this.projectLbl.Text = ">>>Project Name<<<";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point( 309, 175 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 52, 13 );
      this.label2.TabIndex = 10;
      this.label2.Text = "Run time:";
      // 
      // runTimePicker
      // 
      this.runTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.runTimePicker.Location = new System.Drawing.Point( 312, 191 );
      this.runTimePicker.Name = "runTimePicker";
      this.runTimePicker.Size = new System.Drawing.Size( 112, 20 );
      this.runTimePicker.TabIndex = 11;
      // 
      // ScheduledTasks
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 436, 275 );
      this.ControlBox = false;
      this.Controls.Add( this.runTimePicker );
      this.Controls.Add( this.label2 );
      this.Controls.Add( this.projectLbl );
      this.Controls.Add( this.label3 );
      this.Controls.Add( this.nextRunLbl );
      this.Controls.Add( this.label1 );
      this.Controls.Add( this.closeBtn );
      this.Controls.Add( this.resetBtn );
      this.Controls.Add( this.removeBtn );
      this.Controls.Add( this.entryList );
      this.Name = "ScheduledTasks";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Scheduled Tasks";
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox entryList;
    private System.Windows.Forms.Button removeBtn;
    private System.Windows.Forms.Button resetBtn;
    private System.Windows.Forms.Button closeBtn;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label nextRunLbl;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label projectLbl;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker runTimePicker;
  }
}