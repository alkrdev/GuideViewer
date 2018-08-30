using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using static Guideviewer.Options;
using static Guideviewer.Progress;
using static Guideviewer.User;
using static Guideviewer.Data;

namespace Guideviewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public bool HasLoaded;
        
        public string UrlUserName
        {
            get => UrlUsername.Text;
            set => UrlUsername.Text = value.Replace(' ', '_');
        }

        //Parameters to handle GoogleRequest
        public IList<IList<object>> Values;
                
        public static List<string[]> ColumnList = new List<string[]>();

        //Struct to insert data from datasource in correct columns
        public struct Data
        {
            public string Qt { set; get; }
            public string L { set; get; }
            public string Mqc { set; get; }
            public string Cc { set; get; }
            public string Tcc { set; get; }
            public string Im { set; get; }
        }



        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 6; i++)
            {
                ColumnList.Add(new string[new GoogleRequest().GoogleRequestInit().Execute().Values.Count]);
            }

            FirstLoad();
        }

        private void LoadOnline_OnClick (object sender, RoutedEventArgs e)
        {
            try
            {
                string runemetrics = new WebClient().DownloadString("https://apps.runescape.com/runemetrics/quests?user=" + UrlUserName);
                string hiscore = new WebClient().DownloadString("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + UrlUserName);
                Loading.LoadUser(runemetrics, hiscore.Split('\n'), true);
                        SaveText(runemetrics, UrlUserName, new StreamWriter($"{UrlUserName}.txt"), DefaultIntArrayString);
            }
            catch (Exception d)
            {
                MessageBox.Show($"The username is either wrong or the user has set their profile to private. If the username is correct, contact a developer. \n\n Error: {d}");
            }
            finally
            {
                MessageBox.Show("User was successfully loaded, please \"Reload\"");
            }

            if (HasLoaded)
            {
                MessageBox.Show("Please use the Reset function before loading an accounts progress again");
            }

            HasLoaded = true;
        }

        //Fill all of the columns
        private void FillAllColumns()
        {
            if (Values != null)
            {
                for (int a = 0; a < Values.Count; a++)
                {
                    MyDataGrid.Items.Add(new Data
                    {
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

        private void FirstLoad()
        {
            
            //Google Request
            Values = new GoogleRequest().GoogleRequestInit().Execute().Values;

            //Insert the requested data into column arrays
            if (Values != null && Values.Count > 0)
            {
                for (var j = 0; j < Values.Count; j++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        ColumnList[i][j] = Values[j][i].ToString();
                    }
                }
            }
            
            FillAllColumns();

            for (int i = 0; i < LoadedSkillLevels.Length; i++)
            {
                if (i == 4)
                {
                    LoadedSkillLevels[i] = 10;
                    LoadedSkillExperiences[i] = 1154;
                }
                else
                {
                    LoadedSkillLevels[i] = 1;
                    LoadedSkillExperiences[i] = 0;
                }
                Levels[i] = new Tuple<string, int, int>(SkillNames[i], LoadedSkillLevels[i], LoadedSkillExperiences[i]);
            }
        }

        private void DeleteEmptyRows(object sender, RoutedEventArgs routedEventArgs)
        {            
            for (int i = ColumnList[0].Length - 1; i >= 0; i--)
            {
                var strings = new List<string> { ColumnList[0][i], ColumnList[1][i], ColumnList[2][i],
                                                ColumnList[3][i], ColumnList[4][i], ColumnList[5][i] };
                
                if (strings.All(x => x == strings.First()))
                {
                    MyDataGrid.Items.RemoveAt(i);
                }


            }
        }

        private void Op_OnClick(object sender, RoutedEventArgs e)
        {
            new Options().Show();
        }

        private void LoadFile_OnClick(object sender, RoutedEventArgs e)
        {
            Load();
            HasLoaded = false;
        }

        private void Reset(object sender, RoutedEventArgs routedEventArgs)
        {
            HasLoaded = false;
            MyDataGrid.Items.Clear();
            MessageBox.Show("ALL ITEMS WERE CLEARED");

            FirstLoad();
        }

        private void Reload(object sender, RoutedEventArgs e)
        {
            if (HasApplied)
            {
                HasApplied = false;
                CheckboxesBoolDictionary.Clear();
                
                for (int i = 0; i < AllCheckBoxes.Count; i++)
                {
                    switch (AllCheckBoxes[i].IsChecked)
                    {
                        case true:
                            CheckboxesBoolDictionary.Add(AllCheckBoxes[i].Name, true);
                            break;
                        case false:
                            CheckboxesBoolDictionary.Add(AllCheckBoxes[i].Name, false);
                            break;
                    }

                    Specific.CheckBoxRemover(CheckboxesBoolDictionary, AllCheckBoxes[i], NameCompareTuples[i].Item1, NameCompareTuples[i].Item2);
                }
            }

            MyDataGrid.Items.Clear();
            FillAllColumns();
        }
    }
}