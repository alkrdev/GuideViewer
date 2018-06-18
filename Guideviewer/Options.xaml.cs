using System.Windows;

namespace Guideviewer {
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window {
        public Options() {
            InitializeComponent();


        }

        private void CheckHandler(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender, true);
        }

        private void UnCheckHandler(object sender, RoutedEventArgs routedEventArgs) {
            Switch(sender, false);
        }

        private void Switch(object sender, bool boolean) {
            switch ((string) sender) {
                // The Abyss
                case "Aby":
                    Abyc.IsChecked = boolean;
                    break;
                case "Abyc":
                    Aby.IsChecked = boolean;
                    break;

                 

                // Annihilator Title
                case "Ann":
                    Annc.IsChecked = boolean;
                    break;
                case "Annc":
                    Ann.IsChecked = boolean;
                    break;
            }
        }
    }
}
