using BankUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.HelpClasses
{
    public class Levenshtein : IDistanceMetric
    {
        /// <summary>
        /// Расчет "редакционного расстояния"
        /// </summary>
        /// <param name="horizontal">Слово по-горизонтали</param>
        /// <param name="vertical">Слово по-вертикали</param>
        /// <returns>Значение "редакционного расстояния" = сколько правок нужно сделать, чтобы слова были идентичны</returns>
        public int FindDistance(string horizontal, string vertical)
        {
            horizontal.ToLower();
            vertical.ToLower();
            int[,] array = new int[vertical.Length + 1, horizontal.Length + 1];
            int[] tempData;
            for (int i = 0; i <= vertical.Length; i++)
            {
                for (int j = 0; j <= horizontal.Length; j++)
                {
                    if (i == 0)
                    {
                        array[i, j] = j;
                        continue;
                    }
                    if (j == 0)
                    {
                        array[i, j] = i;
                        continue;
                    }
                    if (vertical[i - 1] == horizontal[j - 1])
                    {
                        array[i, j] = array[i - 1, j - 1];
                        continue;
                    }
                    tempData = new int[] { array[i, j - 1], array[i - 1, j], array[i - 1, j - 1] };
                    array[i, j] = tempData.Min() + 1;
                }
            }
            return array[vertical.Length, horizontal.Length];
        }
    }
}