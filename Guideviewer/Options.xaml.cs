using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Guideviewer {
    public partial class Options {
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
            

        }

        private void NameCheck(bool boolean, string senderName) {
            if (!_checkboxes.TryGetValue(senderName, out var response)) return;
            foreach (var checkBox in response)
            {
                checkBox.IsChecked = boolean;
            }
        }
    }
}
