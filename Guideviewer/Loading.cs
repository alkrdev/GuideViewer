using System;
using System.Collections.Generic;
using System.Linq;

namespace Guideviewer
{
    public class Loading
	{
		//Parameters to handle GoogleRequest
		public static IList<IList<object>> Values;

		public void LoadUser(string userquestData, string[] userskillData, bool online)
        {
            // Loop through the amount of skills
            new Progress().ExtractInsert(userskillData, online);

			MainWindow.ColumnList[0].ToList().ForEach(text =>
			{
				// If the current value starts with "[Train"
				if (text.StartsWith("[Train"))
				{
					// Then loop through all known skills in the game
					User.SkillNames.ForEach(skill =>
					{
						// If the users level is higher than our focuslevel
						if (skill != "Total" && text.Contains(skill) && User.SkillsDictionary.TryGetValue(skill, out int value) && Convert.ToInt32(
								text.Substring(("[Train " + skill + " to ").Length)
									.Replace("]", "").Replace("[OPTIONAL", "")) <= value) {
							// Remove the value from the Datagrid
							text = text.Remove(0);
						}					
					});
				}
			});

			Quests.FromJson(userquestData).QuestsList.ForEach(quest => {
				// Loop through the length of our first column
				for (int j = 0; j < MainWindow.ColumnList[0].Length; j++)
				{
					// If the title of the current quest, and the current value are the same - And the quest has been completed
					if (quest.Title == MainWindow.ColumnList[0][j] && quest.Status == Status.Completed)
					{
						// Remove the value from the Datagrid
						MainWindow.ColumnList[0][j] = MainWindow.ColumnList[0][j].Remove(0);

						// If there is a value in the column to the right
						if (MainWindow.ColumnList[1][j] != "")
						{
							// Remove the value from the Datagrid
							MainWindow.ColumnList[1][j] = MainWindow.ColumnList[1][j].Remove(0);
						}

						MainWindow.ColumnList.ForEach(col =>
						{
							// Loop through the length of our Column
							for (var i = 0; i < col.Length; i++)
							{
								// If the value is a space, and the value is not nothing
								if (col[i] == " " && col[i] != "")
								{
									// Remove the value from the Datagrid
									col[i] = col[i].Remove(0);
								}
							}
						});

						User.PrerequisiteTuples.ToList().ForEach(
						pre => {
							new Specific().PrerequisiteRemover(pre.Item1, pre.Item2, quest);
						});
					}
				}
			});
        }

		public void FirstLoad()
		{
			//Google Request
			Values = new GoogleRequest().GoogleRequestInit().Execute().Values;

			//Insert the requested data into column arrays
			if (Values != null && Values.Count > 0)
			{
				for (var j = 0; j < Values.Count; j++)
				{
					for (int i = 0; i < 5; i++)
					{
						MainWindow.ColumnList[i][j] = Values[j][i].ToString();
					}
				}
			}

			//MainWindow.FillAllColumns();

			for (int i = 0; i < User.LoadedSkillLevels.Length; i++)
			{
				if (i == 4)
				{
					User.LoadedSkillLevels[i] = 10;
					User.LoadedSkillExperiences[i] = 1154;
				}
				else
				{
					User.LoadedSkillLevels[i] = 1;
					User.LoadedSkillExperiences[i] = 0;
				}
				User.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], User.LoadedSkillLevels[i], User.LoadedSkillExperiences[i]);
			}
		}
	}
}
