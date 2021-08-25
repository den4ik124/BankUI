using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BankUI;
using BankUI.HelpClasses;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    public class MyBenchmarkTest
    {
        private Levenshtein levenshtein = new Levenshtein();

        [Benchmark]
        public void LevenshteinHorVert()
        {
            var test = levenshtein.FindDistance("молоко", "колокол");
        }

        [Benchmark]
        public void LevenshteinVertHor()
        {
            var test = levenshtein.FindDistance("колокол", "молоко");
        }

        //[Benchmark]
        //public void CapitalizationTest()
        //{
        //    _ = new BankUI.Models.Accounts.DepositAccountModel(1, 100, 12, 12, true).GetBalanceAtMonth(5);
        //}

        //[Benchmark]
        //public void NoCapitalizationTest()
        //{
        //    _ = new BankUI.Models.Accounts.DepositAccountModel(1, 100, 12, 12, false).GetBalanceAtMonth(5);
        //}
    }
}