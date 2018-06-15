using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace Guideviewer {
    public class Progress {

        public static string FileUserName;

        public void ExtractInsert(string[] userSkillData, User user, int i) {
            //Extract Userdata and seperate by ","
            var categories = userSkillData[i].Split(',');
                        
            //Insert Userdata into arrays for storage
            user.LoadedSkillLevels[i] = Convert.ToInt32(categories[1]);
            user.LoadedSkillExperiences[i] = Convert.ToInt32(categories[2]);

            user.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], user.LoadedSkillLevels[i], user.LoadedSkillExperiences[i]);
        }


        public void Save(string userQuestData, string username, User user, StreamWriter sw) {

            sw.WriteLine("Username: " + username.Replace(' ', '_') + "\n");
            sw.WriteLine(" ");
            for (var index = 1; index < user.Levels.Length; index++) {
                var t = user.Levels[index];
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
            }

            for (int i = 1; i < user.Levels.Length; i++) {
                user.LoadedSkillLevels[i] = Convert.ToInt32(File.ReadLines(ofd.FileName).Skip(i+2).Take(1).First().Substring(File.ReadLines(ofd.FileName).Skip(i+2).Take(1).First().IndexOf(": ", 3, StringComparison.Ordinal) + 2));
                user.Levels[i] = new Tuple<string, int, int>(User.SkillNames[i], user.LoadedSkillLevels[i], user.LoadedSkillExperiences[i]);
            }
            
        }
    }
}
