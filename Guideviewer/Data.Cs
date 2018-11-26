using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Guideviewer
{
    class Data
    {
        // Dictionary to keep track of all CheckBoxes
        public static Dictionary<string, List<CheckBox>>CheckboxesDictionary = new Dictionary<string, List<CheckBox>>();
        // Dictionary to keep track of all bool values of CheckBoxes
        public static Dictionary<string, bool> CheckboxesBoolDictionary = new Dictionary<string, bool>();
        // List of all "Select All" Checkboxes
        public static List<Tuple<CheckBox, ListView>> ListViewSelectAllList = new List<Tuple<CheckBox, ListView>>();
        // List of tuples that contain almost identical names
        public static List<Tuple<string, string>> NameCompareTuples = new List<Tuple<string, string>>();
        // List of all "Select All" Checkboxes
        public static List<CheckBox> SelectAllCheckBoxes = new List<CheckBox>();
        // List of all Checkboxes
        public static List<CheckBox> AllCheckBoxes = new List<CheckBox>();
        // List of all Listviews
        public static List<ListView> ListViews = new List<ListView>();        
    }
    class User
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

        //Prerequisites
        public static Tuple<string, string>[] PrerequisiteTuples =
        {
            new Tuple<string, string>("Scorpion Catcher", "Barcrawl Miniquest"),
            new Tuple<string, string>("Nomad's Requiem", "Soul Wars Tutorial"),
            new Tuple<string, string>("Children of Mah", "Koschei's Troubles miniquest"),
            new Tuple<string, string>("While Guthix Sleeps", "Chaos Tunnels: Hunt for Surok miniquest"),
            new Tuple<string, string>("Crocodile Tears", "Tier 3 Menaphos City Reputation"),
            new Tuple<string, string>("Our Man in the North", "Tier 6 Menaphos City Reputation"),
            new Tuple<string, string>("'Phite Club", "Tier 9 Menaphos City Reputation")
        };
        
        //Defaults
        public static int[] DefaultCbList = new int[512];
        public static int[] DefaultIntArray = DefaultCbList.Select(i => 0).ToArray();
        public static string DefaultIntArrayString = string.Join(",", DefaultIntArray);
    }
}

