using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Mod16_Parallel
{
    internal class Program
    {
        private static object locker = new object();
        public static int _count = 0;
        public static List<int> rangesStart;
        public static List<int> rangesEnd;
        public static List<Task> tasks;

        private static void Main(string[] args)
        {
            int start = 1_000_000_000;
            int end = 2_000_000_000;

            #region TPL try

            //int step = 10_000_000;
            //int portions = 100;
            //int indexS = 0;
            //int indexE = 0;
            //rangesStart = new List<int>(portions);
            //rangesEnd = new List<int>(portions);
            //tasks = new List<Task>(portions);
            //for (int i = 0; i < portions; i++)
            //{
            //    if (i == 0)
            //    {
            //        indexS = start;
            //        indexE = start + step;
            //    }
            //    else
            //    {
            //        indexS = start + i * step + 1;
            //        indexE = rangesEnd[i - 1] + step;
            //    }
            //    rangesStart.Add(indexS);
            //    rangesEnd.Add(indexE);
            //}

            //Stopwatch sw3 = new Stopwatch();
            //sw3.Start();
            //for (int i = 0; i < portions; i++)
            //{
            //    int startIndex = rangesStart[i];
            //    int endIndex = rangesEnd[i];
            //    tasks.Add(Task.Factory.StartNew(() =>
            //   {
            //       for (int j = startIndex; j <= endIndex; j++)
            //           CheckNumberV2(j);
            //   }));
            //}

            //Task.WaitAll(tasks.ToArray());
            //sw3.Stop();
            //Console.WriteLine($"В диапазоне {start / 1e6} млн - {end / 1e6} млн, нашлось {_count} чисел\n" +
            //     $"Было затрачено: {sw3.ElapsedMilliseconds} ms");

            #endregion TPL try

            #region sync

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = start; i < end; i++)
            {
                CheckNumberV2(i);
            };
            sw.Stop();
            Console.WriteLine($"В диапазоне {start / 1e6} млн - {end / 1e6} млн, нашлось {_count} чисел\n" +
                $"Было затрачено: {sw.ElapsedMilliseconds} ms");

            #endregion sync

            #region Parallel

            ParallelOptions parOpt = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
            _count = 0;
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            Parallel.For(start, end, parOpt, CheckNumberV2);
            sw1.Stop();

            Console.WriteLine($"V2\nВ диапазоне {start / 1e6} млн - {end / 1e6} млн, нашлось {_count} чисел\n" +
                $"Было затрачено: {sw1.ElapsedMilliseconds} ms");

            #endregion Parallel

            Console.ReadLine();
        }

        /// <summary>
        /// Проверка кратности суммы цифр числа последней цифре числа: ABC...XYZ => (A+B+C+...+X+Y+Z) % Z==0
        /// </summary>
        /// <param name="value">Число для анализа</param>
        public static void CheckNumberV2(int value)
        {
            if (value % 10 == 0 || value % 10 == 1)
            {
                Interlocked.Increment(ref _count);
                return;
            }
            int sum = 0;
            int lastNumber = value % 10;
            while (value > 0)
            {
                sum += value % 10;
                value /= 10;
            }

            if (sum % lastNumber == 0)
                Interlocked.Increment(ref _count);
        }
    }
}