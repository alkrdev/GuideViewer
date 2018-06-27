using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using static Guideviewer.Progress;

namespace Guideviewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json

        #region Initialize

        public bool HasLoaded;

        public static User User = new User();

        public Progress Pr = new Progress();
        
        public string UrlUserName {
            get => Box.Text;
            set => Box.Text = value.Replace(' ', '_');
        }

        //Parameters to handle GoogleRequest
        public IList<IList<object>> Values;

        //The maximum amount of rows in the datasource spreadsheet
        public static int Limiter = new GoogleRequest().GoogleRequestInit().Execute().Values.Count;

        public static string[] ColumnA = new string[Limiter];
        public static string[] ColumnB = new string[Limiter];
        public static string[] ColumnC = new string[Limiter];
        public static string[] ColumnD = new string[Limiter];
        public static string[] ColumnE = new string[Limiter];
        public static string[] ColumnF = new string[Limiter];

        public static List<string[]> ColumnList = new List<string[]> {
            ColumnA, ColumnB, ColumnC, ColumnD, ColumnE, ColumnF
        };
        //Struct to insert data from datasource in correct columns
        public struct MyData {
            public string Qt { set; get; }
            public string L { set; get; }
            public string Mqc { set; get; }
            public string Cc { set; get; }
            public string Tcc { set; get; }
            public string Im { set; get; }
        }

        #endregion

        public MainWindow() {
            InitializeComponent();

            FirstLoad();
            for (int i = 0; i < User.LoadedSkillLevels.Length; i++) {
                User.LoadedSkillLevels[i] = 1;
                User.LoadedSkillExperiences[i] = 0;
            }
            
        }

        #region Methods

        private void LoadOnline_OnClick (object sender, RoutedEventArgs e) {
            if (!HasLoaded) {
                HasLoaded = true;
                try {
                    User.Load(
                        new WebClient().DownloadString("https://apps.runescape.com/runemetrics/quests?user=" + UrlUserName), //UserQuestData
                        new WebClient().DownloadString("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + UrlUserName).Split('\n'), //UserSkillData
                        User, true);
                }
                catch (Exception d) {
                    MessageBox.Show(
                        $"The username is either wrong, the user has set their profile to private. If the username is correct, contact a developer. \n\n Error: {d}");
                }
                finally {
                    MessageBox.Show("User was successfully loaded, please \"Reload\"");
                }
                    StreamWriter sw = new StreamWriter($"{UrlUserName}.txt");
                    Pr.Save(new WebClient().DownloadString("https://apps.runescape.com/runemetrics/quests?user=" + UrlUserName)/*.Replace("},{", "}\n{")*/, UrlUserName, User, sw);
            }
            else if (HasLoaded) {
                MessageBox.Show("Please use the Reset function before loading an accounts progress again");
            }
        }

        //Fill all of the columns
        private void FillAllColumns() {
            if (Values != null) {
                for (int a = 0; a < Values.Count; a++) {
                    MyDataGrid.Items.Add(new MyData {
                        Qt = ColumnList[0][a],
                        L = ColumnList[1][a],
                        Mqc = ColumnList[2][a],
                        Cc = ColumnList[3][a],
                        Tcc = ColumnList[4][a],
                        Im = ColumnList[5][a]
                    });
                }
            }
        }

        private void FirstLoad() {
            //Google Request
            Values = new GoogleRequest().GoogleRequestInit().Execute().Values;

            //Insert the requested data into column arrays
            if (Values != null && Values.Count > 0) {
                for (var index = 0; index < Values.Count; index++) {
                    if (Values[index][0] != null) {
                        ColumnList[0][index] = Values[index][0].ToString();
                    }

                    if (Values[index][1] != null) {
                        ColumnList[1][index] = Values[index][1].ToString();
                    }

                    if (Values[index][2] != null) {
                        ColumnList[2][index] = Values[index][2].ToString();
                    }

                    if (Values[index][3] != null) {
                        ColumnList[3][index] = Values[index][3].ToString();
                    }

                    if (Values[index][4] != null) {
                        ColumnList[4][index] = Values[index][4].ToString();
                    }

                    if (Values[index][5] != null) {
                        ColumnList[5][index] = Values[index][5].ToString();
                    }
                }
            }
            FillAllColumns();
        }

        private void DeleteEmptyRows(object sender, RoutedEventArgs routedEventArgs) {
            
            for (int m = ColumnList[0].Length - 1; m >= 0; m--) {
                if (ColumnList[0][m] == ColumnList[1][m] && 
                    ColumnList[1][m] == ColumnList[2][m] && 
                    ColumnList[2][m] == ColumnList[3][m] && 
                    ColumnList[3][m] == ColumnList[4][m] && 
                    ColumnList[4][m] == ColumnList[5][m]) {
                                    
                    MyDataGrid.Items.RemoveAt(m);
                }
            }
        }

        private void Op_OnClick(object sender, RoutedEventArgs e) {
            new Options().Show();
        }

        private void LoadFile_OnClick(object sender, RoutedEventArgs e) {
            try {
                Load(User);
                HasLoaded = false;

            }
            catch (Exception exception) {
                MessageBox.Show("Loading user failed: \n" + exception);
                throw;
            }
            finally {
                MessageBox.Show("User was successfully loaded, please \"Reload\"");
            }
        }

        private void Reset(object sender, RoutedEventArgs routedEventArgs) {
            HasLoaded = false;
            MyDataGrid.Items.Clear();
            MessageBox.Show("ALL ITEMS WERE CLEARED");

            FirstLoad();
        }

        private void Reload(object sender, RoutedEventArgs e) {
            MyDataGrid.Items.Clear();
            FillAllColumns();
        }

        #endregion
    }
}