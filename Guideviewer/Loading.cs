using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Guideviewer.MainWindow;

namespace Guideviewer
{
    public class Loading
    {
        protected static Progress Pr = new Progress();
   

        public static void LoadUser(string userquestData, string[] userskillData, User user, bool online)
        {

            //Loop through the amount of skills
            for (int i = 1; i < User.SkillNames.Count; i++)
            {
                Pr.ExtractInsert(userskillData, user, i, online);
            }

            for (int i = 1; i < ColumnList[0].Length; i++)
            {
                string rowData = ColumnList[0][i];
                if (rowData.StartsWith("[Train"))
                {
                    foreach (var skill in User.SkillNames)
                    {
                        if (skill == "Total")
                        {
                            continue;
                        }
                        if (rowData.Contains(skill))
                        {
                            var focusSkill = skill;
                            string distance = "[Train " + focusSkill + " to ";
                            var substring = rowData.Substring(distance.Length);
                            var replace = substring.Replace("]", "");
                            var replaceA = replace.Replace("[OPTIONAL", "");
                            var focusLevel = Convert.ToInt32(replaceA);

                            //If the userdatas level is bigger than what I am expecting, do the following:
                            if (user.SkillsDictionary.TryGetValue(focusSkill, out int value))
                            {
                                if (focusLevel <= value)
                                {
                                    rowData = rowData.Remove(0);
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
                        Specific.Remover("Scorpion Catcher", "Barcrawl Miniquest", t);
                        Specific.Remover("Nomad's Requiem", "Soul Wars Tutorial", t);
                        Specific.Remover("Children of Mah", "Koschei's Troubles miniquest", t);
                        Specific.Remover("While Guthix Sleeps", "Chaos Tunnels: Hunt for Surok miniquest", t);
                        Specific.Remover("Crocodile Tears", "Tier 3 Menaphos City Reputation", t);
                        Specific.Remover("Our Man in the North", "Tier 6 Menaphos City Reputation", t);
                        Specific.Remover("'Phite Club", "Tier 9 Menaphos City Reputation", t);

                        ColumnList[0][j] = ColumnList[0][j].Remove(0);
                        if (ColumnList[1][j] != "")
                        {
                            ColumnList[1][j] = ColumnList[1][j].Remove(0);

                        }

                        foreach (var col in ColumnList)
                        {
                            for (var index1 = 0; index1 < ColumnList[0].Length; index1++)
                            {
                                if (col[index1] == " " && col[index1] != "")
                                {
                                    col[index1] = col[index1].Remove(0);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
