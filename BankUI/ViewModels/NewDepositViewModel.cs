using BankUI.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankUI.ViewModels
{
    public class NewDepositViewModel : INotifyPropertyChanged
    {
        #region Fields

        //123
        private decimal _balanceAtMonth;

        private decimal _startBalance;
        private double _interestRateYear;
        private int _depositDuration;
        private DateTime _depositOpened;
        private bool _isCapitalization;

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public NewDepositViewModel()
        {
            //ositModel deposit = new DepositModel();
        }

        #endregion Constructors

        #region Properties

        public decimal StartBalance { get => _startBalance; set => _startBalance = value; }
        public double InterestRateYear { get => _interestRateYear; set => _interestRateYear = value; }
        public int DepositDuration { get => _depositDuration; set => _depositDuration = value; }
        public DateTime DepositOpened { get => _depositOpened; set => _depositOpened = value; }
        public bool IsCapitalization { get => _isCapitalization; set => _isCapitalization = value; }

        public decimal BalanceAtMonth
        {
            get => _balanceAtMonth;
            set
            {
                if (_balanceAtMonth == value)
                    return;
                _balanceAtMonth = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}