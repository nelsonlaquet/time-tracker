using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TimeTracker.Config;
using TimeTracker.Controls;
using TimeTracker.Forms;

namespace TimeTracker
{
	public partial class MainForm : Form
	{
		private bool _wasClosed;
		private bool _isCreated;
		private string _lastText;

		private readonly List<Action> _isCreatedQueue;
		private readonly GoogleServices _google;
		private readonly TimeTrackerService _timeTrackerService;
		private readonly Dictionary<TimeIntervalPrompt, TimeIntervalControl> _timeIntervals;

		public MainForm()
		{
			_lastText = "";

			InitializeComponent();

			_timeIntervals = new Dictionary<TimeIntervalPrompt, TimeIntervalControl>();
			_isCreatedQueue = new List<Action>();

			UserConfig.OnValuesChanged += OnUserConfigChanged;
			_google = new GoogleServices(ProgramConfig.Values.Google);
			_timeTrackerService = new TimeTrackerService(UserConfig.Values.TimeTracker);
			_timeTrackerService.OnTimeIntervalPrompt += OnTimeIntervalPrompt;
			_timeTrackerService.OnTimeIntervalSet += OnTimeIntervalSet;
			_timeTrackerService.OnIsRunningChanged += OnIsRunningChanged;

			OnIsRunningChanged(_timeTrackerService);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (UserConfig.Values.SheetId == null)
				SelectSheet();

			_isCreated = true;
			foreach (var action in _isCreatedQueue)
				action();

			_isCreatedQueue.Clear();

			// Quick hack because Application.Run(Form) sets Visible to true
			System.Threading.Timer timer = null;
			timer = new System.Threading.Timer(tag =>
			{
				RunOnUIThread(() => Hide());
				timer.Dispose();
			}, 0, 10, 10000);
		}

		private void OnUserConfigChanged(UserConfigValues config)
		{
			_timeTrackerService.Config = config.TimeTracker;
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			_wasClosed = true;
			Close();
		}

		private void ChangeSheetButton_Click(object sender, EventArgs e) =>
			SelectSheet();

		private void SelectSheet()
		{
			var sheetPicker = new SelectSheet(_google);
			sheetPicker.ShowDialog();

			if (sheetPicker.SelectedSheet == null)
			{
				MessageBox.Show("Invalid sheet selected!");
				Close();
				return;
			}

			UserConfig.Values.SheetId = sheetPicker.SelectedSheet.Id;
		}

		private void OnIsRunningChanged(TimeTrackerService service)
		{
			RunOnUIThread(() =>
			{
				Text = service.IsRunning ? "Time Tracker" : "(NOT RUNNING) Time Tracker";
			});
		}

		private void OnTimeIntervalPrompt(TimeTrackerService service, TimeIntervalPrompt prompt)
		{
			RunOnUIThread(() =>
			{
				var control = new TimeIntervalControl(prompt);
				PromptsList.Controls.Add(control);
				if (!Visible)
					control.SetIntervalText.Focus();
				control.SetIntervalText.Text = _lastText;
				Height = Math.Min(PromptsList.InnerHeight + 20, 500);
				_timeIntervals.Add(prompt, control);
				Show();
			});
		}

		private void OnTimeIntervalSet(TimeTrackerService service, TimeIntervalPrompt prompt)
		{
			RunOnUIThread(() =>
			{
				_lastText = prompt.Text;

				var control = _timeIntervals[prompt];
				PromptsList.Controls.Remove(control);
				control.Dispose();

				Height = Math.Min(PromptsList.InnerHeight + 20, 500);
				if (PromptsList.Controls.Count == 0)
					Hide();

				_google.SetPrompt(UserConfig.Values.SheetId, prompt);
			});
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_wasClosed)
				return;

			Hide();
			e.Cancel = true;
		}

		private void RunOnUIThread(Action action)
		{
			if (!_isCreated)
			{
				_isCreatedQueue.Add(action);
				return;
			}

			BeginInvoke((Action)(() =>
			{
				if (Handle != IntPtr.Zero)
					BeginInvoke(action);
			}));
		}

		private void IconMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
		}
	}
}
