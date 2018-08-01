using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using static System.Convert;
using static Guideviewer.User;

namespace Guideviewer {
    public class Progress {

        public static string FileUserName;

        public string[] Categories = { "", "", "" };

        public void ExtractInsert(string[] userSkillData, User user, int i, bool online)
        {
            if (i < 27)
            {
                if (online)
                {
                    Categories = userSkillData[i].Split(',');
                }
                else if (!online)
                {
                    userSkillData[27] = "";
                    Categories[1] = userSkillData[i].Substring(userSkillData[i].IndexOf(':') + 2);
                }
            }

            //Insert Userdata into arrays for storage
            if (userSkillData[i] != null)
            {
                LoadedSkillLevels[i] = ToInt32(Categories[1]);
            }
            Levels[i] = new Tuple<string, int, int>(SkillNames[i], LoadedSkillLevels[i], LoadedSkillExperiences[i]);
        }

        public static void Save(string userQuestData, string username, User user, StreamWriter sw, string checkboxStringSave)
        {
            string DefOrC = File.Exists($"{username}.txt") ? checkboxStringSave : DefaultIntString;
            saveText(userQuestData, username, sw, DefOrC);
        }

        private static void saveText(string userQuestData, string username, StreamWriter sw, string DefOrC)
        {
            using (sw)
            {
                sw.WriteLine("Username: " + username.Replace(' ', '_') + "\n");
                sw.WriteLine(" ");

                for (var index = 1; index < Levels.Length; index++)
                {
                    var t = Levels[index];
                    sw.WriteLine(t.Item1 + " level: " + t.Item2);
                }

                sw.WriteLine(" ");
                sw.WriteLine(DefOrC);

                sw.WriteLine(" ");
                sw.WriteLine(userQuestData);
            }
        }

        public static void Load(User user) {
            
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true) {
                FileUserName = File.ReadLines(ofd.FileName).Skip(0).Take(1).First();

                string[] userskilldata = new string[SkillNames.Count];

                for (int i = 1; i < SkillNames.Count-1; i++)
                {
                    userskilldata[i] = File.ReadLines(ofd.FileName).Skip(2+i).Take(1).First();
                }

                for (int i = 1; i < Levels.Length; i++)
                {
                    string v = File.ReadLines(ofd.FileName).Skip(i + 2).Take(1).First();
                    LoadedSkillLevels[i] = ToInt32(v.Substring(v.IndexOf(": ", 3, StringComparison.Ordinal) + 2));
                    Levels[i] = new Tuple<string, int, int>(SkillNames[i], LoadedSkillLevels[i], LoadedSkillExperiences[i]);
                }

                LoadUser(File.ReadLines(ofd.FileName).Skip(33).Take(1).First(), userskilldata, user, false);
            }
        }
    }
}