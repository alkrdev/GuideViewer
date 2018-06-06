using System;
using System.Collections.Generic;

namespace Guideviewer {
    public class User {
        
        //List of all skillnames in the game
        public static List<string> SkillNames = new List<string> {
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

        //Storage of  the users data
        public int[] LoadedSkillLevels = new int[SkillNames.Count];
        public int[] LoadedSkillExperiences = new int[SkillNames.Count];

        //Combination of the users data
        public Tuple<string, int, int>[] Levels = new Tuple<string, int, int>[SkillNames.Count];

    }
}