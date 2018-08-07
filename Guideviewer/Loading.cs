using System;
using static Guideviewer.MainWindow;
using static Guideviewer.User;

namespace Guideviewer
{
    public class Loading
    {
   

        public static void LoadUser(string userquestData, string[] userskillData, bool online)
        {
            //Loop through the amount of skills
            Progress.ExtractInsert(userskillData, online);
            
            for (int i = 1; i < ColumnList[0].Length; i++)
            {
                if (ColumnList[0][i].StartsWith("[Train"))
                {
                    foreach (var skill in SkillNames)
                    {
                        if (skill == "Total")
                        {
                            continue;
                        }
                        if (ColumnList[0][i].Contains(skill))
                        {
                            var focusSkill = skill;
                            string distance = "[Train " + focusSkill + " to ";
                            var substring = ColumnList[0][i].Substring(distance.Length);
                            var replace = substring.Replace("]", "");
                            var replaceA = replace.Replace("[OPTIONAL", "");
                            var focusLevel = Convert.ToInt32(replaceA);

                            //If the userdatas level is bigger than what I am expecting, do the following:
                            if (SkillsDictionary.TryGetValue(focusSkill, out int value))
                            {
                                if (focusLevel <= value)
                                {
                                    ColumnList[0][i] = ColumnList[0][i].Remove(0);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var t in Quests.FromJson(userquestData).QuestsList)
            {
                for (int j = 0; j < ColumnList[0].Length; j++)
                {
                    if (t.Title == ColumnList[0][j] && t.Status == Status.Completed)
                    {
                        ColumnList[0][j] = ColumnList[0][j].Remove(0);

                        if (ColumnList[1][j] != "")
                        {
                            ColumnList[1][j] = ColumnList[1][j].Remove(0);

                        }

                        foreach (var col in ColumnList)
                        {
                            for (var i = 0; i < ColumnList[0].Length; i++)
                            {
                                if (col[i] == " " && col[i] != "")
                                {
                                    col[i] = col[i].Remove(0);
                                }
                            }
                        }

                        Specific.Remover("Scorpion Catcher", "Barcrawl Miniquest", t);
                        Specific.Remover("Nomad's Requiem", "Soul Wars Tutorial", t);
                        Specific.Remover("Children of Mah", "Koschei's Troubles miniquest", t);
                        Specific.Remover("While Guthix Sleeps", "Chaos Tunnels: Hunt for Surok miniquest", t);
                        Specific.Remover("Crocodile Tears", "Tier 3 Menaphos City Reputation", t);
                        Specific.Remover("Our Man in the North", "Tier 6 Menaphos City Reputation", t);
                        Specific.Remover("'Phite Club", "Tier 9 Menaphos City Reputation", t);

                    }
                }
            }
        }
    }
}
