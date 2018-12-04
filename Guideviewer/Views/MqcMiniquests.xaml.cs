using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Guideviewer.Views
{
	/// <summary>
	/// Interaction logic for MqcMiniquests.xaml
	/// </summary>
	public partial class MqcMiniquests : UserControl
	{
		public MqcMiniquests()
		{
			InitializeComponent();

            DataContext = new CheckBoxViewModel();
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
