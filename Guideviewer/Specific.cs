using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Guideviewer.MainWindow;

namespace Guideviewer {
    class Specific {
        public static void Remover(string main, string second, Quest t)
        {
            if (t.Title == main && t.Status == Status.Completed)
            {
                for (int h = 0; h < ColumnList[0].Length; h++)
                {
                    if (ColumnList[0][h] == second)
                    {
                        ColumnList[0][h] = ColumnList[0][h].Remove(0);

                        if (ColumnList[1][h] != "")
                        {
                            ColumnList[1][h] = ColumnList[1][h].Remove(0);
                        }
                    }
                }
            }
        }

        public static void CheckBoxRemover(Dictionary<string, bool> checkboxesBoolDictionary, CheckBox cb, string shortString, string longString)
        {
            if (checkboxesBoolDictionary.TryGetValue(cb.Name, out bool isTrue))
            {
                for (int i = ColumnList.Count - 1; i >= 2; i--)
                {
                    for (int j = ColumnList[0].Length - 1; j >= 0; j--)
                    {
                        if (string.Equals(cb.Name.ToLower(), shortString.ToLower(), StringComparison.Ordinal) && isTrue && ColumnList[i][j].Contains(longString)) {
                            
                            ColumnList[i][j] = ColumnList[i][j].Remove(0);
                        }
                    }
                }
            }
        }
    }
}
