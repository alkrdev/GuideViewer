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

            SheetsService service;

            //Everytime I mistakenly push the correct link, I revoke the access of the link.
            using (WebClient wc = new WebClient()) {
                wc.DownloadFile(URLReturner("[REDACTED]"), "\\client_secret.json");

                using (var stream = new FileStream("\\client_secret.json", FileMode.Open, FileAccess.Read,
                    FileShare.Delete, Int16.MaxValue, FileOptions.DeleteOnClose)) {
                    _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None,
                        new FileDataStore(
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                ".credentials\\"), true)).Result;
                }

                service = new SheetsService(new BaseClientService.Initializer {
                    HttpClientInitializer = _credential,
                    ApplicationName = "GuideViewer"
                });

                File.Delete("\\client_secret.json");
            }

            return service.Spreadsheets.Values.Get("1uLxm0jvmL1_FJNYUJp6YqIezzqrZdjPf2xQGOWYd6ao", "TestSheet!A2:F");
        }

        private string URLReturner(string name) {
            return $"https://api.myjson.com/bins/{name}.json";
        }
    }
}
