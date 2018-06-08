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
                D1.IsChecked = true;
                D2.IsChecked = true;
                D3.IsChecked = true;
                D4.IsChecked = true;
                D5.IsChecked = true;
                D6.IsChecked = true;
                D7.IsChecked = true;
                D8.IsChecked = true;
                B1.IsChecked = true;
                B2.IsChecked = true;
                B3.IsChecked = true;
            }
        }
    }
}
