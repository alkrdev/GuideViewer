using static Guideviewer.MainWindow;

namespace Guideviewer {
    class Specific {
        public static void Remover(string main, string second, Quest t) {
            if (t.Title == main && t.Status == Status.Completed) {
                for (int h = 0; h < ColumnList[0].Length; h++) {
                    if (ColumnList[0][h] == second) {
                        ColumnList[0][h] = ColumnList[0][h].Remove(0);
                        if (ColumnList[1][h] != "") {
                            ColumnList[1][h] = ColumnList[1][h].Remove(0);
                        }
                    }
                }
            }
        }

        public string MyClass1(int rating) {
            return "rating: " + rating;
        }
    }
}
