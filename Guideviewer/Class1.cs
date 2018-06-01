using System;
using System.IO;
using System.Net;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;

namespace Guideviewer
{
    public class Class1
    {
        
        private const string ApplicationName = "GuideViewer";
        private static readonly string[] Scopes = {SheetsService.Scope.SpreadsheetsReadonly};
        private UserCredential credential;

        public SpreadsheetsResource.ValuesResource.GetRequest GoogleRequest() {

            String myUrl = URLReturner("[REDACTED]");

            WebClient client = new WebClient();
            {
                client.DownloadFile(myUrl, "\\client_secret.json");
            }
            
            using (var stream = new FileStream("\\client_secret.json", FileMode.Open, FileAccess.Read)) {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials\\");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credential);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1uLxm0jvmL1_FJNYUJp6YqIezzqrZdjPf2xQGOWYd6ao";
            String range = "TestSheet!A2:F";

            File.Delete("\\client_secret.json");

            return  service.Spreadsheets.Values.Get(spreadsheetId, range);
        }

        private String URLReturner(string name) {
            return $"https://api.myjson.com/bins/{name}.json";
        }
         
    }
}
