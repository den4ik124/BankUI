using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankUI.Models
{
    //public class  Account<T>
    public class AccountModel : INotifyPropertyChanged
    {
        #region Fields

        private string _id;
        private static int _nextId = 1;
        private decimal _balance;
        private DateTime _dateOfCreation;
        private ClientModel _clientData;
        private DepositModel _deposit;

        #endregion Fields

        #region Constructors

        public AccountModel(ClientModel client, decimal balance = 0)
        {
            _id = "_A" + client.Id.ToString() + $"|{_nextId++}";
            this._balance = balance;
            DateOfCreation = DateTime.Now;
            _clientData = client;
            _deposit = null;
            //TODO добавить кредитование
            //_credit = null;
        }

        #endregion Constructors

        #region Properties

        public string Id { get => _id; set => _id = value; }
        public decimal Balance { get => _balance; set => _balance = value; }
        public DateTime DateOfCreation { get => _dateOfCreation; set => _dateOfCreation = value; }
        public ClientModel ClientData { get => _clientData; set => _clientData = value; }

        public DepositModel Deposit
        {
            get => _deposit;
            set
            {
                if (_deposit == value)
                    return;
                _deposit = value;
            }
        }

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #endregion Properties

        #region Methods

        public void ChangeBalance(double value)
        {
            _balance += (decimal)value;
        }

        public void OpenDeposit(decimal startBalance, int duration, double interestRateYear)
        {
            _deposit = new DepositModel(startBalance, duration, interestRateYear);
        }

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion Methods
    }
}