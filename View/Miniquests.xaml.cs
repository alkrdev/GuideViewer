using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuideViewer.View
{
    /// <summary>
    /// Interaction logic for Miniquests.xaml
    /// </summary>
    public partial class Miniquests : UserControl
    {
        public Miniquests()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Check(object sender, RoutedEventArgs e) => Switch(sender, true);
        private void UnCheck(object sender, RoutedEventArgs e) => Switch(sender, false);
        private void Switch(object sender, bool boolean)
        {
            if (!(sender is CheckBox senderBox)) return;
            // Main Duplicate Control
            if (MainWindow.data.CheckboxesDictionary.TryGetValue(senderBox.Name, out var value))
            {
                foreach (var x in value) x.IsChecked = boolean;
            }

            // Select All Control
            if (!senderBox.Name.StartsWith("Sa")) return;
            foreach (var listViewChild in LogicalTreeHelper.GetChildren(LogicalTreeHelper.GetParent(senderBox)))
            {
                if (listViewChild is CheckBox cb)
                {
                    cb.IsChecked = boolean;
                }
            }
        }
    }
}
