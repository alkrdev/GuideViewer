using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Guideviewer {
    class Specific {
        public void PrerequisiteRemover(string main, string second, Quest t)
        {
            if (t.Title == main && t.Status == Status.Completed)
            {
                for (int h = 0; h < MainWindow.ColumnList[0].Length; h++)
                {
                    if (MainWindow.ColumnList[0][h] == second)
                    {
                        MainWindow.ColumnList[0][h] = MainWindow.ColumnList[0][h].Remove(0);

                        if (MainWindow.ColumnList[1][h] != "")
                        {
                            MainWindow.ColumnList[1][h] = MainWindow.ColumnList[1][h].Remove(0);
                        }
                    }
                }
            }
        }

        public void CheckBoxRemover(Dictionary<string, bool> checkboxesBoolDictionary, CheckBox cb, string shortString, string longString)
        {
            if (checkboxesBoolDictionary.TryGetValue(cb.Name, out bool isTrue))
            {
                for (int i = MainWindow.ColumnList.Count - 1; i >= 2; i--)
                {
                    for (int j = MainWindow.ColumnList[0].Length - 1; j >= 0; j--)
                    {
                        if (string.Equals(cb.Name.ToLower(), shortString.ToLower(), StringComparison.Ordinal) && isTrue && MainWindow.ColumnList[i][j].Contains(longString)) {

							MainWindow.ColumnList[i][j] = MainWindow.ColumnList[i][j].Remove(0);
                        }
                    }
                }
            }
        }
    }
}
