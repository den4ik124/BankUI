﻿using System;
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
    public class MyParserBenchmark
    {
        private BankUI.Models.Accounts.DepositAccountModel depositAccount;

        [Benchmark]
        public void CapitalizationTest()
        {
            depositAccount = new BankUI.Models.Accounts.DepositAccountModel(1, 100, 12, 12, true);
            depositAccount.GetBalanceAtMonth(5);
        }

        [Benchmark]
        public void NoCapitalizationTest()
        {
            depositAccount = new BankUI.Models.Accounts.DepositAccountModel(1, 100, 12, 12, false);
            depositAccount.GetBalanceAtMonth(5);
        }
    }
}