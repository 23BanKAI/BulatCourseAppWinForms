using System;
using System.Drawing.Drawing2D;
namespace BulatCourseAppWinForms;
class HungarianAlgorithm
{
    public void Algorithm(int[,] matrix)
    {
        // Матрица


        // Получаем размеры матрицы
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        // Инициализировать массивы для сокращения строк и столбцов, а также для отслеживания назначения
        int[] u = new int[n + 1];
        int[] v = new int[m + 1];
        int[] p = new int[m + 1];
        int[] way = new int[m + 1];

        // Для каждой строки в матрице
        for (int i = 1; i <= n; ++i)
        {
            // Устанавливаем первый элемент массива отслеживания
            p[0] = i;
            // Инициализируем переменные для нахождения минимального значения в строке
            int j0 = 0;
            int[] minv = new int[m + 1];
            bool[] used = new bool[m + 1];
            for (int j = 1; j <= m; ++j)
            {
                minv[j] = int.MaxValue;
                used[j] = false;
            }
            // Находим минимальное значение в каждом еще не использованном столбце
            do
            {
                used[j0] = true;
                int i0 = p[j0], delta = int.MaxValue, j1 = 0;
                for (int j = 1; j <= m; ++j)
                {
                    if (!used[j])
                    {
                        int cur = matrix[i0 - 1, j - 1] - u[i0] - v[j];
                        if (cur < minv[j])
                        {
                            minv[j] = cur;
                            way[j] = j0;
                        }
                        if (minv[j] < delta)
                        {
                            delta = minv[j];
                            j1 = j;
                        }
                    }
                }
                // Обновляем соркащенные массивы
                for (int j = 0; j <= m; ++j)
                {
                    if (used[j])
                    {
                        u[p[j]] += delta;
                        v[j] -= delta;
                    }
                    else
                    {
                        minv[j] -= delta;
                    }
                }
                j0 = j1;
            } while (p[j0] != 0);

            // Обновляем массив отслеживания c присваиванием
            do
            {
                int j1 = way[j0];
                p[j0] = p[j1];
                j0 = j1;
            } while (j0 != 0);
        }

        // Извлекаем присваивание из массива отслеживания
        int[] ans = new int[n];
        for (int j = 1; j <= m; ++j)
        {
            if (p[j] > 0)
            {
                ans[p[j] - 1] = j - 1;
            }
        }


        string textToFile = "Optimal assignment:\n";

       
        // Выводим оптимальное назначение

        for (int i = 0; i < n; ++i)
        {
            textToFile += ($"Worker {(i + 1)} is assigned to job {(ans[i] + 1)}\n");
        }

        int routeLength = 0;
        for (int i = 0; i < n; i++)
        {
            int j = ans[i];
            if (j >= 0 && j < m)
            {
                routeLength += matrix[i, j];
            }
        }
        textToFile += ($"Optimal assignment route length: {routeLength}\n");

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Save file";
        saveFileDialog.Filter = "Text files (*.txt)|*.txt";
        saveFileDialog.DefaultExt = "txt";

        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            string filePath = saveFileDialog.FileName;
            File.WriteAllText(filePath, textToFile);
        }
    }

}