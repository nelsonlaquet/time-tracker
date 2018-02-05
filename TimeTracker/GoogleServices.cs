using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TimeTracker
{
	public class GoogleConfig
	{
		public string ClientId { get; }
		public string ClientSecret { get; }

		public GoogleConfig(string clientId, string clientSecret)
		{
			ClientId = clientId;
			ClientSecret = clientSecret;
		}
	}

	public class Sheet
	{
		public string Name { get; }
		public string Id { get; }
		public DateTime ModifiedTime { get; }

		public Sheet(string id, string name, DateTime modifiedTime)
		{
			Name = name;
			Id = id;
			ModifiedTime = modifiedTime;
		}
	}

	public class GoogleServices
	{
		private static string[] Scopes = { SheetsService.Scope.Spreadsheets, DriveService.Scope.DriveReadonly };
		private static string ApplicationName = "Time Tracker";

		private SheetsService _sheetsService;
		private DriveService _driveService;

		public GoogleServices(GoogleConfig googleConfig)
		{
			var secrets = new ClientSecrets
			{
				ClientId = googleConfig.ClientId,
				ClientSecret = googleConfig.ClientSecret
			};

			var fileStore = new FileDataStore(Path.Combine(Directory.GetCurrentDirectory(), "files"), true);
			var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, Scopes, "user", CancellationToken.None, fileStore).Result;

			BaseClientService.Initializer initializer = new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName
			};

			_sheetsService = new SheetsService(initializer);
			_driveService = new DriveService(initializer);
		}

		public async Task<IEnumerable<Sheet>> ListSheets()
		{
			var listRequest = _driveService.Files.List();
			listRequest.PageSize = 25;
			listRequest.Fields = "nextPageToken, files(name, id, modifiedTime)";
			listRequest.Q = "mimeType='application/vnd.google-apps.spreadsheet'";

			var pageStreamer = new PageStreamer<Google.Apis.Drive.v3.Data.File, FilesResource.ListRequest, FileList, string>(
				(req, token) => req.PageToken = token,
				response => response.NextPageToken,
				response => response.Files);

			var allSheets = await pageStreamer.FetchAllAsync(listRequest, CancellationToken.None);
			var list = new List<Sheet>(allSheets.Select(file => new Sheet(file.Id, file.Name, file.ModifiedTime ?? new DateTime())));
			list.OrderByDescending(sheet => sheet.ModifiedTime);
			return list;
		}

		public void SetPrompt(string sheetId, TimeIntervalPrompt prompt)
		{
			var range = $"A{prompt.IntervalCount}:B{prompt.IntervalCount}";
			var valueRange = new ValueRange
			{
				Range = range,
				Values = new List<IList<object>>
				{
					new List<object>
					{
						prompt.IntervalTime.ToString("h:mm tt"),
						prompt.Text
					}
				}
			};

			var request = new SpreadsheetsResource.ValuesResource.UpdateRequest(_sheetsService, valueRange, sheetId, range);
			request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
			var response = request.Execute();
		}
	}
}
