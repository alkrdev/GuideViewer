using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Guideviewer;
using Library;
using Microsoft.Win32;

namespace GuideViewer
{
    public partial class MainWindow : Window
    {
        private bool _hasLoaded;
        private static bool _hasApplied;

        private string UrlUserName
        {
            get => UrlUsername.Text;
            set => UrlUsername.Text = value.Replace(' ', '_');
        }

        private string ApplyUserName
        {
            get => ApplyUsername.Text;
            set => ApplyUsername.Text = value.Replace(' ', '_');
        }

        private static List<string[]> _columnList = new List<string[]>();



        //Parameters to handle GoogleRequest
        private IList<IList<object>> _values;

        public static Data data;

        //Struct to insert data from datasource in correct columns
        struct DataGridInfo
        {
            public string Qt { set; get; }
            public string L { set; get; }
            public string Mqc { set; get; }
            public string Cc { set; get; }
            public string Tcc { set; get; }
        }

        public MainWindow()
        {
            InitializeComponent();

            data = new Data();

            //SearchAndIdentify();
            AddColumns();
            HandleData();
        }

        private void HandleData()
        {
            MakeGoogleRequest();
            InsertRequestedData();
            FillAllColumns();
            SetDefaultUserData();
        }

        private void SearchAndIdentify()
        {
            SearchTreeHierarchy(SearchCriteria.ListView);
            IdentifySelectAllCheckboxes();
            //IdentifyDuplicateCheckboxes();
        }

        private void IdentifySelectAllCheckboxes()
        {
            for (int i = 0; i < data.SelectAllCheckBoxes.Count; i++)
            {
                data.ListViewSelectAllList.Add(new Tuple<CheckBox, ListView>(data.SelectAllCheckBoxes[i], data.ListViews[i]));
            }
        }
        private void AddColumns()
        {
            int count = new GoogleRequest().GoogleRequestInit().Execute().Values.Count;
            for (int i = 0; i < 6; i++)
            {
                _columnList.Add(new string[count]);
            }
        }        

        private void LoadOnline_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string runemetrics = new WebClient().DownloadString("https://apps.runescape.com/runemetrics/quests?user=" + UrlUserName);
                string hiscore = new WebClient().DownloadString("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + UrlUserName);
                LoadUser(runemetrics, hiscore.Split('\n'), true);
                SaveText(runemetrics, UrlUserName, new StreamWriter($"{UrlUserName}.txt"), User.DefaultIntArrayString);
            }
            catch (Exception d)
            {
                MessageBox.Show($"The username is either wrong or the user has set their profile to private. If the username is correct, contact a developer. \n\n Error: {d}");
            }
            finally
            {
                MessageBox.Show("User was successfully loaded, please \"Reload\"");
            }

            if (_hasLoaded)
            {
                MessageBox.Show("Please use the Reset function before loading an accounts progress again");
            }

            _hasLoaded = true;
        }

        private void DeleteEmptyRows(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = _columnList[0].Length - 1; i >= 0; i--)
                {
                    if (new[]
                    {
                        _columnList[0][i], _columnList[1][i], _columnList[2][i],
                        _columnList[3][i], _columnList[4][i], _columnList[5][i]
                    }.All(string.IsNullOrWhiteSpace))
                    {
                        MyDataGrid.Items.RemoveAt(i);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please reload before deleting rows");
            }
        }

        private void LoadFile_OnClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {

                var userskilldata = new string[User.SkillNames.Count];

                for (int i = 1; i < User.SkillNames.Count - 1; i++)
                {
                    userskilldata[i] = File.ReadLines(ofd.FileName).Skip(2 + i).Take(1).First();
                }

                for (int i = 1; i < User.Levels.Length; i++)
                {
                    string v = File.ReadLines(ofd.FileName).Skip(i + 2).Take(1).First();
                    User.LoadedSkillLevels[i] = Convert.ToInt32(v.Substring(v.IndexOf(": ", 3, StringComparison.Ordinal) + 2));
                    User.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], User.LoadedSkillLevels[i], User.LoadedSkillExperiences[i]);
                }

                LoadUser(File.ReadLines(ofd.FileName).Skip(33).Take(1).First(), userskilldata, false);
            }
            _hasLoaded = false;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            try
            {
                _hasLoaded = false;
                MyDataGrid.Items.Clear();

                MakeGoogleRequest();
                InsertRequestedData();

                FillAllColumns();
                SetDefaultUserData();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
                throw;
            }
            finally
            {
                MessageBox.Show("ALL ITEMS WERE CLEARED");
            }
        }

        private void Reload(object sender, RoutedEventArgs e)
        {
            if (_hasApplied || _hasLoaded)
            {
                if (_hasApplied)
                {
                    _hasApplied = false;
                }
                else
                {
                    _hasLoaded = false;
                }

                RemoveByCheckboxes();
            }

            MyDataGrid.Items.Clear();
            FillAllColumns();
        }

        private void Check(object sender, RoutedEventArgs e) => Switch(sender, true);

        private void UnCheck(object sender, RoutedEventArgs e) => Switch(sender, false);

        private void Switch(object sender, bool boolean)
        {
            if (!(sender is CheckBox senderBox)) return;
            // Main Duplicate Control
            if (data.CheckboxesDictionary.TryGetValue(senderBox.Name, out var value))
            {
                foreach (var x in value) x.IsChecked = boolean;
            }

            // Select All Control
            if (!senderBox.Name.StartsWith("Sa")) return;
            foreach (var listViewChild in LogicalTreeHelper.GetChildren(LogicalTreeHelper.GetParent(senderBox)))
            {
                if (listViewChild is CheckBox cb)
                {
                    cb.IsChecked = boolean;
                }
            }
        }

        private string CheckboxStringSave()
        {
            data.CheckboxesBoolDictionary.Clear();
            string str = "";

            data.AllCheckBoxes.ForEach(cb =>
            {
                var isChecked = cb.IsChecked;
                if (isChecked != null && cb.IsChecked == isChecked)
                {
                    data.CheckboxesBoolDictionary.Add(cb.Name, (bool)isChecked);
                    str += "1,";
                }
                else
                {
                    data.CheckboxesBoolDictionary.Add(cb.Name, isChecked != null && (bool)isChecked);
                    str += "0,";
                }
            });

            return str;
        }

        private void OnApplyOptions(object sender, RoutedEventArgs e)
        {
            _hasApplied = true;

            SaveText(new WebClient().DownloadString("https://apps.runescape.com/runemetrics/quests?user=" + ApplyUserName),
                     ApplyUserName,
                     new StreamWriter($"{ApplyUserName}.txt"),
                     CheckboxStringSave());
        }

        private void OnOpenLoad(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                string fileFetch = File.ReadLines(ofd.FileName).Skip(31).Take(1).First();
                
                Array.ConvertAll(fileFetch.Remove(fileFetch.LastIndexOf(','))
                 .Split(','), int.Parse).ToList().ForEach(checkBoxValue =>
                 {
                     data.AllCheckBoxes.ForEach(cb => cb.IsChecked = checkBoxValue == 1);
                 });
            }

            SearchTreeHierarchy(SearchCriteria.CheckBox);
            ApplyUsername.Text = ofd.SafeFileName.Replace(".txt", "");
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string url = e.Uri.OriginalString;

            Uri fullUri = new Uri(new Uri("https//runescape.wiki"), 
                new Uri(url.Substring(url.IndexOf('/')), UriKind.Relative));
            
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void MqcMiniquestsData_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBoxViewModel cbvm = new CheckBoxViewModel();
            cbvm.LoadCheckBoxes();

            MqcMiniquestsData.DataContext = cbvm;
        }
        private void MakeGoogleRequest()
        {
            //Google Request
            _values = new GoogleRequest().GoogleRequestInit().Execute().Values;
        }
        private void FillAllColumns()
        {
            if (_values == null) return;
            for (int a = 0; a < _values.Count; a++)
            {
                MyDataGrid.Items.Add(new DataGridInfo
                {
                    Qt = _columnList[0][a],
                    L = _columnList[1][a],
                    Mqc = _columnList[2][a],
                    Cc = _columnList[3][a],
                    Tcc = _columnList[4][a]
                });
            }
        }

        private static void SaveText(string userQuestData, string username, StreamWriter sw, string checkboxStringSave)
        {
            using (sw)
            {
                sw.WriteLine("Username: " + username.Replace(' ', '_') + "\n");
                sw.WriteLine(" ");

                for (var index = 1; index < User.Levels.Length; index++)
                {
                    sw.WriteLine(User.Levels[index].Item1 + " level: " + User.Levels[index].Item2);
                }

                sw.WriteLine(" ");
                sw.WriteLine(File.Exists($"{username}.txt") ? checkboxStringSave : User.DefaultIntArrayString);

                sw.WriteLine(" ");
                sw.WriteLine(userQuestData);
            }
        }

        private void LoadUser(string userquestData, IReadOnlyList<string> userskillData, bool online)
        {
            User.SkillsDictionary.Clear();
            for (int i = 1; i < User.SkillNames.Count; i++)
            {
                User.LoadedSkillLevels[i] = Convert.ToInt32(userskillData[i].Split(',')[1]);
                User.SkillsDictionary.Add(User.SkillNames[i], User.LoadedSkillLevels[i]);
            }

            for (int i = 0; i < _columnList[0].Length; i++)
            {
                string text = _columnList[0][i];
                // If the current value starts with "[Train"
                if (text.StartsWith("[Train"))
                {
                    // Then loop through all known skills in the game
                    User.SkillNames.ForEach(skill =>
                    {
                        // If the users level is higher than our focuslevel
                        if (skill != "Total" && text.Contains(skill) &&
                            User.SkillsDictionary.TryGetValue(skill, out int value) && Convert.ToInt32(
                                text.Substring(("[Train " + skill + " to ").Length)
                                    .Replace("]", "").Replace("[OPTIONAL", "")) <= value)
                        {
                            // Remove the value from the Datagrid
                            _columnList[0][i] = text.Remove(0);
                        }
                    });
                }
            }

            Quests.FromJson(userquestData).QuestsList.ForEach(quest =>
            {
                // Loop through the length of our first column
                for (int j = 0; j < _columnList[0].Length; j++)
                {
                    // If the title of the current quest, and the current value are the same - And the quest has been completed
                    if (quest.Title != _columnList[0][j] || quest.Status != Status.Completed) continue;
                    // Remove the value from the Datagrid
                    _columnList[0][j] = _columnList[0][j].Remove(0);

                    // If there is a value in the column to the right
                    if (_columnList[1][j] != "")
                    {
                        // Remove the value from the Datagrid
                        _columnList[1][j] = _columnList[1][j].Remove(0);
                    }

                    _columnList.ForEach(col =>
                    {
                        // Loop through the length of our Column
                        for (var i = 0; i < col.Length; i++)
                        {
                            // If the value is a space, and the value is not nothing
                            if (col[i] == " " && col[i] != "")
                            {
                                // Remove the value from the Datagrid
                                col[i] = col[i].Remove(0);
                            }
                        }
                    });

                    foreach (var pre in User.PrerequisiteTuples)
                    {
                        if (quest.Title != pre.Item1 || quest.Status != Status.Completed) continue;
                        for (int h = 0; h < _columnList[0].Length; h++)
                        {
                            if (_columnList[0][h] != pre.Item2) continue;
                            _columnList[0][h] = _columnList[0][h].Remove(0);

                            if (_columnList[1][h] != "")
                            {
                                _columnList[1][h] = _columnList[1][h].Remove(0);
                            }
                        }
                    }
                }
            });
        }

        private void RemoveByCheckboxes()
        {
            data.CheckboxesBoolDictionary.Clear();

            foreach (var cb in data.AllCheckBoxes)
            {
                if (!(cb.IsChecked is true))
                {
                    if (cb.IsChecked is false) data.CheckboxesBoolDictionary.Add(cb.Name, false);
                }
                else
                    data.CheckboxesBoolDictionary.Add(cb.Name, true);

                if (!data.CheckboxesBoolDictionary.TryGetValue(cb.Name, out bool isTrue)) continue;
                for (int j = _columnList.Count - 1; j >= 2; j--)
                {
                    for (int k = _columnList[0].Length - 1; k >= 0; k--)
                    {
                        if (string.Equals(data.AllCheckBoxes[j].Name.ToLower(), data.NameCompareTuples[j].Item1.ToLower(),
                                StringComparison.Ordinal) && isTrue && _columnList[j][k].Contains(data.NameCompareTuples[j].Item2))
                        {
                            _columnList[j][k] = _columnList[j][k].Remove(0);
                        }
                    }
                }
            }
        }//NEEDS REDUX

        private void SetDefaultUserData()
        {
            for (int i = 0; i < User.LoadedSkillLevels.Length; i++)
            {
                if (i == 4)
                {
                    User.LoadedSkillLevels[i] = 10;
                    User.LoadedSkillExperiences[i] = 1154;
                }
                else
                {
                    User.LoadedSkillLevels[i] = 1;
                    User.LoadedSkillExperiences[i] = 0;
                }
                User.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], User.LoadedSkillLevels[i], User.LoadedSkillExperiences[i]);
            }
        }

        private void InsertRequestedData()
        {
            //Insert the requested data into column arrays
            if (_values != null && _values.Count > 0)
            {
                for (var j = 0; j < _values.Count; j++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _columnList[i][j] = _values[j][i].ToString();
                    }
                }
            }
        }

        private void SearchTreeHierarchy(SearchCriteria crit)
        {
            foreach (var tabcontrolItem in MainTabControl.Items)
            {
                if (!(tabcontrolItem is TabItem tabitem)) continue;
                foreach (var child in LogicalTreeHelper.GetChildren(tabitem))
                {
                    if (!(child is Grid grid)) continue;
                    foreach (var gridChild in grid.Children)
                    {
                        if (!(gridChild is TabControl tabcontrol)) continue;
                        foreach (var tabcontrolItem2 in tabcontrol.Items)
                        {
                            if (!(tabcontrolItem2 is TabItem tabitem2)) continue;
                            foreach (var child2 in LogicalTreeHelper.GetChildren(tabitem2))
                            {
                                if (!(child2 is Grid grid2)) continue;
                                foreach (var grid2Child in grid2.Children)
                                {
                                    if (!(grid2Child is ListView listview)) continue;
                                    if (crit == SearchCriteria.CheckBox)
                                    {
                                        if (new[] { "Mq", "Sa", "Co", "Tri" }.Any(p => listview.Name.StartsWith(p)))
                                        {
                                            data.ListViews.Add(listview);
                                        }

                                        foreach (var checkbox in listview.Items)
                                        {
                                            if (!(checkbox is CheckBox cb)) continue;
                                            if (cb.Name.StartsWith("Sa"))
                                            {
                                                data.SelectAllCheckBoxes.Add(cb);
                                            }
                                            else
                                            {
                                                data.AllCheckBoxes.Add(cb);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var availableCheckBoxes = new List<CheckBox>();
                                        foreach (var checkbox in listview.Items)
                                        {
                                            if (checkbox is CheckBox cb && !cb.Name.StartsWith("Sa"))
                                            {
                                                availableCheckBoxes.Add(cb);
                                            }
                                        }
                                        foreach (var listviewItem in listview.Items)
                                        {
                                            if (listviewItem is CheckBox cb && cb.Name.StartsWith("Sa"))
                                            {
                                                if (availableCheckBoxes.All(box => box.IsChecked == true))
                                                {
                                                    cb.IsChecked = true;
                                                }
                                                else if (availableCheckBoxes.All(box => box.IsChecked == false))
                                                {
                                                    cb.IsChecked = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        } //NEEDS REDUX
    }
}
