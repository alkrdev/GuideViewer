using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Guideviewer {
    public partial class Options {
<<<<<<< HEAD
        private readonly Dictionary<string, List<CheckBox>> _checkboxes = new Dictionary<string, List<CheckBox>>();

        public Options() {
            InitializeComponent();

            _checkboxes.Add("Ann", new List<CheckBox> {Ann, Annc});
            _checkboxes.Add("Aby", new List<CheckBox> {Aby, Abyc});
            _checkboxes.Add("Bam", new List<CheckBox> {Bam, Bamc});
            _checkboxes.Add("Aprc", new List<CheckBox> {Aprc, Aprt});
            _checkboxes.Add("Bts", new List<CheckBox> {Bts, Btst});
            _checkboxes.Add("Frs", new List<CheckBox> {Frs, Frsc});
            _checkboxes.Add("Sab", new List<CheckBox> {Sab, Sabt});
            _checkboxes.Add("Tbo", new List<CheckBox> {Tbo, Tboc});

        }

        //Master Quest Cape on/off
        private void MqcCheck(object sender, RoutedEventArgs routedEventArgs) { Switch(sender as CheckBox, true); }
        private void MqcUnCheck(object sender, RoutedEventArgs routedEventArgs) { Switch(sender as CheckBox, false); }
=======
        
        private Dictionary<string, List<CheckBox>> _checkboxes = new Dictionary<string, List<CheckBox>>();
        
        public Options() {
            InitializeComponent();

            #region Method 1
            List<List<CheckBox>> checkboxes = new List<List<CheckBox>> {
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
                new List<CheckBox> {Sab, Sabt}, // Swept Away - Broomstick
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
                new List<CheckBox> {Mwk, Mwkt}, // Sheep Shearer
            
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

            };

            foreach (var list in checkboxes) {
                _checkboxes.Add(list[0].Name, list);
            }
>>>>>>> 90b6ed63a844920053102a651d63f08d7007e4de

            #endregion
            #region Method 2
//            // Master Quest + Completionist
//            _checkboxes.Add("Ann", new List<CheckBox> {Ann, Annc}); // Annihilator Title
//            _checkboxes.Add("Aby", new List<CheckBox> {Aby, Abyc}); // The Abyss
//            _checkboxes.Add("Bam", new List<CheckBox> {Bam, Bamc}); // Bandos Memories
//            _checkboxes.Add("Frs", new List<CheckBox> {Frs, Frsc}); // Fremennik Sagas
//            _checkboxes.Add("Tbo", new List<CheckBox> {Tbo, Tboc}); // Tune Bane Ore
//            _checkboxes.Add("Crv", new List<CheckBox> {Crv, Crvc}); // Carnillean Rising
//            _checkboxes.Add("Tha", new List<CheckBox> {Tha, Thac}); // Thalassus
//            _checkboxes.Add("Hag", new List<CheckBox> {Hag, Hagc}); // Hefin Agility Course
//            _checkboxes.Add("Rsp", new List<CheckBox> {Rsp, Rspc}); // Reconnect Spirit Tree
//            _checkboxes.Add("Cle", new List<CheckBox> {Cle, Clec}); // Cleansing Shadow Cores
//            _checkboxes.Add("Rob", new List<CheckBox> {Rob, Robc}); // Rush of Blood
//            _checkboxes.Add("Mam", new List<CheckBox> {Mam, Mamc}); // Mahjarrat Memories
//            _checkboxes.Add("Mge", new List<CheckBox> {Mge, Mgec}); // Memorial to Guthix Engrams
//            _checkboxes.Add("Sme", new List<CheckBox> {Sme, Smec}); // Seren Memoriam
//            _checkboxes.Add("Pme", new List<CheckBox> {Pme, Pmec}); // Prifddinas Memoriam
//            _checkboxes.Add("Zme", new List<CheckBox> {Zme, Zmec}); // Zaros Memoriam
//            _checkboxes.Add("Cme", new List<CheckBox> {Cme, Cmec}); // Core Memories
//            _checkboxes.Add("Fko", new List<CheckBox> {Fko, Fkoc}); // Full Kudos Obtained
//            _checkboxes.Add("Imt", new List<CheckBox> {Imt, Imtc}); // In Memory of the Myreque
//            _checkboxes.Add("Rcl", new List<CheckBox> {Rcl, Rclc}); // Returning Clarence
//            _checkboxes.Add("Our", new List<CheckBox> {Our, Ourc}); // Ouranaia Teleport
//            _checkboxes.Add("Pre", new List<CheckBox> {Pre, Prec}); // Lost Potion Recipes
//            _checkboxes.Add("Csr", new List<CheckBox> {Csr, Csrc}); // Crystal Singing Research
//            _checkboxes.Add("Sop", new List<CheckBox> {Sop, Sopc}); // Stronghold of Player Safety
//            _checkboxes.Add("Sos", new List<CheckBox> {Sos, Sosc}); // Stronghold of Security
//            _checkboxes.Add("Ttr", new List<CheckBox> {Ttr, Ttrc}); // The Lair of Tarn Razorlor
//            _checkboxes.Add("Rco", new List<CheckBox> {Rco, Rcoc}); // Removing Corruption
//            _checkboxes.Add("Hsw", new List<CheckBox> {Hsw, Hswc}); // Hopespear's Will
//
//            // Master Quest + Trimmed Completionist
//            _checkboxes.Add("Bts", new List<CheckBox> {Bts, Btst}); // Balloon Transport System
//            _checkboxes.Add("Sab", new List<CheckBox> {Sab, Sabt}); // Swept Away - Broomstick
//            _checkboxes.Add("Ekm", new List<CheckBox> {Ekm, Ekmt}); // Enchanted Key
//            _checkboxes.Add("Aca", new List<CheckBox> {Aca, Acat}); // Ancient Cavern
//            _checkboxes.Add("Ter", new List<CheckBox> {Ter, Tert}); // Temple Trekking
//            _checkboxes.Add("Tgu", new List<CheckBox> {Tgu, Tgut}); // Thieves Guild Capers
//            _checkboxes.Add("Etr", new List<CheckBox> {Etr, Etrt}); // Eagle Transport Route
//            _checkboxes.Add("Qbd", new List<CheckBox> {Qbd, Qbdt}); // Queen Black Dragon Journals
//            _checkboxes.Add("Bch", new List<CheckBox> {Bch, Bcht}); // Broken Home Challenges
//            _checkboxes.Add("Cts", new List<CheckBox> {Cts, Ctst}); // Char's Treasured Symbol
//            _checkboxes.Add("Uif", new List<CheckBox> {Uif, Uift}); // Upgrade Ivandis Flail
//            _checkboxes.Add("Wip", new List<CheckBox> {Wip, Wipt}); // Witch's Potion
//            _checkboxes.Add("Ton", new List<CheckBox> {Ton, Tont}); // Tales of Nomad
//            _checkboxes.Add("Tgw", new List<CheckBox> {Tgw, Tgwt}); // Tales of the God Wars
//            _checkboxes.Add("Dsl", new List<CheckBox> {Dsl, Dslt}); // Desert Slayer Dungeon
//            _checkboxes.Add("Scn", new List<CheckBox> {Scn, Scnt}); // Scabarite Notes
//            _checkboxes.Add("Sde", new List<CheckBox> {Sde, Sdet}); // Song from the Depths
//            _checkboxes.Add("Shs", new List<CheckBox> {Shs, Shst}); // Sheep Shearer
//            _checkboxes.Add("Mwk", new List<CheckBox> {Mwk, Mwkt}); // Sheep Shearer
//            
//            // Doric and Boric Tasks
//            _checkboxes.Add("D1", new List<CheckBox> {D1, D1c});
//            _checkboxes.Add("D2", new List<CheckBox> {D2, D2c}); 
//            _checkboxes.Add("D3", new List<CheckBox> {D3, D3c}); 
//            _checkboxes.Add("D4", new List<CheckBox> {D4, D4c}); 
//            _checkboxes.Add("D5", new List<CheckBox> {D5, D5c}); 
//            _checkboxes.Add("D6", new List<CheckBox> {D6, D6c}); 
//            _checkboxes.Add("D7", new List<CheckBox> {D7, D7c}); 
//            _checkboxes.Add("D8", new List<CheckBox> {D8, D8c}); 
//            // Boric Tasks
//            _checkboxes.Add("B1", new List<CheckBox> {B1, B1c}); 
//            _checkboxes.Add("B2", new List<CheckBox> {B2, B2c}); 
//            _checkboxes.Add("B3", new List<CheckBox> {B3, B3c});
            #endregion
    }

<<<<<<< HEAD
        //Completionist Cape on/off
        private void CompCheck(object sender, RoutedEventArgs routedEventArgs) { Switch(sender as CheckBox, true); }
        private void CompUnCheck(object sender, RoutedEventArgs routedEventArgs) { Switch(sender as CheckBox, false); }


        //Trimmed Completionist Cape on/off
        private void TrimCompCheck(object sender, RoutedEventArgs routedEventArgs) { Switch(sender as CheckBox, true); }
        private void TrimCompUnCheck(object sender, RoutedEventArgs routedEventArgs) { Switch(sender as CheckBox, false); }


        //Media Controls
        private void Media(object sender, RoutedEventArgs routedEventArgs) {
            if (!(sender is CheckBox senderBox)) return;
            switch (senderBox.Name)
            {
                case "CiControlPlay":
                    switch (CiControlPlay.Content.ToString())
                    {
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

        private void Switch(CheckBox sender, bool boolean) {
            
            if (sender.Name.EndsWith("c")) {
                NameCheck(boolean, sender.Name.Remove(sender.Name.LastIndexOf('c')));
            } else if (sender.Name.EndsWith("t")) {
                NameCheck(boolean, sender.Name.Remove(sender.Name.LastIndexOf('t')));
            } else {
                NameCheck(boolean, sender.Name);
            }
=======
        private void MqcCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, true);}
        private void MqcUnCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false);}
        
        private void CompCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, true);}
        private void CompUnCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false);}
        
        private void TrimCompCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, true);}
        private void TrimCompUnCheck(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false);}

        private void Media(object sender, RoutedEventArgs routedEventArgs) {Switch(sender as CheckBox, false);}
        
        
        private void Switch(CheckBox sender, bool boolean) {
>>>>>>> 90b6ed63a844920053102a651d63f08d7007e4de
            
            if (!_checkboxes.TryGetValue(sender.Name.EndsWith("c") ? sender.Name.Remove(sender.Name.LastIndexOf('c')) :
                                         sender.Name.EndsWith("t") ? sender.Name.Remove(sender.Name.LastIndexOf('t')) : 
                                         
                                         sender.Name, out var value)) return;

<<<<<<< HEAD
        }

        private void NameCheck(bool boolean, string senderName) {
            if (!_checkboxes.TryGetValue(senderName, out var response)) return;
            foreach (var checkBox in response)
            {
                checkBox.IsChecked = boolean;
=======
            foreach (var cb in value) {
                cb.IsChecked = boolean;
            }

            switch (sender.Name) {
                // MediaControl
                case "CiControlPlay" when CiControlPlay.Content.ToString() == "Play":
                    ChimpIce.Play();
                    CiControlPlay.Content = "Pause";
                    break;
                case "CiControlPlay" when CiControlPlay.Content.ToString() == "Pause":
                    ChimpIce.Pause();
                    CiControlPlay.Content = "Play";
                    break;
                case "CiControlReset":
                    ChimpIce.Stop();
                    CiControlPlay.Content = "Play";
                    break;
>>>>>>> 90b6ed63a844920053102a651d63f08d7007e4de
            }
        }
    }
}
