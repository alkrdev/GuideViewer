using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Guideviewer {
    public partial class Options {

        public static readonly Dictionary<string, List<CheckBox>> CheckboxesDictionary =
            new Dictionary<string, List<CheckBox>>();

        public static readonly Dictionary<string, bool> CheckboxesBoolDictionary = new Dictionary<string, bool>();

        public static readonly List<Tuple<CheckBox, ListView>> ListViewSelectAllList = new List<Tuple<CheckBox, ListView>>();
        public static readonly List<Tuple<string, string>> NameCompareTuples = new List<Tuple<string, string>>();
        public static readonly List<CheckBox> SelectAllCheckBoxes = new List<CheckBox>();
        public static readonly List<CheckBox> AllCheckBoxes = new List<CheckBox>();
        public static readonly List<ListView> ListViews = new List<ListView>();

        // ReSharper disable once RedundantDefaultMemberInitializer
        public static bool HasApplied = false;

        public string ApplyUserName {
            get => ApplyUsername.Text;
            set => ApplyUsername.Text = value.Replace(' ', '_');
        }

        public string CheckboxStringSave;

        public Options() {
            InitializeComponent();

            CheckboxesDictionary.Clear();
            ListViewSelectAllList.Clear();
            SelectAllCheckBoxes.Clear();
            AllCheckBoxes.Clear();
            ListViews.Clear();

            foreach (var tabcontrolItem in MainTabControl.Items) {
            if (tabcontrolItem is TabItem tabitem) {
                foreach (var child in LogicalTreeHelper.GetChildren(tabitem)) {
                if (child is Grid grid) {
                    foreach (var gridChild in grid.Children) {
                    if (gridChild is TabControl tabcontrol) {
                        foreach (var tabcontrolItem2 in tabcontrol.Items) {
                        if (tabcontrolItem2 is TabItem tabitem2) {
                            foreach (var child2 in LogicalTreeHelper.GetChildren(tabitem2)) {
                            if (child2 is Grid grid2) {
                                foreach (var grid2Child in grid2.Children) {
                                if (grid2Child is ListView listview) {
                                    if (listview.Name.StartsWith("Mq") ||
                                        listview.Name.StartsWith("Sa") ||
                                        listview.Name.StartsWith("Co") ||
                                        listview.Name.StartsWith("Tri")) {
                                        //MessageBox.Show("I just added " + listview.Name + " which is a ListView, to _listViews");
                                        ListViews.Add(listview);
                                    }
                                    foreach (var checkbox in listview.Items) {
                                        if (checkbox is CheckBox cb && cb.Name.StartsWith("Sa")) {
                                            //MessageBox.Show("I just added " + cb.Name + " which is a SelectAll CheckBox, to _selectAllCheckBoxes");
                                            SelectAllCheckBoxes.Add(cb);
                                        }
                                        else if (checkbox is CheckBox cba) {
                                            //MessageBox.Show("I just added " + cba.Name + " which is a CheckBox, to _allCheckBoxes");
                                            AllCheckBoxes.Add(cba);
                                        }
            }}}}}}}}}}}}}

            for (int i = 0; i < SelectAllCheckBoxes.Count; i++) {
                ListViewSelectAllList.Add(new Tuple<CheckBox, ListView>(SelectAllCheckBoxes[i], ListViews[i]));
            }

            foreach (var list in new List<List<CheckBox>> {
                // Master Quest + Completionist 
                new List<CheckBox> {Ann, Annc}, // Annihilator Title
                new List<CheckBox> {Aby, Abyc}, // The Abyss
                new List<CheckBox> {Bam, Bamc}, // Bandos Memories
                new List<CheckBox> {Frs, Frsc}, // Fremennik Sagas
                new List<CheckBox> {Tbo, Tboc}, // Tune Bane Ore
                new List<CheckBox> {Crv, Crvc}, // Carnillean Rising
                new List<CheckBox> {Tha, Thac}, // Thalassus
                new List<CheckBox> {Hag, Hagc}, // Hefin Agility Course
                new List<CheckBox> {Rsp, Rspc}, // Reconnect Spirit Tree
                new List<CheckBox> {Cle, Clec}, // Cleansing Shadow Cores
                new List<CheckBox> {Rob, Robc}, // Rush of Blood
                new List<CheckBox> {Mam, Mamc}, // Mahjarrat Memories
                new List<CheckBox> {Mge, Mgec}, // Memorial to Guthix Engrams
                new List<CheckBox> {Sme, Smec}, // Seren Memoriam
                new List<CheckBox> {Pme, Pmec}, // Prifddinas Memoriam
                new List<CheckBox> {Zme, Zmec}, // Zaros Memoriam
                new List<CheckBox> {Cme, Cmec}, // Core Memories
                new List<CheckBox> {Fko, Fkoc}, // Full Kudos Obtained
                new List<CheckBox> {Imt, Imtc}, // In Memory of the Myreque
                new List<CheckBox> {Rcl, Rclc}, // Returning Clarence
                new List<CheckBox> {Our, Ourc}, // Ouranaia Teleport
                new List<CheckBox> {Pre, Prec}, // Lost Potion Recipes
                new List<CheckBox> {Csr, Csrc}, // Crystal Singing Research
                new List<CheckBox> {Sop, Sopc}, // Stronghold of Player Safety
                new List<CheckBox> {Sos, Sosc}, // Stronghold of Security
                new List<CheckBox> {Ttr, Ttrc}, // The Lair of Tarn Razorlor
                new List<CheckBox> {Rco, Rcoc}, // Removing Corruption
                new List<CheckBox> {Hsw, Hswc}, // Hopespear's Will
                                                

                new List<CheckBox> {Sa03, Sa30}, // SelectAll Doric And Boric tasks
                // Doric Tasks
                new List<CheckBox> {D1, D1c},
                new List<CheckBox> {D2, D2c},
                new List<CheckBox> {D3, D3c},
                new List<CheckBox> {D4, D4c},
                new List<CheckBox> {D5, D5c},
                new List<CheckBox> {D6, D6c},
                new List<CheckBox> {D7, D7c},
                new List<CheckBox> {D8, D8c},
                // Boric Tasks
                new List<CheckBox> {B1, B1c},
                new List<CheckBox> {B2, B2c},
                new List<CheckBox> {B3, B3c},


                // Master Quest + Trimmed Completionist
                new List<CheckBox> {Bts, Btst}, // Balloon Transport System
                new List<CheckBox> {Swb, Swbt}, // Swept Away - Broomstick
                new List<CheckBox> {Ekm, Ekmt}, // Enchanted Key
                new List<CheckBox> {Aca, Acat}, // Ancient Cavern
                new List<CheckBox> {Ter, Tert}, // Temple Trekking
                new List<CheckBox> {Etr, Etrt}, // Eagle Transport Route
                new List<CheckBox> {Qbd, Qbdt}, // Queen Black Dragon Journals
                new List<CheckBox> {Bch, Bcht}, // Broken Home Challenges
                new List<CheckBox> {Cts, Ctst}, // Char's Treasured Symbol
                new List<CheckBox> {Uif, Uift}, // Upgrade Ivandis Flail
                new List<CheckBox> {Wip, Wipt}, // Witch's Potion
                new List<CheckBox> {Ton, Tont}, // Tales of Nomad
                new List<CheckBox> {Tgw, Tgwt}, // Tales of the God Wars
                new List<CheckBox> {Dsl, Dslt}, // Desert Slayer Dungeon
                new List<CheckBox> {Scn, Scnt}, // Scabarite Notes
                new List<CheckBox> {Sde, Sdet}, // Song from the Depths
                new List<CheckBox> {Shs, Shst}, // Sheep Shearer
                new List<CheckBox> {Mwk, Mwkt}, // Master White Knight
                new List<CheckBox> {Fta, Ftat}, // From Tiny Acorns
                new List<CheckBox> {Lhm, Lhmt}, // Lost Her Marbles
                new List<CheckBox> {Ago, Agot}, // A Guild of Our Own


                

            }) {
                CheckboxesDictionary.Add(list[0].Name, list);
                CheckboxesDictionary.Add(list[1].Name, list);
            }

            foreach (var cb in AllCheckBoxes) {
                NameCompareTuples.Add(new Tuple<string, string>(cb.Name, cb.Content.ToString()));
            }
        }

        private void Check(object sender, RoutedEventArgs routedEventArgs) {Switch(sender, true);}
        private void UnCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender, false);}
        
        private void Media(object sender, RoutedEventArgs routedEventArgs) {Switch(sender, false);}

        private void Switch(object sender, bool boolean) {
            if (sender is CheckBox senderBox) {
                // Main Duplicate Control
                if (CheckboxesDictionary.TryGetValue(senderBox.Name, out var value)) {
                    foreach (var cb in value) {
                        cb.IsChecked = boolean;
                    }
                }

                // Select All Control
                foreach (var tuple in ListViewSelectAllList) {
                    if (Equals(tuple.Item1, sender)) {
                        if (Equals(Sa03, sender)) {
                            if (CheckboxesDictionary.TryGetValue(Sa03.Name, out var dbList)) {
                                foreach (var cb in dbList) {
                                    cb.IsChecked = boolean;
                                    SelectAll(boolean, tuple);
                                }
                            }
                            else if (CheckboxesDictionary.TryGetValue(Sa30.Name, out var dbList2)) {
                                foreach (var cb in dbList2) {
                                    cb.IsChecked = boolean;
                                    SelectAll(boolean, tuple);
                                }
                            }
                        }
                        else {
                            SelectAll(boolean, tuple);
                        }
                    }
                }
            }


            if (sender is Button senderButton) {
                // Main Media Control
                switch (senderButton.Name) {
                    case "CiControlPlay":
                        switch (CiControlPlay.Content.ToString()) {
                            case "Play":
                                ChimpIce.Play();
                                CiControlPlay.Content = "Pause";
                                break;
                            case "Pause":
                                ChimpIce.Pause();
                                CiControlPlay.Content = "Play";
                                break;
                        }

                        break;
                    case "CiControlReset":
                        ChimpIce.Stop();
                        CiControlPlay.Content = "Play";
                        break;
                }
            }

        }

        private static void SelectAll(bool boolean, Tuple<CheckBox, ListView> tuple) {
            foreach (var t in tuple.Item2.Items) {
                if (t is CheckBox checkbox) {
                    checkbox.IsChecked = boolean;
                }
            }
        }

        private void OnApplyOptions(object sender, RoutedEventArgs e) {
            HasApplied = true;
            CheckboxStringSave = "";
            CheckboxesBoolDictionary.Clear();

            foreach (var cb in AllCheckBoxes) {
                switch (cb.IsChecked) {
                    case true:
                        CheckboxesBoolDictionary.Add(cb.Name, true);
                        CheckboxStringSave += "1,";
                        break;
                    case false:
                        CheckboxesBoolDictionary.Add(cb.Name, false);
                        CheckboxStringSave += "0,";
                        break;
                }
            }

            Progress.Save(
                new WebClient().DownloadString("https://apps.runescape.com/runemetrics/quests?user=" + ApplyUserName),
                ApplyUserName, new User(), new StreamWriter($"{ApplyUserName}.txt"), CheckboxStringSave);
        }

        private void OnOpenLoad(object sender, RoutedEventArgs routedEventArgs) {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true) {
                if (File.ReadLines(ofd.FileName).Skip(31).Take(1).First().EndsWith(",")) {
                    CheckboxStringSave = File.ReadLines(ofd.FileName).Skip(31).Take(1).First()
                                 .Remove(File.ReadLines(ofd.FileName).Skip(31).Take(1).First().LastIndexOf(','));
                }
                string[] checkBoxStringSaveArray = CheckboxStringSave.Split(',');

                int[] checkBoxIntArray = Array.ConvertAll(checkBoxStringSaveArray, int.Parse);

                for (var index = 0; index < checkBoxIntArray.Length; index++) {
                    AllCheckBoxes[index].IsChecked = checkBoxIntArray[index] == 1;
                }
            }

            foreach (var tabcontrolItem in MainTabControl.Items) {
                if (tabcontrolItem is TabItem tabitem) {
                    foreach (var child in LogicalTreeHelper.GetChildren(tabitem)) {
                        if (child is Grid grid) {
                            foreach (var gridChild in grid.Children) {
                                if (gridChild is TabControl tabcontrol) {
                                    foreach (var tabcontrolItem2 in tabcontrol.Items) {
                                        if (tabcontrolItem2 is TabItem tabitem2) {
                                            foreach (var child2 in LogicalTreeHelper.GetChildren(tabitem2)) {
                                                if (child2 is Grid grid2) {
                                                    foreach (var grid2Child in grid2.Children) {
                                                        if (grid2Child is ListView listview) {
                                                            List<CheckBox> availableCheckBoxes = new List<CheckBox>();
                                                            foreach (var checkbox in listview.Items) {
                                                                if (checkbox is CheckBox cb && !cb.Name.StartsWith("Sa")) {
                                                                    availableCheckBoxes.Add(cb);
                                                                }
                                                            }
                                                            foreach (var listviewItem in listview.Items) {
                                                                if (listviewItem is CheckBox cb && cb.Name.StartsWith("Sa")) {
                                                                    if (availableCheckBoxes.All(box => box.IsChecked == true)) {
                                                                        cb.IsChecked = true;
                                                                    } else if (availableCheckBoxes.All(box => box.IsChecked == false)) {
                                                                        cb.IsChecked = false;
                                                                    } 
            }   }   }   }   }   }   }   }   }   }   }   }   }   }
            ApplyUsername.Text = ofd.SafeFileName.Replace(".txt", "");
        }
    }
}