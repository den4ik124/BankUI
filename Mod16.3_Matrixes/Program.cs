using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mod16._3_Matrixes
{
    internal class Program
    {
        private static Random random = new Random();
        private static int rows = 1000;
        private static int columns = 2000;
        private static int columns2 = 3000;
        private static int[,] matrix1;
        private static int[,] matrix2;

        private static void Main(string[] args)
        {
            matrix1 = new int[rows, columns];
            matrix2 = new int[columns, columns2];

            var task1 = Task.Run(() => FillMatrix(matrix1));
            var task2 = Task.Run(() => FillMatrix(matrix2));
            Task.WaitAll(task1, task2);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = MatrixMultiplication(matrix1, matrix2, matrix1.GetLength(0), matrix2.GetLength(1));
            sw.Stop();
            Console.WriteLine($"Затрачено {sw.ElapsedMilliseconds}");
            Console.WriteLine();
        }

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
        private static int[,] MatrixMultiplication(int[,] array1, int[,] array2, int row1, int column2)
        {
            int[,] newArray = new int[row1, column2];
            int sum;
            int rows1 = newArray.GetLength(0);
            int columns1 = newArray.GetLength(1);
            int columnsInitial2 = array2.GetLength(0);
            for (int i = 0; i < rows1; i++) //проход цикла по строкам матрицы 1
            {
                for (int j = 0; j < columns1; j++) //проход цикла по столбцам матрицы 1
                {
                    sum = 0;    // обнуление суммы
                    for (int k = 0; k < columnsInitial2; k++)   //проход цикла по столбцам матрицы 2
                    {
                        newArray[i, j] = array1[i, k] * array2[k, j];   //расчет элемента суммы
                        sum += newArray[i, j];  //прибавление к сумме расчитанного значения
                    }
                    newArray[i, j] = sum;   //присвоение элементу результирующей матрицы значения суммы
                }
            }
            return newArray;    //возврат массива
        }
    }
}