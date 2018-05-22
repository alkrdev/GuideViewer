using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;


namespace Guideviewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = {SheetsService.Scope.SpreadsheetsReadonly};

        #region Initialize

        static string[] SkillNames = {
            "Total", "Attack", "Defense",
            "Strength", "Constitution", "Ranged",
            "Prayer", "Magic", "Cooking",
            "Woodcutting", "Fletching", "Fishing",
            "Firemaking", "Crafting", "Smithing",
            "Mining", "Herblore", "Agility",
            "Thieving", "Slayer", "Farming",
            "Runecrafting", "Hunter", "Construction",
            "Summoning", "Dungeoneering", "Divination",
            "Invention"
        };

        public static int[] loadedSkillLevels;
        public static int[] loadedSkillExperiences;

        private const string ApplicationName = "GuideViewer";

        private float widthDef = 1600 / 5;
        private float widthMed = 1600 / 5 / 5 * 3.7f;
        private float widthSm = 1600 / 5 / 4.6f;

        private int deletedFirstRows;

        public IList<IList<Object>> Values;
        public ValueRange Response;

        public List<string> ColumnAList = new List<string>();
        public List<string> ColumnBList = new List<string>();
        public List<string> ColumnCList = new List<string>();
        public List<string> ColumnDList = new List<string>();
        public List<string> ColumnEList = new List<string>();
        public List<string> ColumnFList = new List<string>();

        SaveFileDialog sfd = new SaveFileDialog {
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };


        OpenFileDialog ofd = new OpenFileDialog {
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        public Binding[] Bind = {
            new Binding("Qt"),
            new Binding("L"),
            new Binding("Mqc"),
            new Binding("Cp"),
            new Binding("Tcp"),
            new Binding("Im")
        };

        public string[] Header = {
            "Quest / Train to",
            "Lamps",
            "Master Quest Cape",
            "Completionist Cape",
            "Trimmed Completionist Cape",
            "Ironman"
        };

        public DataGridTextColumn[] Colu = {
            new DataGridTextColumn(),
            new DataGridTextColumn(),
            new DataGridTextColumn(),
            new DataGridTextColumn(),
            new DataGridTextColumn(),
            new DataGridTextColumn()
        };

        public struct MyData {
            public string Qt { set; get; }
            public string L { set; get; }
            public string Mqc { set; get; }
            public string Cp { set; get; }
            public string Tcp { set; get; }
            public string Im { set; get; }
        }

        #endregion

        public MainWindow() {
            InitializeComponent();

            StreamReader sr = new StreamReader("deletedrows.txt");
            deletedFirstRows = Convert.ToInt32(sr.ReadToEnd());

            #region Style

            var style = new Style(typeof(TextBlock));
            var headerstyle = new Style(typeof(DataGridColumnHeader));
            var cell = new Style(typeof(DataGridCell));

            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center));
            style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));

            headerstyle.Setters.Add(new Setter(DataGridColumnHeader.HorizontalContentAlignmentProperty,
                HorizontalAlignment.Center));

            cell.Setters.Add(new Setter(DataGridCell.BackgroundProperty, Brushes.Black));
            cell.Setters.Add(new Setter(DataGridCell.PaddingProperty, new Thickness(3)));

            MyDataGrid.MinRowHeight = 30;

            #endregion

            #region GoogleRequest

            String myUrl = "[Redacted]";

            System.Net.WebClient client = new System.Net.WebClient();
            {
                client.DownloadFile(myUrl, "\\client_secret.json");
            }

            UserCredential credential;
            using (var stream =
                new FileStream("\\client_secret.json", FileMode.Open, FileAccess.Read)) {
                string credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath,
                    ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1uLxm0jvmL1_FJNYUJp6YqIezzqrZdjPf2xQGOWYd6ao";
            String range = "TestSheet!A2:F";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

            File.Delete("\\client_secret.json");

            #endregion

            ValueRange response = request.Execute();
            Response = request.Execute();
            IList<IList<Object>> values = response.Values;
            Values = response.Values;

            #region Create Columns


            if (values != null && values.Count > 0) {
                foreach (var col in values) {
                    if (col[0] != null) {
                        ColumnAList.Add(col[0].ToString());
                    }

                    if (col[1] != null) {
                        ColumnBList.Add(col[1].ToString());
                    }

                    if (col[2] != null) {
                        ColumnCList.Add(col[2].ToString());
                    }

                    if (col[3] != null) {
                        ColumnDList.Add(col[3].ToString());
                    }

                    if (col[4] != null) {
                        ColumnEList.Add(col[4].ToString());
                    }

                    if (col[5] != null) {
                        ColumnFList.Add(col[5].ToString());
                    }
                }
            }

            for (var i = 0; i < 6; i++) {

                Colu[i].Header = Header[i];
                Colu[i].Binding = Bind[i];
                Colu[i].ElementStyle = style;
                Colu[i].HeaderStyle = headerstyle;
                Colu[i].CellStyle = cell;
                switch (i) {
                    case 0:
                        Colu[i].Foreground = Brushes.Aqua;
                        break;
                    case 1:
                        Colu[i].Foreground = Brushes.White;
                        Colu[i].MinWidth = widthSm;
                        //Colu[i].Width = 1600 / 5 / 4.6;
                        Colu[i].MaxWidth = widthSm;
                        break;
                    case 2:
                        Colu[i].Foreground = Brushes.CornflowerBlue;
                        Colu[i].MinWidth = widthMed;
                        //Colu[i].Width = 1600 / 5 / 5 * 3.7;
                        Colu[i].MaxWidth = widthMed;
                        break;
                    case 3:
                        Colu[i].Foreground = Brushes.Brown;
                        break;
                    case 4:
                        Colu[i].Foreground = Brushes.DarkOrange;
                        break;
                    case 5:
                        Colu[i].Foreground = Brushes.DarkGray;
                        break;
                    default:
                        Colu[i].MinWidth = widthDef;
                        //Colu[i].Width = 1600 / 5;
                        Colu[i].MaxWidth = widthDef;
                        break;
                }

                MyDataGrid.Columns.Add(Colu[i]);

            }

            if (values != null) {
                for (int i = 0; i < values.Count; i++) {
                    MyDataGrid.Items.Add(new MyData {
                        Qt = ColumnAList[i],
                        L = ColumnBList[i],
                        Mqc = ColumnCList[i],
                        Cp = ColumnDList[i],
                        Tcp = ColumnEList[i],
                        Im = ColumnFList[i]
                    });
                }
            }

            #endregion
        }

        #region Methods

        private void DeleteFirstRow_OnClick(object sender, RoutedEventArgs e) {
            MyDataGrid.Items.RemoveAt(1);
            deletedFirstRows++;
        }

        private void SaveToFile_OnClick(object sender, RoutedEventArgs e) {
            bool? result = sfd.ShowDialog();
            if (result == true) {
                string path = sfd.FileName;
                StreamWriter sw = new StreamWriter(File.Create(path));
                sw.WriteAsync(deletedFirstRows.ToString());
                sw.Dispose();
            }
        }

        private void LoadFile_OnClick(object sender, RoutedEventArgs e) {
            if (ofd.ShowDialog() == true) {
                string path = ofd.FileName;
                using (StreamReader sr = new StreamReader(File.OpenRead(path))) {

                    MyDataGrid.Items.Clear();

                    if (Values != null) {
                        for (int a = 0; a < Values.Count; a++) {
                            MyDataGrid.Items.Add(new MyData {
                                Qt = ColumnAList[a],
                                L = ColumnBList[a],
                                Mqc = ColumnCList[a],
                                Cp = ColumnDList[a],
                                Tcp = ColumnEList[a],
                                Im = ColumnFList[a]
                            });
                        }
                    }
                }
            }
        }

        private void collectData () {
            WebClient wc = new WebClient();
            var s = wc.DownloadString(Environment.NewLine + "http://services.runescape.com/m=hiscore/index_lite.ws?player=" + sender);
            
            for (int i = 0; i < SkillNames.Length; i++) {
                var skills = s.Split('\n');
                var categories = skills[i].Split(',');
                var skill = new Tuple<int, int>(Convert.ToInt32(categories[1]), Convert.ToInt32(categories[2]));
                var result = "The " + SkillNames[i] + " level of the user is: " + skill.Item1 + "." + Environment.NewLine +
                         "The " + SkillNames[i] + " experience of the user is: " + skill.Item2 + ".";
                MessageBox.Show(result);
            }
        }
        #endregion
    }
}