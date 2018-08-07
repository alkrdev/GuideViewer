using System;
using System.Collections.Generic;
using System.Linq;

namespace Guideviewer
{
    public class User
    {
        //List of all skillnames in the game
        public static List<string> SkillNames = new List<string>
        {
            "Total",
            "Attack",
            "Defense",
            "Strength",
            "Constitution",
            "Ranged",
            "Prayer",
            "Magic",
            "Cooking",
            "Woodcutting",
            "Fletching",
            "Fishing",
            "Firemaking",
            "Crafting",
            "Smithing",
            "Mining",
            "Herblore",
            "Agility",
            "Thieving",
            "Slayer",
            "Farming",
            "Runecrafting",
            "Hunter",
            "Construction",
            "Summoning",
            "Dungeoneering",
            "Divination",
            "Invention"
        };


        public static Dictionary<string, int> SkillsDictionary = new Dictionary<string, int>();
        
        //Storage of  the users data
        public static int[] LoadedSkillLevels = new int[SkillNames.Count];
        public static int[] LoadedSkillExperiences = new int[SkillNames.Count];

        //Combination of the users data
        public static Tuple<string, int, int>[] Levels = new Tuple<string, int, int>[SkillNames.Count];

        public static int[] DefaultCbList = new int[512];
        public static int[] DefaultIntArray = DefaultCbList.Select(i => 0).ToArray();
        public static string DefaultIntArrayString = string.Join(",", DefaultIntArray);
    }
}
