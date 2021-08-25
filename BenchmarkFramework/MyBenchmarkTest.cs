using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BankUI;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    public class MyBenchmarkTest
    {
        [Benchmark]
        public void CapitalizationTest()
        {
            _ = new BankUI.Models.Accounts.DepositAccountModel(1, 100, 12, 12, true).GetBalanceAtMonth(5);
        }

        [Benchmark]
        public void NoCapitalizationTest()
        {
            _ = new BankUI.Models.Accounts.DepositAccountModel(1, 100, 12, 12, false).GetBalanceAtMonth(5);
        }
    }
}