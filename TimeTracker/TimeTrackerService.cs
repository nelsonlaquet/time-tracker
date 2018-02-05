using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TimeTracker
{
	public struct TimeTrackerConfig
	{
		public TimeSpan StartTime { get; }
		public TimeSpan EndTime { get; }
		public TimeSpan Interval { get; }
		
		public TimeTrackerConfig(TimeSpan startTime, TimeSpan endTime, TimeSpan interval)
		{
			StartTime = startTime;
			EndTime = endTime;
			Interval = interval;

			if (StartTime > EndTime)
				throw new ArgumentException("startTime", "Start time cannot be after the end time");
		}
	}

	public class TimeIntervalPrompt
	{
		public event Action<TimeIntervalPrompt> OnTextSet;

		public TimeTrackerConfig Config { get; }
		public int IntervalCount { get; }
		public DateTime IntervalTime { get; }
		public string Text { get; private set; }
		public bool IsSet { get; private set; }
		
		public TimeIntervalPrompt(TimeTrackerConfig config, int intervalCount, DateTime intervalTime)
		{
			Config = config;
			IntervalCount = intervalCount;
			IntervalTime = intervalTime;
			IsSet = false;
		}

		public void SetText(string text)
		{
			Text = text;
			IsSet = true;
			OnTextSet?.Invoke(this);
		}
	}

	public class TimeTrackerService
	{
		private Timer _timer;
		private TimeTrackerConfig _config;
		private bool _isRunning;
		private readonly Timer _startStopTimer;
		private readonly List<TimeIntervalPrompt> _allPrompts;

		public event Action<TimeTrackerService, TimeIntervalPrompt> OnTimeIntervalPrompt;
		public event Action<TimeTrackerService, TimeIntervalPrompt> OnTimeIntervalSet;
		public event Action<TimeTrackerService> OnIsRunningChanged;

		public IEnumerable<TimeIntervalPrompt> AllPrompts => _allPrompts;
		public IEnumerable<TimeIntervalPrompt> AllUnsetPrompts => _allPrompts.Where(p => !p.IsSet);

		public bool IsRunning
		{
			get { return _isRunning; }
			private set
			{
				if (_isRunning == value)
					return;

				_isRunning = value;
				OnIsRunningChanged?.Invoke(this);

				if (value)
					Start();
				else
					Stop();
			}
		}

		public TimeTrackerConfig Config
		{
			get { return _config; }
			set
			{
				if (_timer != null)
					_timer.Dispose();

				_config = value;
				ReloadConfig();
			}
		}

		public DateTime TodaysStart => DateTime.Today + _config.StartTime;
		public DateTime TodaysEnd => DateTime.Today + _config.EndTime;
		
		public TimeTrackerService(TimeTrackerConfig config)
		{
			_allPrompts = new List<TimeIntervalPrompt>();
			_startStopTimer = new Timer(OnStartStopTimer, null, 0, (int)(new TimeSpan(0, 5, 0)).TotalMilliseconds);
			Config = config;
		}

		private void OnTimerTick(object state)
		{
			DateTime now = DateTime.Now;
			var currentInterval = (int)Math.Floor((now.TimeOfDay - _config.StartTime).TotalMilliseconds / _config.Interval.TotalMilliseconds);
			var prompt = new TimeIntervalPrompt(_config, currentInterval, now);
			prompt.OnTextSet += OnPromptSet;
			OnTimeIntervalPrompt?.Invoke(this, prompt);

			void OnPromptSet(TimeIntervalPrompt _)
			{
				prompt.OnTextSet -= OnPromptSet;
				OnTimeIntervalSet?.Invoke(this, prompt);
			}
		}

		private void OnStartStopTimer(object state)
		{
			var now = DateTime.Now;
			var shouldBeRunning = now >= TodaysStart && now <= TodaysEnd;

			if (_isRunning == shouldBeRunning)
				return;

			IsRunning = shouldBeRunning;
		}

		private void ReloadConfig()
		{
			if (!IsRunning)
				return;

			var now = DateTime.Now;

			var intervalsElapsed = (int) Math.Floor((now.TimeOfDay - _config.StartTime).TotalMilliseconds / _config.Interval.TotalMilliseconds);
			var lastIntervalInMilliseconds = (int)(_config.StartTime.TotalMilliseconds + intervalsElapsed * _config.Interval.TotalMilliseconds);
			var nextIntervalInMilliseconds = (int)(_config.StartTime.TotalMilliseconds + (intervalsElapsed + 1) * _config.Interval.TotalMilliseconds);
			var timeToWaitInMilliseconds = nextIntervalInMilliseconds - (int) now.TimeOfDay.TotalMilliseconds;

			_timer = new Timer(OnTimerTick, null, timeToWaitInMilliseconds, (int)_config.Interval.TotalMilliseconds);
		}

		private void Stop()
		{
			_allPrompts.Clear();

			if (_timer != null)
				_timer.Dispose();

			_timer = null;
		}

		private void Start()
		{
			_allPrompts.Clear();
			ReloadConfig();
		}
	}
}
