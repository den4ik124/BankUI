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

        public decimal GetBalanceAtMonth(int monthCount)
        {
            decimal noCapitalizationResult = _startBalance;
            if (monthCount > 12)
                noCapitalizationResult = _startBalance * (decimal)Math.Pow((1 + _interestRateYear / 100), monthCount / 12);

            return _isCapitalization ? _startBalance * (decimal)Math.Pow((1 + _interestRateYear / 12 / 100), monthCount) : noCapitalizationResult;
        }

        #endregion Methods
    }
}