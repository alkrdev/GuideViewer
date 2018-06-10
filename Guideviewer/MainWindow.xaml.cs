using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Win32;

namespace Guideviewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json

        #region Initialize

        public bool HasLoaded;

        public static User U = new User();

        //Parameters to handle GoogleRequest
        public IList<IList<object>> Values;
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

            FirstLoad();

        }

        #region Methods

        private void LoadProgress_OnClick (object sender, RoutedEventArgs e) {
            if (!HasLoaded) {
                HasLoaded = true;
                try {
                    string data = new WebClient().DownloadString(
                        "https://apps.runescape.com/runemetrics/quests?user=" + Box.Text.Replace(' ', '_'));

                    Quests response = Quests.FromJson(data);

                    //Download Userdata
                    var skills = new WebClient()
                        .DownloadString("http://services.runescape.com/m=hiscore/index_lite.ws?player=" +
                                        Box.Text.Replace(' ', '_')).Split('\n');
                    
                    //Loop through the amount of skills
                    for (int i = 1; i < User.SkillNames.Count; i++) {

                        //Extract Userdata and seperate by ","
                        var categories = skills[i].Split(',');
                        
                        //Insert Userdata into arrays for storage
                        U.LoadedSkillLevels[i] = Convert.ToInt32(categories[1]);
                        U.LoadedSkillExperiences[i] = Convert.ToInt32(categories[2]);

                        U.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], U.LoadedSkillLevels[i],
                            U.LoadedSkillExperiences[i]);

                        //Loop through list of current data
                        for (var index = 1; index < ColumnA.Length; index++) {
                            //If any of the looped through data contains "[Train" - Very specific
                            if (ColumnA[index].StartsWith("[Train")) {
                                //Creates a string that can look like: "Attack to " - With specification on spaces
                                string combined = User.SkillNames[i] + " to ";

                                if (ColumnA[index].Contains(combined)) {

                                    //Create a new string that starts with "Attack to ", and has a problematic number added to it - Example: "Attack to 96]"
                                    string extract = combined + ColumnA[index]
                                                         .Substring(
                                                             ColumnA[index].IndexOf(combined, 3,
                                                                 StringComparison.Ordinal) + combined.Length, 3);

                                    if (extract.EndsWith("]")) {
                                        extract = extract.Remove(extract.LastIndexOf(']'),
                                            1); //Remove the problematic symbol "]" - End of string
                                    }
                                    else if (extract.EndsWith(",")) {
                                        extract = extract.Remove(extract.LastIndexOf(','),
                                            1); //Remove the problematic symbol "," - Before comma
                                    }
                                    else if (extract.EndsWith(" ")) {
                                        extract = extract.Remove(extract.LastIndexOf(' '),
                                            1); //Remove the problematic symbol " " - Before space
                                    }
                                    else if (extract.EndsWith("a")) {
                                        extract = extract.Remove(extract.LastIndexOf('a'),
                                            1); //Remove the problematic symbol "a" - Before and
                                    }

                                    //If the userdatas level is bigger than what I am expecting, do the following:
                                    if (Convert.ToInt32(extract
                                            .Substring(extract.IndexOf(combined, 1, StringComparison.Ordinal) +
                                                       combined.Length).Replace(" ", "")) <= U.Levels[i].Item2) {
                                        ColumnA[index] = ColumnA[index].Replace(extract, "");
                                    }

                                    foreach (var t in response.QuestsList) {
                                        for (int j = 0; j < ColumnA.Length; j++) {

                                            if (t.Title == ColumnA[j] && t.Status == Status.Completed) {

                                                SpecificRemover("Scorpion Catcher", "Barcrawl Miniquest", t);
                                                SpecificRemover("Nomad's Requiem", "Soul Wars Tutorial", t);
                                                SpecificRemover("Children of Mah", "Koschei's Troubles miniquest", t);
                                                SpecificRemover("While Guthix Sleeps",
                                                    "Chaos Tunnels: Hunt for Surok miniquest", t);
                                                SpecificRemover("Crocodile Tears", "Tier 3 Menaphos City Reputation",
                                                    t);
                                                SpecificRemover("Our Man in the North",
                                                    "Tier 6 Menaphos City Reputation", t);
                                                SpecificRemover("'Phite Club", "Tier 9 Menaphos City Reputation", t);

                                                ColumnA[j] = ColumnA[j].Remove(0);
                                                if (ColumnB[j] != "") {
                                                    ColumnB[j] = ColumnB[j].Remove(0);
                                                }
                                            }
                                        }
                                    }

                                    //Remove instances of example: "Attack to ," - Redundant
                                    if (ColumnA[index].Contains(" ,")) {
                                        ColumnA[index] = ColumnA[index].Replace(" ,", "");
                                    }


                                    //Cleanup Switch-statement
                                    switch (ColumnA[index]) {
                                        case "[Train ]":

                                        case "[Train and ]":

                                        case "[Train  and ]":

                                        case "[Train ,  and ]":

                                        case "[Train , ,  and ]":

                                        case "[Train , , ,  and ]":

                                        case "[Train , , , ,  and ]":

                                        case "[Train , , , , ,  and ]":

                                        case "[Train ,  and  [OPTIONAL]]":

                                        case "[Train  and  [OPTIONAL]]":
                                            ColumnA[index] = ColumnA[index].Replace(ColumnA[index], "");
                                            break;
                                    }

                                    if (ColumnA[index].EndsWith(" and ]")) {
                                        ColumnA[index] = ColumnA[index].Replace(" and ]", "]");
                                    }

                                    for (var index1 = 0; index1 < ColumnA.Length; index1++) {
                                        if (ColumnA[index1] == " " && ColumnA[index1] != "") {
                                            ColumnA[index1] = ColumnA[index1].Remove(0);
                                        }

                                        if (ColumnB[index1] == " " && ColumnB[index1] != "") {
                                            ColumnB[index1] = ColumnB[index1].Remove(0);
                                        }

                                        if (ColumnC[index1] == " " && ColumnC[index1] != "") {
                                            ColumnC[index1] = ColumnC[index1].Remove(0);
                                        }

                                        if (ColumnD[index1] == " " && ColumnD[index1] != "") {
                                            ColumnD[index1] = ColumnD[index1].Remove(0);
                                        }

                                        if (ColumnE[index1] == " " && ColumnE[index1] != "") {
                                            ColumnE[index1] = ColumnE[index1].Remove(0);
                                        }

                                        if (ColumnF[index1] == " " && ColumnF[index1] != "") {
                                            ColumnF[index1] = ColumnF[index1].Remove(0);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Clear everything in the DataGrid
                    MyDataGrid.Items.Clear();
                    //Use the filling method, read below
                    FillAllColumns();
                }
                catch (Exception d) {
                    MessageBox.Show(
                        $"The username is either wrong, the user has set their profile to private. If the username is correct, contact a developer. \n\n Error: {d}");

                }
            }
            else if (HasLoaded) {
                MessageBox.Show("Please use the Reload function before loading an accounts progress again");
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

        private void ReloadSpreadsheetData(object sender, RoutedEventArgs routedEventArgs) {
            HasLoaded = false;
            MyDataGrid.Items.Clear();
            MessageBox.Show("ALL ITEMS WERE CLEARED");

            FirstLoad();
        }

        private void FirstLoad() {
            //Google Request
            Values = new GoogleRequest().GoogleRequestInit().Execute().Values;

            //Insert the requested data into column arrays
            if (Values != null && Values.Count > 0) {
                for (var index = 0; index < Values.Count; index++) {
                    if (Values[index][0] != null) {
                        ColumnA[index] = Values[index][0].ToString();
                    }

                    if (Values[index][1] != null) {
                        ColumnB[index] = Values[index][1].ToString();
                    }

                    if (Values[index][2] != null) {
                        ColumnC[index] = Values[index][2].ToString();
                    }

                    if (Values[index][3] != null) {
                        ColumnD[index] = Values[index][3].ToString();
                    }

                    if (Values[index][4] != null) {
                        ColumnE[index] = Values[index][4].ToString();
                    }

                    if (Values[index][5] != null) {
                        ColumnF[index] = Values[index][5].ToString();
                    }
                }
            }
            FillAllColumns();
            }

        private void SpecificRemover(string main, string second, Quest t) {
            if (t.Title == main && t.Status == Status.Completed) {
                for (int h = 0; h < ColumnA.Length; h++) {
                    if (ColumnA[h] == second) {
                        ColumnA[h] = ColumnA[h].Remove(0);
                        if (ColumnB[h] != "") {
                            ColumnB[h] = ColumnB[h].Remove(0);
                        }
                        
                    }
                }
            }
        }

        private void DeleteEmptyRows(object sender, RoutedEventArgs routedEventArgs) {
            
            for (int m = ColumnA.Length - 1; m >= 0; m--) {
                if (ColumnA[m] == ColumnB[m] && ColumnB[m] == ColumnC[m] && 
                    ColumnC[m] == ColumnD[m] && ColumnD[m] == ColumnE[m] && 
                    ColumnE[m] == ColumnF[m]) {
                                    
                    MyDataGrid.Items.RemoveAt(m);
                }
            }
        }
        #endregion

        private void Op_OnClick(object sender, RoutedEventArgs e) {
            Options options = new Options();
            options.Show();
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Delete) {
                DataGridCell cell = e.OriginalSource as DataGridCell;

                if (cell == null) {
                    return;
                }

                if (!cell.IsReadOnly && cell.IsEnabled) {
                    TextBlock tb = cell.Content as TextBlock;

                    if (tb != null) {
                        Binding binding = BindingOperations.GetBinding(tb, TextBlock.TextProperty);

                        if (binding == null) {
                            return;
                        }

                        BindingExpression exp = BindingOperations.GetBindingExpression(tb, TextBlock.TextProperty);

                        if (exp != null) {
                            PropertyInfo info = exp.DataItem.GetType().GetProperty(binding.Path.Path);

                            if (info == null) {
                                return;
                            }

                            info.SetValue(exp.DataItem, null, null);
                        }
                    }
                }
            }
        }
    }
}