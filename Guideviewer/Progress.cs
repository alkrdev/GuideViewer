using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using static Guideviewer.User;

namespace Guideviewer {
    public class Progress {
        public static string[] Categories;

        public static void ExtractInsert(string[] userSkillData, bool online)
        {
            SkillsDictionary.Clear();
            for (int i = 1; i < SkillNames.Count; i++) {
                if (online) 
                {
                    Categories = userSkillData[i].Split(',');
                }
                else 
                {
                    userSkillData[SkillNames.Count] = "";
                    Categories[1] = userSkillData[i].Substring(userSkillData[i].IndexOf(':') + 2);
                }

                LoadedSkillLevels[i] = Convert.ToInt32(Categories[1]);

                SkillsDictionary.Add(SkillNames[i], LoadedSkillLevels[i]);
            }
        }

        public static void SaveText(string userQuestData, string username, StreamWriter sw, string checkboxStringSave)
        {
            string DefOrC = File.Exists($"{username}.txt") ? checkboxStringSave : DefaultIntArrayString;

            using (sw)
            {
                sw.WriteLine("Username: " + username.Replace(' ', '_') + "\n");
                sw.WriteLine(" ");

                for (var index = 1; index < Levels.Length; index++)
                {
                    sw.WriteLine(Levels[index].Item1 + " level: " + Levels[index].Item2);
                }

                sw.WriteLine(" ");
                sw.WriteLine(DefOrC);

                sw.WriteLine(" ");
                sw.WriteLine(userQuestData);
            }
        }

        public static void Load() {
            
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true) {

                string[] userskilldata = new string[SkillNames.Count];

                for (int i = 1; i < SkillNames.Count-1; i++)
                {
                    userskilldata[i] = File.ReadLines(ofd.FileName).Skip(2+i).Take(1).First();
                }

                for (int i = 1; i < Levels.Length; i++)
                {
                    string v = File.ReadLines(ofd.FileName).Skip(i + 2).Take(1).First();
                    LoadedSkillLevels[i] = Convert.ToInt32(v.Substring(v.IndexOf(": ", 3, StringComparison.Ordinal) + 2));
                    Levels[i] = new Tuple<string, int, int>(SkillNames[i], LoadedSkillLevels[i], LoadedSkillExperiences[i]);
                }

                Loading.LoadUser(File.ReadLines(ofd.FileName).Skip(33).Take(1).First(), userskilldata, false);
            }
        }
    }
}