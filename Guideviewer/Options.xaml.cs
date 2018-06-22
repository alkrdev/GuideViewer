using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Guideviewer {
    public partial class Options {
        public Options() {
            InitializeComponent();
        }

        enum TitleOfNames {
            Mqc,
            Comp,
            TrimComp,
            Media
        }
        
        private void MqcCheck(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender as CheckBox, true, TitleOfNames.Mqc);
        }
        
        private void MqcUnCheck(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender as CheckBox, false, TitleOfNames.Mqc);
        }



        private void CompCheck(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender as CheckBox, true, TitleOfNames.Comp);
        }
        
        private void CompUnCheck(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender as CheckBox, false, TitleOfNames.Comp);
        }



        private void TrimCompCheck(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender as CheckBox, true, TitleOfNames.TrimComp);
        }
        
        private void TrimCompUnCheck(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender as CheckBox, false, TitleOfNames.TrimComp);
        }



        private void Media(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender as CheckBox, false, TitleOfNames.Media);
        }

        private void Switch(CheckBox sender, bool boolean, TitleOfNames origin) {
            
            sender.IsChecked = boolean;
            
            if (FindName(sender + "c") != null) {
                sender + "c".IsChecked = boolean;
            } else if (FindName(sender + "t") != null) {
                sender + "t".IsChecked = boolean;
            }

        
            



            if (origin == TitleOfNames.Mqc) {
                switch (sender.Name) {
                    case "Abyc":
                        Abyc.IsChecked = boolean;
                        break;

                    case "Aby":
                        Aby.IsChecked = boolean;
                        break;

                    case "Ann":
                        Annc.IsChecked = boolean;
                        break;
                    case "Annc":
                        Ann.IsChecked = boolean;
                        break;


                    case "Aprt":
                        Aprc.IsChecked = boolean;
                        break;
                    case "Aprc":
                        Aprt.IsChecked = boolean;
                        break;


                    case "Bam":
                        Bamc.IsChecked = boolean;
                        break;
                    case "Bamc":
                        Bam.IsChecked = boolean;
                        break;


                    case "Bts":
                        Btst.IsChecked = boolean;
                        break;
                    case "Btst":
                        Bts.IsChecked = boolean;
                        break;


                    case "Frs":
                        Frsc.IsChecked = boolean;
                        break;
                    case "Frsc":
                        Frs.IsChecked = boolean;
                        break;


                    case "Sab":
                        Annc.IsChecked = boolean;
                        break;
                    case "Sabt":
                        Ann.IsChecked = boolean;
                        break;


                    case "Tbo":
                        Annc.IsChecked = boolean;
                        break;
                    case "Tboc":
                        Ann.IsChecked = boolean;
                        break;
                }
            }
            else if (origin == TitleOfNames.Comp) {
                switch (senderC.Name)
                {
                    default:
                        break;
                }
            }
            else if (origin == TitleOfNames.TrimComp) {
                switch (senderT.Name)
                {
                    default:
                        break;
                }
            }
            else if (origin == TitleOfNames.Media) {
                switch (sender.Name)
                {
                    // MediaControl
                    case "System.Windows.Controls.Button: Play" when CiControlPlay.Content.ToString() == "Play":
                        ChimpIce.Play();
                        CiControlPlay.Content = "Pause";
                        break;
                    case "System.Windows.Controls.Button: Pause" when CiControlPlay.Content.ToString() == "Pause":
                        ChimpIce.Pause();
                        CiControlPlay.Content = "Play";
                        break;
                    case "System.Windows.Controls.Button: Reset":
                        ChimpIce.Stop();
                        CiControlPlay.Content = "Play";
                        break;
                    default:
                        break;
                }

            }

           
        }

        class Personal {
            public Dictionary<string, CheckBox> checkboxes = new Dictionary<string, CheckBox>() {
                {"Ann", Options.Ann}
            };
        }
    }
}
