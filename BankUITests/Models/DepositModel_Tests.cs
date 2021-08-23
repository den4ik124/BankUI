using BankUI.Models.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankUI.Models.Tests
{
    [TestClass()]
    public class DepositModel_Tests
    {
        [TestMethod()]
        public void GetBalanceAtMonth_100_112_68_Test()
        {
            //Arrange
            decimal startBalance = 100;
            int duration = 12;
            double interestRestYear = 12;
            DepositAccountModel deposit = new DepositAccountModel(-1, startBalance, interestRestYear, duration, true);

            decimal expected = 112.6825M;
            double delta = 0.001;
            //Act

            decimal result = deposit.GetBalanceAtMonth(duration);

            //Assert

            Assert.AreEqual((double)expected, (double)result, delta);
        }

        [TestMethod()]
        public void GetBalanceAtMonth_100_100_Bad_Test()
        {
            //Arrange
            decimal startBalance = 100;
            int duration = 12;
            double interestRestYear = 12;
            DepositAccountModel deposit = new DepositAccountModel(-1, startBalance, interestRestYear, duration, false);

            decimal expected = 100;
            double delta = 0.001;
            //Act

            decimal result = deposit.GetBalanceAtMonth(11);

            //Assert

            Assert.AreEqual((double)expected, (double)result, delta);
        }

        [TestMethod()]
        public void GetBalanceAtMonth_100_125_Good_Test()
        {
            //Arrange
            decimal startBalance = 100;
            int duration = 12;
            double interestRestYear = 12;
            DepositAccountModel deposit = new DepositAccountModel(-1, startBalance, interestRestYear, duration, false);

            decimal expected = 125.44M;
            double delta = 0.01;
            //Act

            decimal result = deposit.GetBalanceAtMonth(24);

            //Assert

            Assert.AreEqual((double)expected, (double)result, delta);
        }
    }
}