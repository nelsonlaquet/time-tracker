using System;
using System.Windows.Forms;

namespace TimeTracker.Controls
{
	public partial class TimeIntervalControl : UserControl
	{
		private readonly TimeIntervalPrompt _prompt;

		public TimeIntervalControl(TimeIntervalPrompt prompt)
		{
			InitializeComponent();
			_prompt = prompt;
		}

		private void SetIntervalButton_Click(object sender, EventArgs e)
		{
			_prompt.SetText(SetIntervalText.Text);
		}

		private void SetIntervalText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != (char)13)
				return;

			SetIntervalButton_Click(sender, new EventArgs());
			e.Handled = true;
		}

		private void TimeIntervalControl_Load(object sender, EventArgs e)
		{
			DateLabel.Text = $"{_prompt.IntervalTime.ToString("h:mm tt")}: ";
		}
	}
}
