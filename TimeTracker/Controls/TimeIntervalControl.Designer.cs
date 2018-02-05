namespace TimeTracker.Controls
{
	partial class TimeIntervalControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SetIntervalText = new System.Windows.Forms.TextBox();
			this.SetIntervalButton = new System.Windows.Forms.Button();
			this.DateLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// SetIntervalText
			// 
			this.SetIntervalText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SetIntervalText.Location = new System.Drawing.Point(80, 3);
			this.SetIntervalText.Name = "SetIntervalText";
			this.SetIntervalText.Size = new System.Drawing.Size(345, 20);
			this.SetIntervalText.TabIndex = 0;
			this.SetIntervalText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SetIntervalText_KeyPress);
			// 
			// SetIntervalButton
			// 
			this.SetIntervalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SetIntervalButton.Location = new System.Drawing.Point(431, 3);
			this.SetIntervalButton.Name = "SetIntervalButton";
			this.SetIntervalButton.Size = new System.Drawing.Size(69, 20);
			this.SetIntervalButton.TabIndex = 1;
			this.SetIntervalButton.Text = "Send";
			this.SetIntervalButton.UseVisualStyleBackColor = true;
			this.SetIntervalButton.Click += new System.EventHandler(this.SetIntervalButton_Click);
			// 
			// DateLabel
			// 
			this.DateLabel.Location = new System.Drawing.Point(0, 0);
			this.DateLabel.Name = "DateLabel";
			this.DateLabel.Size = new System.Drawing.Size(74, 27);
			this.DateLabel.TabIndex = 2;
			this.DateLabel.Text = "label1";
			this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TimeIntervalControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.DateLabel);
			this.Controls.Add(this.SetIntervalButton);
			this.Controls.Add(this.SetIntervalText);
			this.Name = "TimeIntervalControl";
			this.Size = new System.Drawing.Size(503, 27);
			this.Load += new System.EventHandler(this.TimeIntervalControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button SetIntervalButton;
		private System.Windows.Forms.Label DateLabel;
		public System.Windows.Forms.TextBox SetIntervalText;
	}
}
