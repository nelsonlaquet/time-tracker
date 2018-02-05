using Newtonsoft.Json;
using System;
using System.IO;

namespace TimeTracker.Config
{
	public class UserConfigValues
	{
		private string _sheetId;
		private TimeTrackerConfig _timeTrackerConfig;

		public string SheetId
		{
			get { return _sheetId; }
			set
			{
				if (_sheetId == value)
					return;

				_sheetId = value;
				OnConfigChanged?.Invoke(this);
			}
		}

		public TimeTrackerConfig TimeTracker
		{
			get { return _timeTrackerConfig; }
			set
			{
				_timeTrackerConfig = value;
				OnConfigChanged?.Invoke(this);
			}
		}

		public event Action<UserConfigValues> OnConfigChanged;
	}

	public class UserConfig
	{
		static UserConfig()
		{
			_instance = new UserConfig();
		}

		private static UserConfig _instance;

		public static UserConfigValues Values => _instance.ConfigValues;
		public static Action<UserConfigValues> OnValuesChanged;

		public UserConfigValues ConfigValues { get; private set; }

		private UserConfig()
		{
			ConfigValues = new UserConfigValues();
			Reload();
		}

		public void Reload()
		{
			if (ConfigValues != null)
				ConfigValues.OnConfigChanged -= DoOnValuesChanged;

			var file = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
			if (File.Exists(file))
				ConfigValues = JsonConvert.DeserializeObject<UserConfigValues>(File.ReadAllText(file));
			else
				ConfigValues = new UserConfigValues
				{
					TimeTracker = new TimeTrackerConfig(new TimeSpan(8, 30, 0), new TimeSpan(17, 0, 0), new TimeSpan(0, 15, 0))
				};

			ConfigValues.OnConfigChanged += DoOnValuesChanged;
		}

		private void DoOnValuesChanged(UserConfigValues values)
		{
			var file = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
			File.WriteAllText(file, JsonConvert.SerializeObject(ConfigValues, Formatting.Indented));
			OnValuesChanged?.Invoke(values);
		}
	}
}
