namespace TimeTracker.Forms
{
	partial class SelectSheet
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
			this.ProgressText = new System.Windows.Forms.Label();
			this.SheetsListBox = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// ProgressText
			// 
			this.ProgressText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgressText.Location = new System.Drawing.Point(12, 12);
			this.ProgressText.Name = "ProgressText";
			this.ProgressText.Size = new System.Drawing.Size(532, 355);
			this.ProgressText.TabIndex = 1;
			this.ProgressText.Text = "Loading...";
			this.ProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ProgressText.Visible = false;
			// 
			// SheetsListBox
			// 
			this.SheetsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SheetsListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.SheetsListBox.FullRowSelect = true;
			this.SheetsListBox.HideSelection = false;
			this.SheetsListBox.Location = new System.Drawing.Point(12, 12);
			this.SheetsListBox.MultiSelect = false;
			this.SheetsListBox.Name = "SheetsListBox";
			this.SheetsListBox.Size = new System.Drawing.Size(532, 354);
			this.SheetsListBox.TabIndex = 2;
			this.SheetsListBox.UseCompatibleStateImageBehavior = false;
			this.SheetsListBox.View = System.Windows.Forms.View.Details;
			this.SheetsListBox.DoubleClick += new System.EventHandler(this.SheetsListBox_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "LastModified";
			// 
			// SelectSheet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 378);
			this.Controls.Add(this.ProgressText);
			this.Controls.Add(this.SheetsListBox);
			this.Name = "SelectSheet";
			this.Text = "Select Sheet";
			this.Load += new System.EventHandler(this.SelectSheet_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label ProgressText;
		private System.Windows.Forms.ListView SheetsListBox;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}