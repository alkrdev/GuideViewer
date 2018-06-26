using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Guideviewer {
    public partial class Options {

        private readonly Dictionary<string, List<CheckBox>> _checkboxesDictionary = new Dictionary<string, List<CheckBox>>();
        private readonly List<Tuple<CheckBox, ListView>> _listViewSelectAlList = new List<Tuple<CheckBox, ListView>>();
        private readonly List<CheckBox> _selectAllCheckBoxes = new List<CheckBox>();
        private readonly List<CheckBox> _allCheckBoxes = new List<CheckBox>();
        private readonly List<ListView> _listViews = new List<ListView>();


        public Options() {
            InitializeComponent();

            _checkboxesDictionary.Clear();
            _listViewSelectAlList.Clear();
            _selectAllCheckBoxes.Clear();
            _allCheckBoxes.Clear();
            _listViews.Clear();

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
                                            _listViews.Add(listview);
                                        }

                                        foreach (var checkbox in listview.Items) {
                                        if (checkbox is CheckBox cb && cb.Name.StartsWith("Sa")) {
                                            //MessageBox.Show("I just added " + cb.Name + " which is a SelectAll CheckBox, to _selectAllCheckBoxes");
                                            _selectAllCheckBoxes.Add(cb);
                                        } else if (checkbox is CheckBox cba) {
                                            //MessageBox.Show("I just added " + cba.Name + " which is a CheckBox, to _allCheckBoxes");
                                            _allCheckBoxes.Add(cba);
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
                    }
                    }
                }
            }

            for (int i = 0; i < _selectAllCheckBoxes.Count; i++) {
                _listViewSelectAlList.Add(new Tuple<CheckBox, ListView>(_selectAllCheckBoxes[i], _listViews[i]));
                //MessageBox.Show("Sa checkbox:" + _selectAllCheckBoxes[i].Name + " + Lv Listview: " + _listViews[i].Name);
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

                // Master Quest + Trimmed Completionist
                new List<CheckBox> {Bts, Btst}, // Balloon Transport System
                new List<CheckBox> {Swb, Swbt}, // Swept Away - Broomstick
                new List<CheckBox> {Ekm, Ekmt}, // Enchanted Key
                new List<CheckBox> {Aca, Acat}, // Ancient Cavern
                new List<CheckBox> {Ter, Tert}, // Temple Trekking
                new List<CheckBox> {Tgu, Tgut}, // Thieves Guild Capers
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

                new List<CheckBox> {Sa03, Sa30}, //Select All D/B
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
                new List<CheckBox> {B3, B3c}

            }) {
                _checkboxesDictionary.Add(list[0].Name, list);
            }
        }
        
        private void MqcCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, true); }
        private void MqcUnCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false); }

        private void CompCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, true); }
        private void CompUnCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false); }

        private void TrimCompCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, true); }
        private void TrimCompUnCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false); }


        private void Media(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false); }

        private void Switch(CheckBox sender, bool boolean) {
            // Select All Control
            foreach (var tuple in _listViewSelectAlList) {
                if (Equals(tuple.Item1, sender)) {
                    foreach (var t in tuple.Item2.Items) {
                        if (t is CheckBox checkbox) {
                            checkbox.IsChecked = boolean;
                        }
                    }
                }
            }

            MessageBox.Show(sender.Name);
            // Main Duplicate Control
            if (!_checkboxesDictionary.TryGetValue(
                sender.Name.EndsWith("c")
                ? sender.Name.Remove(sender.Name.LastIndexOf('c'))
                : sender.Name.EndsWith("t")
                    ? sender.Name.Remove(sender.Name.LastIndexOf('t'))
                    : sender.Name.StartsWith("Sa") ? sender.Name.Replace("03", "30")
                    : sender.Name, out var value)) return;

            foreach (var cb in value) {
                cb.IsChecked = boolean;
            }

            // Main Media Control
            if (sender is CheckBox senderBox) {
                switch (senderBox.Name) {
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
    }
}