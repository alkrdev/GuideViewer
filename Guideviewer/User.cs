using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static Guideviewer.MainWindow;

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
        public static int[] LoadedSkillLevels = new int[SkillNames.Count];
        public static int[] LoadedSkillExperiences = new int[SkillNames.Count];
        
        //Combination of the users data
        public static Tuple<string, int, int>[] Levels = new Tuple<string, int, int>[SkillNames.Count];
        
        public static Progress Pr = new Progress();

        public static int[] DefaultCbList = new int[512];
        public static int[] DefaultIntArray = DefaultCbList.Select(i => 1).ToArray();
        public static string DefaultIntString = DefaultIntArray.ToString();

        public static void LoadUser(string userquestData, string[] userskillData, User user, bool online) {
            //Loop through the amount of skills
            for (int i = 1; i < SkillNames.Count; i++)
            {
                Pr.ExtractInsert(userskillData, user, i, online);
            }
            //Loop through list of current data
            for (var index = 1; index < ColumnList[0].Length; index++)
            {
                //If any of the looped through data contains "[Train" - Very specific
                if (ColumnList[0][index].StartsWith("[Train"))
                {
                    for (int b = 0; b < SkillNames.Count; b++) {
                    
                    //Creates a string that can look like: "Attack to " - With specification on spaces
                    string combined = SkillNames[b] + " to ";

                        if (ColumnList[0][index].Contains(combined)) {
                            
                            //Create a new string that starts with "Attack to ", and has a problematic number added to it - Example: "Attack to 96]"
                            string extract = combined + ColumnList[0][index].Substring(
                                                 ColumnList[0][index].IndexOf(combined, 3, StringComparison.Ordinal) +
                                                 combined.Length, 3);

                            if (extract.EndsWith("]")) {
                                extract = extract.Remove(extract.LastIndexOf(']'), 1); //Remove the problematic symbol "]" - End of string
                            }

                            //If the userdatas level is bigger than what I am expecting, do the following:
                            if (Convert.ToInt32(extract.Substring(extract.IndexOf(combined, 1, StringComparison.Ordinal) + combined.Length).Replace(" ", "")) <= Levels[b].Item2) {
                                ColumnList[0][index] = ColumnList[0][index].Replace(extract, "");
                            }

                            foreach (var t in Quests.FromJson(userquestData).QuestsList) {
                                for (int j = 0; j < ColumnList[0].Length; j++) {

                                    if (t.Title == ColumnList[0][j] && t.Status == Status.Completed) {
                                        Specific.Remover("Scorpion Catcher", "Barcrawl Miniquest", t);
                                        Specific.Remover("Nomad's Requiem", "Soul Wars Tutorial", t);
                                        Specific.Remover("Children of Mah", "Koschei's Troubles miniquest", t);
                                        Specific.Remover("While Guthix Sleeps", "Chaos Tunnels: Hunt for Surok miniquest", t);
                                        Specific.Remover("Crocodile Tears", "Tier 3 Menaphos City Reputation", t);
                                        Specific.Remover("Our Man in the North", "Tier 6 Menaphos City Reputation", t);
                                        Specific.Remover("'Phite Club", "Tier 9 Menaphos City Reputation", t);

                                        ColumnList[0][j] = ColumnList[0][j].Remove(0);
                                        if (ColumnList[1][j] != "") {
                                            ColumnList[1][j] = ColumnList[1][j].Remove(0);
                            }}}}
                            
                            //Cleanup Switch-statement
                            if (ColumnList[0][index] == "[Train ]") {
                                ColumnList[0][index] = ColumnList[0][index].Replace(ColumnList[0][index], "");
                            }


                            foreach (var col in ColumnList) {
                                for (var index1 = 0; index1 < ColumnList[0].Length; index1++) {
                                    if (col[index1] == " " && col[index1] != "") {
                                        col[index1] = col[index1].Remove(0);
        }}}}}}}}}
}