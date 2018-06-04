using System;
using System.Collections.Generic;

namespace Guideviewer {
    public class User {
    
        public List<string> SkillNames = new List<string> {
            "Total", "Attack", "Defense",
            "Strength", "Constitution", "Ranged",
            "Prayer", "Magic", "Cooking",
            "Woodcutting", "Fletching", "Fishing",
            "Firemaking", "Crafting", "Smithing",
            "Mining", "Herblore", "Agility",
            "Thieving", "Slayer", "Farming",
            "Runecrafting", "Hunter", "Construction",
            "Summoning", "Dungeoneering", "Divination",
            "Invention"
        };
        public int[] LoadedSkillLevels = new int[MainWindow.SkillsList.Count];
        public int[] LoadedSkillExperiences = new int[MainWindow.SkillsList.Count];

        public Tuple<string, int, int>[] Levels = new Tuple<string, int, int>[MainWindow.SkillsList.Count];
        public int i;

        public void SaveData() {

            foreach (var s in SkillNames) {
                Levels[i] = new Tuple<string, int, int>(s,LoadedSkillLevels[i],LoadedSkillExperiences[i]);
                i++;
            }
        }
    }
}