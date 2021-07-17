using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankUI.Models
{
    //public class  Account<T>
    public class AccountModel// : INotifyPropertyChanged
    {
        #region Fields

        private string _id;
        private static int _nextId = 1;
        private decimal _balance;
        private DateTime _dateOfCreation;
        private IList<Transaction> _transactionsList;

        //private ClientModel _clientData;
        private int _hostID;

        private DepositModel _deposit;

        #endregion Fields

        #region Constructors

        [JsonConstructor]
        public AccountModel() { }

        public AccountModel(ClientModel client, decimal balance = 0)
        {
            _id = "_A" + client.Id.ToString() + $"|{_nextId++}";
            this._balance = balance;
            DateOfCreation = DateTime.Now;
            //_clientData = client;
            _hostID = client.Id; //добавлена как замена _clientData = client; из-за проблемы с десериализацией
            _deposit = null;
            _transactionsList = new List<Transaction>();
            //TODO добавить кредитование
            //_credit = null;
        }

        #endregion Constructors

        #region Properties

        public string Id { get => _id; set => _id = value; }
        public decimal Balance { get => _balance; set => _balance = value; }
        public DateTime DateOfCreation { get => _dateOfCreation; set => _dateOfCreation = value; }

        //public ClientModel ClientData { get => _clientData; set => _clientData = value; }
        public int HostId { get => _hostID; set => _hostID = value; }

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

        public IList<Transaction> TransactionsList
        {
            get => _transactionsList;
            set => _transactionsList = value;
        }

        #region Events

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #endregion Properties

        #region Methods

        public void ChangeBalance(Transaction transaction)
        {
            if (transaction.ReceiverID == transaction.SenderID)
            {
                if (_id == transaction.ReceiverAccID)
                    _balance += transaction.Value;
                else
                    _balance -= transaction.Value;
            }
            else if (_hostID == transaction.ReceiverID)
                _balance += transaction.Value;
            else if (_hostID == transaction.SenderID)
                _balance -= transaction.Value;
            _transactionsList.Add(transaction);
        }

        public void OpenDeposit(decimal startBalance, int duration, double interestRateYear)
        {
            _deposit = new DepositModel(startBalance, duration, interestRateYear);
        }

        //private void OnPropertyChanged([CallerMemberName] string propName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        //}

        #endregion Methods
    }
}