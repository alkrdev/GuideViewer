using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Guideviewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json

        #region Initialize
        
        public static User u = new User();

        //???
        public string Qt { get; set; }
        
        //Column measurements
        private const float Def = 325;
        private const float Med = 245;
        private const float Sm = 80;

        //Parameters to handle GoogleRequest
        public IList<IList<Object>> Values;
        public ValueRange Response;

        //The maximum amount of rows in the datasource spreadsheet
        public static int Limiter = new GoogleRequest().GoogleRequestInit().Execute().Values.Count;

        //Arrays to store the data
        public string[] ColumnA = new string[Limiter];
        public string[] ColumnB = new string[Limiter];
        public string[] ColumnC = new string[Limiter];
        public string[] ColumnD = new string[Limiter];
        public string[] ColumnE = new string[Limiter];
        public string[] ColumnF = new string[Limiter];

        //SaveFileDialog Class initialization - Save a file
        public SaveFileDialog Sfd = new SaveFileDialog {
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        //OpenFileDialog Class initialization - Open a file
        public OpenFileDialog Ofd = new OpenFileDialog {
            Filter = "txt files (*.txt)|*.txt|" +
                     "All files (*.*)|*.*"
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

            //Google Request
            Values = new GoogleRequest().GoogleRequestInit().Execute().Values;

            //Insert the requested data into column arrays
            if (Values != null && Values.Count > 0) {
                for (var index = 0; index < Values.Count; index++) {
                    var col = Values[index];
                    if (col[0] != null) {
                        ColumnA[index] = col[0].ToString();
                    }

                    if (col[1] != null) {
                        ColumnB[index] = col[1].ToString();
                    }

                    if (col[2] != null) {
                        ColumnC[index] = col[2].ToString();
                    }

                    if (col[3] != null) {
                        ColumnD[index] = col[3].ToString();
                    }

                    if (col[4] != null) {
                        ColumnE[index] = col[4].ToString();
                    }

                    if (col[5] != null) {
                        ColumnF[index] = col[5].ToString();
                    }
                }
            }

            //Use the filling method, read below
            FillAllColumns();

        }

        #region Methods

        private void LoadProgress_OnClick (object sender, RoutedEventArgs e) {
            
            try {
                IList<Quests> quests  = new List<Quests>();
                JsonTextReader Jr = new JsonTextReader(new StringReader(new WebClient().DownloadString("https://apps.runescape.com/runemetrics/quests?user=" + Box.Text.Replace(' ', '_')))) {
                    SupportMultipleContent = true
                };

                while (true) {
                    if (!Jr.Read()) {
                        break;
                    }

                    JsonSerializer s = new JsonSerializer();
                    Quests quest = s.Deserialize<Quests>(Jr);

                    quests.Add(quest);
                }

                foreach (Quests quest in quests) {
                    MessageBox.Show(quest.Title);
                }
                
                //Download Userdata
                var skills = new WebClient().DownloadString("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + Box.Text.Replace(' ', '_')).Split('\n');

                //Loop through the amount of skills
                for (int i = 1; i < User.SkillNames.Count; i++) {

                    //Extract Userdata and seperate by ","
                    var categories = skills[i].Split(',');

                    //Insert Userdata into arrays for storage
                    u.LoadedSkillLevels[i] = Convert.ToInt32(categories[1]);
                    u.LoadedSkillExperiences[i] = Convert.ToInt32(categories[2]);

                    u.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i],u.LoadedSkillLevels[i],u.LoadedSkillExperiences[i]);

                    //Loop through list of current data
                    for (var index = 1; index < ColumnA.Length; index++) {
                        //If any of the looped through data contains "[Train" - Very specific
                        if (ColumnA[index].Contains("[Train")) {
                            //Creates a string that can look like: "Attack to " - With specification on spaces
                            string combined = User.SkillNames[i] + " to ";

                            //If the current data contains the previously created string
                            if (ColumnA[index].Contains(combined)) {

                                //Remove instances of example: "Attack to ," - Redundant
                                if (ColumnA[index].Contains(" ,")) {
                                    ColumnA[index] = ColumnA[index].Replace(" ,", "");
                                }

                                //Create a new string that starts with "Attack to ", and has a problematic number added to it - Example: "Attack to 96]"
                                string extract = combined + ColumnA[index].Substring(ColumnA[index].IndexOf(combined, 3, StringComparison.Ordinal) + combined.Length, 3);

                                if (extract.EndsWith("]")) {
                                    extract = extract.Remove(extract.LastIndexOf(']'), 1); //Remove the problematic symbol "]" - End of string
                                } else if (extract.EndsWith(",")) {
                                    extract = extract.Remove(extract.LastIndexOf(','), 1); //Remove the problematic symbol "," - Before comma
                                } else if (extract.EndsWith(" ")) {
                                    extract = extract.Remove(extract.LastIndexOf(' '), 1); //Remove the problematic symbol " " - Before space
                                } else if (extract.EndsWith("a")) {
                                    extract = extract.Remove(extract.LastIndexOf('a'), 1); //Remove the problematic symbol " " - Before space
                                }

                                //MessageBox.Show("extract: " + extract);
                                //MessageBox.Show("extract.Substring: " + extract.Substring(extract.IndexOf(combined, 1, StringComparison.Ordinal) + combined.Length).Replace(" ", ""));
                                //MessageBox.Show("userlevel: " + u.Levels[i].Item2);
                                //If the userdatas level is bigger than what I am expecting, do the following:
                                if (Convert.ToInt32(extract.Substring(extract.IndexOf(combined, 1, StringComparison.Ordinal) + combined.Length).Replace(" ", "")) <= u.Levels[i].Item2) {
                                    //MessageBox.Show(extract.Substring(extract.IndexOf(combined, 1, StringComparison.Ordinal) + combined.Length).Replace(" ", "") + " is less than the users level: " + u.Levels[i].Item2);
                                    ColumnA[index] = ColumnA[index].Replace(extract, "");
                                    //MessageBox.Show(ColumnA[index]);
                                }
                            }



                            //Cleanup Switch-statement
                            switch (ColumnA[index]) {
                                case "[Train ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train and ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train  and ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train ,  and ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train , ,  and ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train , , ,  and ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train , , , ,  and ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train , , , , ,  and ]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train ,  and  [OPTIONAL]]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                                case "[Train  and  [OPTIONAL]]":
                                    ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                    break;
                            }
                        }
                    }
                }

                //Clear everything in the DataGrid
                MyDataGrid.Items.Clear();
                MessageBox.Show("ALL ITEMS WERE CLEARED");
                //Use the filling method, read below
                FillAllColumns();
            }
            catch (Exception d) {
                MessageBox.Show($"{d}");
               
            }
        }

        //Fill all of the columns
        private void FillAllColumns() {
            if (Values != null) {
                for (int a = 0; a < Values.Count; a++) {
                    MyDataGrid.Items.Add(new MyData {
                        Qt = ColumnA[a],
                        L = ColumnB[a],
                        Mqc = ColumnC[a],
                        Cc = ColumnD[a],
                        Tcc = ColumnE[a],
                        Im = ColumnF[a]
                    });
                }
            }
        }
        #endregion
    }
}