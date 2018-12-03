using Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Guideviewer
{
	class CheckBoxViewModel
	{
		public ObservableCollection<CheckBoxModel> AllCheckboxes { get; set; }

		List<Tuple<string, string>> checkBoxTupleInfo = new List<Tuple<string, string>>
		{
			new Tuple<string, string>( "Abyssal_Reach", "The Abyss" ),
			new Tuple<string, string>( "Bar_Crawl", "Barcrawl" ),
			new Tuple<string, string>( "The_Curse_of_Zaros", "The Curse of Zaros" ),
			new Tuple<string, string>( "Desert_Slayer_Dungeon", "Desert Slayer Dungeon" ),
			new Tuple<string, string>( "Banking_History", "Banking History" ),
			new Tuple<string, string>( "Fur_%27n%27_Seek", "Fur 'n' Seek Wishlist"),
		};

		public void LoadCheckBoxes()
		{
			string linkPrefix = "https://runescape.wiki/w/";

			AllCheckboxes = new ObservableCollection<CheckBoxModel>	{				
				new CheckBoxModel(CheckBoxType.SelectAll, CheckBoxType.Mqc)
			};	

			checkBoxTupleInfo.ForEach(tuple => {
				AllCheckboxes.Add(new CheckBoxModel(linkPrefix + tuple.Item1, tuple.Item2, CheckBoxType.Mqc));
			});
		}
	}
}
