using BenchmarkDotNet.Running;
using System;
using BankUI.Models.Accounts;
using System.Diagnostics;

namespace Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                BenchmarkRunner.Run<DepositAccountModel>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка:\n{ex.Message}\n");
            }
        }
    }
}