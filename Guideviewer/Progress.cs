using System;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace Guideviewer {
    public class Progress {
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
    }
}
