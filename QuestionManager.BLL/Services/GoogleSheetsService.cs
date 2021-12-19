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
        private const string UserSpreadSheetId = "1Lpl5v0KVesHfsokT8Bj3lqhvEhzqEw--xnATY6GyiBI";
        private const string QuestionSpreadSheetId = "1t9CyiThwrVqIjnScT5sJCDWBrJ8r2oDSprfg7JcyCyQ";
        private const string MessageSheetId = "1yR63EG6sHJ17TFncJYqg9NwM4g79ZS3HFB4w1sALu8I";
        private readonly string EmailRange = "A:A";
        private readonly string QuestionsAndAnswearsRange = "A2:F1000";
        private readonly string MessageRange = "A2";

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

        private int GetCount(SheetsService service, string range, string sheetId)
        {
            var response = GetList(service, range, sheetId);

            var result = 0;

            foreach (var value in response.Values)
            {
                result++;
            }

            return result;
        }

        private ValueRange GetList(SheetsService service, string range, string spreadSheetId)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadSheetId, range);
            ValueRange response = request.Execute();

            if (response.Values != null)
            {
                return response;
            }

            throw new AppException("The list wasn't recieved");
        }

        private void AddToSheet(SheetsService service, string[] data, int rowIndex, string spreadSheetId)
        {
            List<Request> requests = new List<Request>();

            for (int i = 0; i < data.Length; i++)
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

        private string GetCell(SheetsService service, string cell, string spreadSheetId = UserSpreadSheetId)
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
            var count = GetCount(services, EmailRange, UserSpreadSheetId);
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

            AddToSheet(services, data, count, UserSpreadSheetId);
            var newCount = GetCount(services, EmailRange, UserSpreadSheetId);

            if(newCount > count)
            {
                return true;
            }

            throw new AppException("Result wasn't added to Google Sheets");
        }

        public bool CheckByEmail(string email)
        {
            var credentials = GetSheetCredentials();
            var services = GetService(credentials);
            var emailList = GetList(services, EmailRange, UserSpreadSheetId);

            foreach(var mail in emailList.Values)
            {
                if (mail[0].ToString() == email)
                    return true;
            }
            
            return false;
        }

        public IList<IList<object>> GetAllQuestions()
        {
            var credentials = GetSheetCredentials();
            var services = GetService(credentials);

            var questions = GetList(services, QuestionsAndAnswearsRange, QuestionSpreadSheetId);
            return questions.Values;
        }

        public string GetMessage()
        {
            var credentials = GetSheetCredentials();
            var services = GetService(credentials);

            var message = GetCell(services, MessageRange, MessageSheetId);
            return message;
        }
    }
}
