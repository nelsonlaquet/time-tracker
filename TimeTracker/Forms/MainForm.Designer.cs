namespace TimeTracker
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.TimeTrackerIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.IconMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ChangeSheetButton = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitButton = new System.Windows.Forms.ToolStripMenuItem();
			this.PromptsList = new TimeTracker.Controls.VerticalStackPanel();
			this.IconMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// TimeTrackerIcon
			// 
			this.TimeTrackerIcon.ContextMenuStrip = this.IconMenuStrip;
			this.TimeTrackerIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TimeTrackerIcon.Icon")));
			this.TimeTrackerIcon.Text = "Time Tracker";
			this.TimeTrackerIcon.Visible = true;
			// 
			// IconMenuStrip
			// 
			this.IconMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeSheetButton,
            this.toolStripSeparator1,
            this.ExitButton});
			this.IconMenuStrip.Name = "IconMenuStrip";
			this.IconMenuStrip.Size = new System.Drawing.Size(148, 54);
			this.IconMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.IconMenuStrip_Opening);
			// 
			// ChangeSheetButton
			// 
			this.ChangeSheetButton.Name = "ChangeSheetButton";
			this.ChangeSheetButton.Size = new System.Drawing.Size(147, 22);
			this.ChangeSheetButton.Text = "Change Sheet";
			this.ChangeSheetButton.Click += new System.EventHandler(this.ChangeSheetButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
			// 
			// ExitButton
			// 
			this.ExitButton.Name = "ExitButton";
			this.ExitButton.Size = new System.Drawing.Size(147, 22);
			this.ExitButton.Text = "Exit";
			this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
			// 
			// PromptsList
			// 
			this.PromptsList.AutoScroll = true;
			this.PromptsList.AutoScrollMinSize = new System.Drawing.Size(0, 10);
			this.PromptsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PromptsList.Location = new System.Drawing.Point(0, 0);
			this.PromptsList.Name = "PromptsList";
			this.PromptsList.Size = new System.Drawing.Size(403, 130);
			this.PromptsList.Space = 10;
			this.PromptsList.StripeColor = null;
			this.PromptsList.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 130);
			this.Controls.Add(this.PromptsList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Time Tracker";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.IconMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.NotifyIcon TimeTrackerIcon;
		private System.Windows.Forms.ContextMenuStrip IconMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ChangeSheetButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExitButton;
		private Controls.VerticalStackPanel PromptsList;
	}
}

