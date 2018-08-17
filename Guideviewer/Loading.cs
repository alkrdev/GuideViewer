using System;
using static Guideviewer.MainWindow;
using static Guideviewer.User;

namespace Guideviewer
{
    public class Loading
    {
        public static void LoadUser(string userquestData, string[] userskillData, bool online)
        {
            // Loop through the amount of skills
            Progress.ExtractInsert(userskillData, online);
            
            // Loop through the length of the entire first column
            for (int i = 1; i < ColumnList[0].Length; i++)
            {
                // If the current value starts with "[Train"
                if (ColumnList[0][i].StartsWith("[Train"))
                {
                    // Then loop through all known skills in the game
                    foreach (var skill in SkillNames)
                    {
                        // But skip the skillname "Total", because it is not a skill - It's an accumulation
                        if (skill == "Total")
                        {
                            // Skip
                            continue;
                        }

                        // If the current value contains the current skill
                        if (ColumnList[0][i].Contains(skill))
                        {
                            // Create a string.    Example: "[Train Attack to "
                            string text = "[Train " + skill + " to ";

                            // Create a substring. Example: "[Train Attack to 70]" => "70]"
                            var substring = ColumnList[0][i].Substring(text.Length);

                            // Replace "]"         Example: "70]" => "70"
                            var replace = substring.Replace("]", "");
                            var replaceA = replace.Replace("[OPTIONAL", "");

                            // Convert our String to an Int
                            var focusLevel = Convert.ToInt32(replaceA);

                            // Fetch the users level, in the currently focused skill
                            if (SkillsDictionary.TryGetValue(skill, out int value))
                            {
                                // If the users level is higher than our focuslevel
                                if (focusLevel <= value)
                                {
                                    // Remove the value from the Datagrid
                                    ColumnList[0][i] = ColumnList[0][i].Remove(0);
                                }
                            }
                        }
                    }
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

                        Specific.PrerequisiteRemover("Scorpion Catcher", "Barcrawl Miniquest", t);
                        Specific.PrerequisiteRemover("Nomad's Requiem", "Soul Wars Tutorial", t);
                        Specific.PrerequisiteRemover("Children of Mah", "Koschei's Troubles miniquest", t);
                        Specific.PrerequisiteRemover("While Guthix Sleeps", "Chaos Tunnels: Hunt for Surok miniquest", t);
                        Specific.PrerequisiteRemover("Crocodile Tears", "Tier 3 Menaphos City Reputation", t);
                        Specific.PrerequisiteRemover("Our Man in the North", "Tier 6 Menaphos City Reputation", t);
                        Specific.PrerequisiteRemover("'Phite Club", "Tier 9 Menaphos City Reputation", t);

                    }
                }
            }
        }
    }
}
