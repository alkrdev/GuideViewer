using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Guideviewer
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();


        }

        private void CheckboxHandler(object sender, RoutedEventArgs routedEventArgs) {
            if (Db.IsChecked == true) {
                D1.IsEnabled = true;
                D2.IsEnabled = true;
                D3.IsEnabled = true;
                D4.IsEnabled = true;
                D5.IsEnabled = true;
                D6.IsEnabled = true;
                D7.IsEnabled = true;
                D8.IsEnabled = true;
                B1.IsEnabled = true;
                B2.IsEnabled = true;
                B3.IsEnabled = true;
            }
        }
    }
}
