using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BankUI.Models
{
    public abstract class ClientModel
    {
        #region Fields

        private int _id;
        private bool _isVIP;
        private string _name;
        private decimal _totalBalance;
        private IList<AccountModel> _accountsList;
        private static int nextId = 1;

        private static Random random = new Random();

        //private IList<Account<string>> accountsList;

        #endregion Fields

        #region Constructors

        //public ClientModel()
        //{
        //}
        [JsonConstructor]
        public ClientModel()
        {
        }

        public ClientModel(string name, bool isVIP = false)
        {
            _id = nextId++;
            _name = name;
            _isVIP = isVIP;
            _accountsList = new ObservableCollection<AccountModel>();
            for (int i = 0; i < random.Next(5); i++)
            {
                AddNewAccount(random.Next(1000));
            }

            //_balance = balance;
            //_deposit = null;
            //_accountsList = accounts;
        }

        #endregion Constructors

        #region Properties

        public int Id { get => _id; set => _id = value; }
        public bool IsVIP { get => _isVIP; set => _isVIP = value; }
        public string Name { get => _name; set => _name = value; }
        public decimal TotalBalance { get => _totalBalance; set => _totalBalance = value; }

        public IList<AccountModel> AccountsList { get => _accountsList; set => _accountsList = value; }
        //public IList<Account<string>> AccountsList { get => accountsList; set => accountsList = value; }

        #endregion Properties

        #region Methods

        public void AddNewAccount(decimal balance = 0)
        {
            //TEST
            //==============
            if (this.GetType() == typeof(PersonModel))
            {
                //==============
                AccountModel account = new AccountModel(this, balance);
                _accountsList.Add(account);
                AccountsDBModel.Accounts.Add(account);
            }
            //if (!AccountsDBModel.Accounts.Contains(account))
            TotalBalanceCalc();
        }

        public void TotalBalanceCalc()
        {
            _totalBalance = 0;
            foreach (var account in _accountsList)
            {
                _totalBalance += account.Balance;
            }
        }

        #endregion Methods
    }
}