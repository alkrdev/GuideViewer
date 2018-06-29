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
    public class GoogleRequest
    {
        
        private static readonly string[] Scopes = {
            SheetsService.Scope.SpreadsheetsReadonly
        };
        private UserCredential _credential;

        public SpreadsheetsResource.ValuesResource.GetRequest GoogleRequestInit() {

            //Everytime I mistakenly push the correct link, I revoke the access of the link.
            string myUrl = URLReturner("[REDACTED]");
            
            new WebClient().DownloadFile(myUrl, "\\client_secret.json");
            
            using (var stream = new FileStream("\\client_secret.json", FileMode.Open, FileAccess.Read)) {
                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None,
                    new FileDataStore(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials\\"), true)).Result;
            }
            var service = new SheetsService(new BaseClientService.Initializer {
                HttpClientInitializer = _credential,
                ApplicationName = "GuideViewer"
            });

            File.Delete("\\client_secret.json");

            return service.Spreadsheets.Values.Get("1uLxm0jvmL1_FJNYUJp6YqIezzqrZdjPf2xQGOWYd6ao", "TestSheet!A2:F");
        }

        private string URLReturner(string name) {
            return $"https://api.myjson.com/bins/{name}.json";
        }
    }
}
