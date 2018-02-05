using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace TimeTracker.Config
{
	public class ConfigValues
	{
		public GoogleConfig Google { get; }

		public ConfigValues(GoogleConfig google)
		{
			Google = google;
		}
	}

	public class ProgramConfig
	{
		static ProgramConfig()
		{
			_instance = new ProgramConfig();
		}

		private static ProgramConfig _instance;
		public static ConfigValues Values => _instance.ConfigValues;

		public ConfigValues ConfigValues { get; }

		private ProgramConfig()
		{
			try
			{
				using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TimeTracker.Config.ProgramConfig.json"))
				using (var reader = new StreamReader(stream))
				{
					ConfigValues = JsonConvert.DeserializeObject<ConfigValues>(reader.ReadToEnd());
				}
			}
			catch (Exception e)
			{
				throw new Exception("Could not load config! Please create a Config/Config.json file and set is as an embedded resource", e);
			}
		}
	}
}
