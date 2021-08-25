using BankUI.Models.Accounts;
using Benchmark;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<MyBenchmarkTest>();
            Console.ReadLine();
        }
    }
}