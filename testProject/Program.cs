using BankUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PersonModel test = new PersonModel();
            decimal startBalance = 1000;
            int duration = 7;
            double interestRateYear = 8.5;
            test.OpenDeposit(startBalance, duration, interestRateYear);

            var result = test.Deposit.GetBalanceAtMonth(duration);
            Console.WriteLine();
        }
    }
}