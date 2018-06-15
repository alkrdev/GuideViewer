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
            if (sender == Aby) {
                Abyc.IsChecked = true;
            } else if (sender == Abyc) {
                Aby.IsChecked = true;
            }
        }

        private void UnCheckHandler(object sender, RoutedEventArgs routedEventArgs) {

            if (sender == Aby) {
                Abyc.IsChecked = false;
            } else if (sender == Abyc) {
                Aby.IsChecked = false;
            }
        }
    }
}
