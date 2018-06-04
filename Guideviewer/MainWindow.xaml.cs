using System;
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

namespace Guideviewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json

        #region Initialize

        public string userName { get; set; }

        public static List<string> SkillsList = new User().SkillNames;

        public int[] LoadedSkillLevels = new int[SkillsList.Count];
        public int[] LoadedSkillExperiences = new int[SkillsList.Count];

        private const float WidthDef = 320;
        private const float WidthMed = 240;
        private const float Sm = 75;

        private int _deletedFirstRows;

        public IList<IList<Object>> Values;
        public ValueRange Response;

        public static int Limiter = new GoogleRequest().GoogleRequestInit().Execute().Values.Count;

        public string[] ColumnA = new string[Limiter];
        public string[] ColumnB = new string[Limiter];
        public string[] ColumnC = new string[Limiter];
        public string[] ColumnD = new string[Limiter];
        public string[] ColumnE = new string[Limiter];
        public string[] ColumnF = new string[Limiter];

        public SaveFileDialog Sfd = new SaveFileDialog {
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };


        public OpenFileDialog Ofd = new OpenFileDialog {
            Filter = "txt files (*.txt)|*.txt|" +
                     "All files (*.*)|*.*"
        };

        public DataGridTextColumn[] Colu = {
            new DataGridTextColumn {
                Binding = new Binding("Qt"),
                Header = "Quest / Train to"
            },
            new DataGridTextColumn {
                Binding = new Binding("L"),
                Header = "Lamps"
            },
            new DataGridTextColumn {
                Binding = new Binding("Mqc"),
                Header = "Master Quest Cape"
            },
            new DataGridTextColumn {
                Binding = new Binding("Cp"),
                Header = "Completionist Cape"
            },
            new DataGridTextColumn {
                Binding = new Binding("Tcp"),
                Header = "Trimmed Completionist Cape"
            },
            new DataGridTextColumn {
                
                Binding = new Binding("Im"),
                Header = "Ironman"
            }
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
            
            _deletedFirstRows = Convert.ToInt32(new StreamReader("deletedrows.txt").ReadToEnd());
            Values = new GoogleRequest().GoogleRequestInit().Execute().Values;
            
            #region Create Columns
            #region Style

            var style = new Style(typeof(TextBlock));
            var headerstyle = new Style(typeof(DataGridColumnHeader));
            var cell = new Style(typeof(DataGridCell));

            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center));
            style.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));

            headerstyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty,
                HorizontalAlignment.Center));

            cell.Setters.Add(new Setter(BackgroundProperty, Brushes.Black));
            cell.Setters.Add(new Setter(PaddingProperty, new Thickness(3)));

            MyDataGrid.MinRowHeight = 30;

            #endregion

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


            for (var i = 0; i < 6; i++) {

                Colu[i].ElementStyle = style;
                Colu[i].HeaderStyle = headerstyle;
                Colu[i].CellStyle = cell;

                switch (i) {
                    case 0:
                        Colu[i].Foreground = Brushes.Aqua;
                        break;
                    case 1:
                        Colu[i].Foreground = Brushes.White;
                        Colu[i].MinWidth = Sm;
                        //Colu[i].Width = 1600 / 5 / 4.6;
                        Colu[i].MaxWidth = Sm;
                        break;
                    case 2:
                        Colu[i].Foreground = Brushes.CornflowerBlue;
                        Colu[i].MinWidth = WidthMed;
                        //Colu[i].Width = 1600 / 5 / 5 * 3.7;
                        Colu[i].MaxWidth = WidthMed;
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
                        Colu[i].MinWidth = WidthDef;
                        //Colu[i].Width = 1600 / 5;
                        Colu[i].MaxWidth = WidthDef;
                        break;
                }

                MyDataGrid.Columns.Add(Colu[i]);

            }

            FillAllColumns();

            #endregion
        }

        #region Methods

        private void DeleteFirstRow_OnClick(object sender, RoutedEventArgs e) {
            MyDataGrid.Items.RemoveAt(1);
            _deletedFirstRows++;
        }

        private void SaveToFile_OnClick(object sender, RoutedEventArgs e) {
            bool? result = Sfd.ShowDialog();
            if (result == true) {
                StreamWriter sw = new StreamWriter(File.Create(Sfd.FileName));
                sw.WriteAsync(_deletedFirstRows.ToString());
                sw.Dispose();
            }
        }

        private void LoadFile_OnClick(object sender, RoutedEventArgs e) {
            if (Ofd.ShowDialog() == true) {

                MyDataGrid.Items.Clear();

                FillAllColumns();
            }
        }

        private void LoadProgress_OnClick (object sender, RoutedEventArgs e) {
            try {
                WebClient wc = new WebClient();
                User u = new User();
            
                var skills = wc.DownloadString("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + Box.Text.Replace(' ', '_')).Split('\n');

                for (int i = 1; i < SkillsList.Count; i++) {

                    var categories = skills[i].Split(',');
                    var skill = new Tuple<int, int>(Convert.ToInt32(categories[1]), Convert.ToInt32(categories[2]));

                    LoadedSkillLevels[i] = skill.Item1;
                    LoadedSkillExperiences[i] = skill.Item2;

                    for (var index = 1; index < ColumnA.Length; index++) {
                        if (ColumnA[index].Contains("[Train")) {
                            string combined = u.SkillNames[i] + " to ";

                            if (ColumnA[index].Contains(combined)) {
                                string s = combined + ColumnA[index].Substring(ColumnA[index].IndexOf(combined, 3, StringComparison.Ordinal) + combined.Length, 3).Replace(" ", "");
                                //MessageBox.Show(s);

                                if (s.EndsWith("]")) {
                                    s = s.Remove(s.LastIndexOf(']'), 1);
                                    //MessageBox.Show("S HAD A \"]\", WHICH WAS REMOVED: " + s);
                                } else if (s.EndsWith(",")) {
                                    s = s.Remove(s.LastIndexOf(','), 1);
                                    //MessageBox.Show("S HAD A \",\", WHICH WAS REMOVED: " + s);
                                } else if (s.EndsWith(" ")) {
                                    s = s.Remove(s.LastIndexOf(' '), 1);
                                    //MessageBox.Show("S HAD A \" \", WHICH WAS REMOVED: " + s);
                                }

                                //MessageBox.Show(s.Substring(s.IndexOf(combined, 1, StringComparison.Ordinal) + combined.Length + 1).Replace(" ", ""));
                                if (Convert.ToInt32(s.Substring(s.IndexOf(combined, 1, StringComparison.Ordinal) +
                                                                combined.Length + 1).Replace(" ", "")) > skill.Item1) {


                                    for (int j = 0; j < 10; j++) {
                                        if (s.EndsWith(j.ToString())) {
                                            ColumnA[index] = ColumnA[index].Replace(s + j, "");
                                        } 
                                    }
                                }

                                //MessageBox.Show(s);
                                //MessageBox.Show(ColumnA[index].Replace(s, ""));
                            }

                            if (ColumnA[index].Contains(" ,")) {
                                ColumnA[index] = ColumnA[index].Replace(" ,", "");
                            }

                            switch (ColumnA[index])
                            {
                                case "[Train ]":
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
                                default:
                                    break;
                            }
                        }
                    }
                }

                MyDataGrid.Items.Clear();
                MessageBox.Show("ALL ITEMS WERE CLEARED");
                FillAllColumns();
            }
            catch (Exception d) {
                MessageBox.Show($"{d}");
               
            }
        }

        private void FillAllColumns() {
            if (Values != null) {
                for (int a = 0; a < Values.Count; a++) {
                    MyDataGrid.Items.Add(new MyData {
                        Qt = ColumnA[a],
                        L = ColumnB[a],
                        Mqc = ColumnC[a],
                        Cp = ColumnD[a],
                        Tcp = ColumnE[a],
                        Im = ColumnF[a]
                    });
                }
            }
        }
        #endregion
    }
}