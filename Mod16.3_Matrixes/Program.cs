using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mod16._3_Matrixes
{
    /// <summary>
    /// Умножение матриц больших размерностей в параллельном режиме
    /// </summary>
    internal class Program
    {
        private static Random random = new Random();
        private static int rows = 1000;
        private static int columns = 2000;
        private static int columns2 = 30000;
        private static int[,] matrix1;
        private static int[,] matrix2;
        private static int count = 1;

        private static void Main(string[] args)
        {
            matrix1 = new int[rows, columns];
            matrix2 = new int[columns, columns2];

            var task1 = Task.Run(() => FillMatrix(matrix1));
            var task2 = Task.Run(() => FillMatrix(matrix2));
            Task.WaitAll(task1, task2);
            Console.WriteLine("Sync\n");
            int i = 0;
            do
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var result = MatrixMultiplicationSync(matrix1, matrix2);
                //var result = MatrixMultiplicationSync(new int[2, 3] { { 0, -1, -1 }, { 1, -3, -3 } }, new int[3, 2] { { -1, -1 }, { 2, 3 }, { -2, 0 } });
                sw.Stop();
                Console.WriteLine($"Затрачено {sw.ElapsedMilliseconds}");
                i++;
            } while (i < count);

            Console.WriteLine();
            Console.WriteLine("Async Tasks\n");
            i = 1;
            do
            {
                Stopwatch sw2 = new Stopwatch();
                sw2.Start();
                var result = MatrixMultiplicationAsync(matrix1, matrix2);
                //var result = MatrixMultiplicationAsync(new int[2, 3] { { 0, -1, -1 }, { 1, -3, -3 } }, new int[3, 2] { { -1, -1 }, { 2, 3 }, { -2, 0 } });
                sw2.Stop();
                Console.WriteLine($"Затрачено {sw2.ElapsedMilliseconds}");

                i++;
            } while (i < count);

            Console.WriteLine();

            Console.WriteLine("Async Parallel\n");
            i = 1;
            do
            {
                Stopwatch sw2 = new Stopwatch();
                sw2.Start();
                var result = MatrixMultiplicationParallel(matrix1, matrix2);
                //var result = MatrixMultiplicationParallel(new int[2, 3] { { 0, -1, -1 }, { 1, -3, -3 } }, new int[3, 2] { { -1, -1 }, { 2, 3 }, { -2, 0 } });
                sw2.Stop();
                Console.WriteLine($"Затрачено {sw2.ElapsedMilliseconds}");

                i++;
            } while (i < count);

            Console.WriteLine();
            Console.ReadLine();
        }

        /// <summary>
        /// Заполнение матрицы псевдослучайными числами
        /// </summary>
        /// <param name="array">Массив для заполнения</param>
        public static void FillMatrix(int[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);
            //Parallel.For(0, rows, FillRows);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    array[i, j] = random.Next(-3, 4);
        }

        /// <summary>
        /// Перемножение матриц
        /// </summary>
        /// <param name="array1">Матрица 1</param>
        /// <param name="array2">Матрица 2</param>
        /// <param name="row1">Число строк в матрице 1</param>
        /// <param name="column2">Число столбцов в матрице 2</param>
        /// <returns>Результат умножения двух матриц</returns>
        private static int[,] MatrixMultiplicationAsync(int[,] array1, int[,] array2)
        {
            int row1 = array1.GetLength(0);
            int column2 = array2.GetLength(1);
            int[,] newArray = new int[row1, column2];
            int columnsInitial2 = array2.GetLength(0);

            #region Tasks

            int cores = Environment.ProcessorCount - 1;
            int step = row1 / cores;
            int[] ranges = new int[cores + 1];
            for (int i = 0; i < ranges.Length; i++)
            {
                ranges[i] = i * step;
            }
            if (row1 % cores != 0)
            {
                ranges[cores] = row1;
            }

            var tasks = new List<Task>(cores);
            for (int i = 0; i < cores; i++)
            {
                int start = ranges[i];
                int end = ranges[i + 1];
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int row = start; row < end; row++)
                    {
                        for (int j = 0; j < column2; j++) //проход цикла по столбцам матрицы 1
                        {
                            newArray[row, j] = 0;    // обнуление суммы
                            for (int k = 0; k < columnsInitial2; k++)   //проход цикла по столбцам матрицы 2
                            {
                                newArray[row, j] += array1[row, k] * array2[k, j];  //прибавление к сумме расчитанного значения
                            }
                            //newArray[row, j] = sum;   //присвоение элементу результирующей матрицы значения суммы
                        }
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());

            #endregion Tasks

            return newArray;    //возврат массива
        }

        /// <summary>
        /// Перемножение матриц
        /// </summary>
        /// <param name="array1">Матрица 1</param>
        /// <param name="array2">Матрица 2</param>
        /// <param name="row1">Число строк в матрице 1</param>
        /// <param name="column2">Число столбцов в матрице 2</param>
        /// <returns>Результат умножения двух матриц</returns>
        private static int[,] MatrixMultiplicationParallel(int[,] array1, int[,] array2)
        {
            int row1 = array1.GetLength(0);
            int column2 = array2.GetLength(1);
            int[,] newArray = new int[row1, column2];
            int columnsInitial2 = array2.GetLength(0);

            #region Parallel

            Parallel.For(0, row1, (row) =>
            {
                for (int j = 0; j < column2; j++) //проход цикла по столбцам матрицы 1
                {
                    newArray[row, j] = 0;    // обнуление суммы
                    for (int k = 0; k < columnsInitial2; k++)   //проход цикла по столбцам матрицы 2
                        newArray[row, j] += array1[row, k] * array2[k, j];  //прибавление к сумме расчитанного значения
                }
            });

            #endregion Parallel

            return newArray;    //возврат массива
        }

        private static int[,] MatrixMultiplicationSync(int[,] array1, int[,] array2)
        {
            int row1 = array1.GetLength(0);
            int column2 = array2.GetLength(1);
            int[,] newArray = new int[row1, column2];
            int columnsInitial2 = array2.GetLength(0);

            for (int i = 0; i < row1; i++) //проход цикла по строкам матрицы 1
            {
                for (int j = 0; j < column2; j++) //проход цикла по столбцам матрицы 1
                {
                    newArray[i, j] = 0;    // обнуление суммы
                    for (int k = 0; k < columnsInitial2; k++)   //проход цикла по столбцам матрицы 2
                        newArray[i, j] += array1[i, k] * array2[k, j];  //прибавление к сумме расчитанного значения
                }
            }
            return newArray;    //возврат массива
        }
    }
}