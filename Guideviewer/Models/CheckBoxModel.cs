using Library;
using System.ComponentModel;

namespace Guideviewer
{
	class CheckBoxModel
	{
		private string _hyperlink;
		private string _text;
		private CheckBoxType CbType1;
		private CheckBoxType CbType2;
		private CheckBoxType CbType3;
		public string Checked = "Check";
		public string Unchecked = "Uncheck";
		public string RequestNavigate = "Hyperlink_RequestNavigate";

		public CheckBoxModel(CheckBoxType cbType1, CheckBoxType cbType2)
		{
			Text = "Select All";
			CbType1 = cbType1;
			CbType2 = cbType2;
		}
		public CheckBoxModel(string hyperlink, string text)
		{
			Hyperlink = hyperlink;
			Text = text;
		}
		public CheckBoxModel(string hyperlink, string text, CheckBoxType cbType1)
		{
			Hyperlink = hyperlink;
			Text = text;
			CbType1 = cbType1;
		}
		public CheckBoxModel(string hyperlink, string text, CheckBoxType cbType1, CheckBoxType cbType2)
		{
			Hyperlink = hyperlink;
			Text = text;
			CbType1 = cbType1;
			CbType2 = cbType2;
		}
		public CheckBoxModel(string hyperlink, string text, CheckBoxType cbType1, CheckBoxType cbType2, CheckBoxType cbType3)
		{
			Hyperlink = hyperlink;
			Text = text;
			CbType1 = cbType1;
			CbType2 = cbType2;
			CbType3 = cbType3;
		}
		public string Hyperlink
		{
			get => _hyperlink;
			set
			{
				_hyperlink = value;
				RaisePropertyChanged("Hyperlink");
			}
		}

		public string Text
		{
			get => _text;
			set
			{
				_text = value;
				RaisePropertyChanged("Text");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string property)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
