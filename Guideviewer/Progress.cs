using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace Guideviewer {
    public class Progress {

        public static string FileUserName;

        public string[] Categories = {
            "", "", ""
        };

        public void ExtractInsert(string[] userSkillData, User user, int i, bool online) {
            if (i < 27) {
                if (online) {
                    //Extract Userdata and seperate by ","
                    Categories = userSkillData[i].Split(',');
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                } else if (!online) {
                    userSkillData[27] = "";
                    Categories[1] = userSkillData[i].Substring(userSkillData[i].IndexOf(':') + 2);
                }
            }

                //Insert Userdata into arrays for storage
                if (userSkillData[i] != null) {
                    User.LoadedSkillLevels[i] = Convert.ToInt32(Categories[1]);
                }
                User.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], User.LoadedSkillLevels[i], User.LoadedSkillExperiences[i]);
            
        }


        public void Save(string userQuestData, string username, User user, StreamWriter sw) {

            sw.WriteLine("Username: " + username.Replace(' ', '_') + "\n");
            sw.WriteLine(" ");
            for (var index = 1; index < User.Levels.Length; index++) {
                var t = User.Levels[index];
                sw.WriteLine(t.Item1 + " level: " + t.Item2);
            }

            sw.WriteLine(" ");
            sw.WriteLine(userQuestData);

        sw.Dispose();
        }

        public static void Load(User user) {
            
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true) {
                FileUserName = File.ReadLines(ofd.FileName).Skip(0).Take(1).First();

                string userquestdata = File.ReadLines(ofd.FileName).Skip(31).Take(1).First();
                string[] userskilldata = new string[User.SkillNames.Count];
                for (int i = 1; i < User.SkillNames.Count-1; i++) {
                    userskilldata[i] = File.ReadLines(ofd.FileName).Skip(2+i).Take(1).First();
                }

                for (int i = 1; i < User.Levels.Length; i++) {
                    User.LoadedSkillLevels[i] = Convert.ToInt32(File.ReadLines(ofd.FileName).Skip(i+2).Take(1).First().Substring(File.ReadLines(ofd.FileName).Skip(i+2).Take(1).First().IndexOf(": ", 3, StringComparison.Ordinal) + 2));
                    User.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], User.LoadedSkillLevels[i], User.LoadedSkillExperiences[i]);
                }

                User.Load(userquestdata, userskilldata, user, false);
                
            }
        }
    }
}
