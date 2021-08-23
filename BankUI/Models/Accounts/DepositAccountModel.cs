using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models.Accounts
{
    public class DepositAccountModel : AccountBaseModel
    {
        //private decimal _startBalance;
        private double _interestRateYear;

        private int _depositDuration;
        private bool _isCapitalization;

        public DepositAccountModel(int clientID, decimal balance, double interestRateYear, int depositDuration, bool isCapitalization)
            : base(clientID, balance)
        {
            Id = $"_D{clientID}|{_nextId++}";
            //_startBalance = base.Balance;
            _interestRateYear = interestRateYear;
            _depositDuration = depositDuration;
            _isCapitalization = isCapitalization;
        }

        #region Methods

        /// <summary>
        /// Получение баланса счета через определенное число месяцев
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета</returns>
        public decimal GetBalanceAtMonth(int monthCount) =>
            _isCapitalization ? Capitalization(monthCount) : NoCapitalization(monthCount);

        /// <summary>
        /// Расчет баланса счета с капитализацией
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета с капитализацией</returns>
        private decimal Capitalization(int monthCount) =>
            Balance * (decimal)Math.Pow(1 + _interestRateYear / 12 / 100, monthCount);

        /// <summary>
        /// Расчет баланса счета без капитализации
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета с капитализацией</returns>
        private decimal NoCapitalization(int monthCount) =>
            monthCount >= 12 ? Balance * (decimal)Math.Pow(1 + _interestRateYear / 100, monthCount / 12) : Balance;

        #endregion Methods
    }
}