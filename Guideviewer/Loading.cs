using System;
using static Guideviewer.MainWindow;
using static Guideviewer.User;

namespace Guideviewer
{
    public class Loading
    {
        public void LoadUser(string userquestData, string[] userskillData, bool online)
        {
            // Loop through the amount of skills
            new Progress().ExtractInsert(userskillData, online);
            
            // Loop through the length of the entire first column
            for (int i = 1; i < ColumnList[0].Length; i++)
            {
                // If the current value starts with "[Train"
                if (ColumnList[0][i].StartsWith("[Train"))
                {
					// Then loop through all known skills in the game
					SkillNames.ForEach(skill =>
					{
						if (skill != "Total")
						{
							// If the current value contains the current skill
							if (ColumnList[0][i].Contains(skill))
							{
								// Fetch the users level, in the currently focused skill
								if (SkillsDictionary.TryGetValue(skill, out int value))
								{
									// If the users level is higher than our focuslevel
									if (Convert.ToInt32(ColumnList[0][i].Substring(("[Train " + skill + " to ").Length).Replace("]", "").Replace("[OPTIONAL", "")) <= value)
									{
										// Remove the value from the Datagrid
										ColumnList[0][i] = ColumnList[0][i].Remove(0);
									}
								}
							}
						}
					});
                }
            }

            // For each quest in our Questslist
            foreach (var t in Quests.FromJson(userquestData).QuestsList)
            {
                // Loop through the length of our first column
                for (int j = 0; j < ColumnList[0].Length; j++)
                {
                    // If the title of the current quest, and the current value are the same - And the quest has been completed
                    if (t.Title == ColumnList[0][j] && t.Status == Status.Completed)
                    {
                        // Remove the value from the Datagrid
                        ColumnList[0][j] = ColumnList[0][j].Remove(0);

                        // If there is a value in the column to the right
                        if (ColumnList[1][j] != "")
                        {
                            // Remove the value from the Datagrid
                            ColumnList[1][j] = ColumnList[1][j].Remove(0);

                        }

                        // For each column in our columnlist
                        foreach (var col in ColumnList)
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
                        }

                        // For each prerequisite combination
                        foreach (var t1 in PrerequisiteTuples)
                        {
                            new Specific().PrerequisiteRemover(t1.Item1, t1.Item2, t);
                        }
                    }
                }
            }
        }
    }
}
