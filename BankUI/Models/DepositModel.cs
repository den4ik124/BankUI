using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models
{
    public class DepositModel
    {
        #region Fields

        private decimal _startBalance;
        private double _interestRateYear;
        private int _depositDuration;
        private DateTime _depositOpened;
        private bool _isCapitalization;

        #endregion Fields

        #region Constructors

        public DepositModel(decimal startBalance, int duration, double interestRateYear, bool isCapitalization = false)
        {
            _startBalance = startBalance;
            _depositDuration = duration;
            _interestRateYear = interestRateYear;
            _depositOpened = DateTime.Now;
            _isCapitalization = isCapitalization;
        }

        #endregion Constructors

        #region Properties

        public decimal StartBalance { get; set; }
        public int DepositLength { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Получение баланса счета через определенное число месяцев
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета</returns>
        public decimal GetBalanceAtMonth(int monthCount)
        {
            return _isCapitalization ? Capitalization(monthCount) : NoCapitalization(monthCount);
        }

        /// <summary>
        /// Расчет баланса счета с капитализацией
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета с капитализацией</returns>
        private decimal Capitalization(int monthCount)
        {
            return _startBalance * (decimal)Math.Pow((1 + _interestRateYear / 12 / 100), monthCount);
        }

        /// <summary>
        /// Расчет баланса счета без капитализации
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета с капитализацией</returns>
        private decimal NoCapitalization(int monthCount)
        {
            return monthCount >= 12 ? _startBalance * (decimal)Math.Pow((1 + _interestRateYear / 100), monthCount / 12) : _startBalance;
        }

        #endregion Methods
    }
}