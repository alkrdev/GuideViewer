using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace Guideviewer {
    public class Progress {
        public string[] Categories;

        public void ExtractInsert(string[] userSkillData, bool online)
        {
            User.SkillsDictionary.Clear();
            for (int i = 1; i < User.SkillNames.Count; i++) {
                if (online) 
                {
                    Categories = userSkillData[i].Split(',');
                }
                else 
                {
                    userSkillData[User.SkillNames.Count] = "";
                    Categories[1] = userSkillData[i].Substring(userSkillData[i].IndexOf(':') + 2);
                }

				User.LoadedSkillLevels[i] = Convert.ToInt32(Categories[1]);

				User.SkillsDictionary.Add(User.SkillNames[i], User.LoadedSkillLevels[i]);
            }
        }

        public void SaveText(string userQuestData, string username, StreamWriter sw, string checkboxStringSave)
        {
            using (sw)
            {
                sw.WriteLine("Username: " + username.Replace(' ', '_') + "\n");
                sw.WriteLine(" ");

                for (var index = 1; index < User.Levels.Length; index++)
                {
                    sw.WriteLine(User.Levels[index].Item1 + " level: " + User.Levels[index].Item2);
                }

                sw.WriteLine(" ");
                sw.WriteLine(File.Exists($"{username}.txt") ? checkboxStringSave : User.DefaultIntArrayString);

                sw.WriteLine(" ");
                sw.WriteLine(userQuestData);
            }
        }

        public static void Load() {
            
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true) {

                string[] userskilldata = new string[User.SkillNames.Count];

                for (int i = 1; i < User.SkillNames.Count-1; i++)
                {
                    userskilldata[i] = File.ReadLines(ofd.FileName).Skip(2+i).Take(1).First();
                }

                for (int i = 1; i < User.Levels.Length; i++)
                {
                    string v = File.ReadLines(ofd.FileName).Skip(i + 2).Take(1).First();
					User.LoadedSkillLevels[i] = Convert.ToInt32(v.Substring(v.IndexOf(": ", 3, StringComparison.Ordinal) + 2));
					User.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], User.LoadedSkillLevels[i], User.LoadedSkillExperiences[i]);
                }

                Loading.LoadUser(File.ReadLines(ofd.FileName).Skip(33).Take(1).First(), userskilldata, false);
            }
        }
    }
}