using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using QuestionManager.BLL.Helpers;
using QuestionManager.BLL.Services.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace QuestionManager.BLL.Services
{
    public class GoogleSheetsService : IGoogleSheetsService
    {
        private readonly string ClientSecret = "client_secret.json";
        private readonly string[] ScopesSheets = { SheetsService.Scope.Spreadsheets };
        private readonly string AppName = "Desktop client 1";
        private const string SpreadSheetId = "1Lpl5v0KVesHfsokT8Bj3lqhvEhzqEw--xnATY6GyiBI";
        private readonly string ColumnRange = "A:A";

        private UserCredential GetSheetCredentials()
        {
            using (var stream = new FileStream(ClientSecret, FileMode.Open, FileAccess.Read))
            {
                var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "sheetCredentials.json");

                return GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, ScopesSheets, "user", CancellationToken.None, new FileDataStore(credentialPath, true)).Result;
            }
        }

        private SheetsService GetService(UserCredential credential)
        {
            return new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = AppName
            });
        }

        private int GetCount(SheetsService service, string range, string spreadSheetId = SpreadSheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadSheetId, range);
            ValueRange response = request.Execute();

            var result = 0;

            if (response.Values != null)
            {
                foreach (var value in response.Values)
                {
                    result++;
                }

            }

            return result;
        }

        private void AddToSheet(SheetsService service, string[] data, int rowIndex, string spreadSheetId = SpreadSheetId)
        {
            List<Request> requests = new List<Request>();

            for (int i = 0; i < 4; i++)
            {
                List<CellData> values = new List<CellData>();

                values.Add(new CellData
                {
                    UserEnteredValue = new ExtendedValue
                    {
                        StringValue = data[i]
                    }
                });

                requests.Add(new Request
                {
                    UpdateCells = new UpdateCellsRequest
                    {
                        Start = new GridCoordinate
                        {
                            SheetId = 0,
                            RowIndex = rowIndex,
                            ColumnIndex = i
                        },

                        Rows = new List<RowData> { new RowData { Values = values } },
                        Fields = "userEnteredValue"
                    }
                });
            }

            BatchUpdateSpreadsheetRequest busr = new BatchUpdateSpreadsheetRequest
            {
                Requests = requests
            };

            service.Spreadsheets.BatchUpdate(busr, spreadSheetId).Execute();
        }

        private string GetCell(SheetsService service, string cell, string spreadSheetId = SpreadSheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadSheetId, cell);
            ValueRange response = request.Execute();


            if(response.Values != null)
            {
                foreach (var value in response.Values)
                {
                    if(value[0].ToString() != "Последний билет")
                        return value[0].ToString();
                }
            }

            return 0.ToString();
        }

        public bool AddUser(string [] data)
        {
            var credentials = GetSheetCredentials();
            var services = GetService(credentials);
            var count = GetCount(services, ColumnRange);
            var previousLastTicket = GetCell(services, $"D{count}");

            var firstTicket = 0;
            var lastTicket = 0;
            if (data[1] != "0")
            {
                firstTicket = int.Parse(previousLastTicket) + 1;
                lastTicket = int.Parse(previousLastTicket) + int.Parse(data[1]);
            }
            data[2] = firstTicket.ToString();
            data[3] = lastTicket.ToString();

            AddToSheet(services, data, count);
            var newCount = GetCount(services, ColumnRange);

            if(newCount > count)
            {
                return true;
            }

            throw new AppException("Result wasn't added to Google Sheets");
        }
    }
}
