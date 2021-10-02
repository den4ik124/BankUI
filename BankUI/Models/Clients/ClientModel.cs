using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using BankUI.Interfaces;
using BankUI.HelpClasses;

namespace BankUI.Models
{
    public abstract class ClientModel
    {
        #region Fields

        private int _id;
        private bool _isVIP;
        private string _name;
        private decimal _totalBalance;
        private ObservableCollection<IAccount> _accountsList;
        private static int nextId = 1;

        #endregion Fields

        #region Constructors

        [JsonConstructor]
        public ClientModel()
        {
        }

        public ClientModel(string name, bool isVIP = false)
        {
            _id = nextId++;
            _name = name;
            _isVIP = isVIP;
            _accountsList = new ObservableCollection<IAccount>();
        }

        #endregion Constructors

        #region Properties

        public int Id { get => _id; set => _id = value; }
        public bool IsVIP { get => _isVIP; set => _isVIP = value; }
        public string Name { get => _name; set => _name = value; }

        public decimal TotalBalance
        {
            get => _totalBalance;
            set => _totalBalance = value;
        }

        public ObservableCollection<IAccount> AccountsList { get => _accountsList; set => _accountsList = value; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Добавление счета клиенту
        /// </summary>
        /// <param name="balance">Баланс при открытии счета</param>
        public void AddNewAccount(IAccount account, decimal balance = 0)
        {
            account.AddAccountToDB();
            _accountsList.Add(account);
            ClientsDBModel.UpdateBalance(this);
            TotalBalanceCalc();
        }


        /// <summary>
        /// Пересчет суммарного баланса клиентов.
        /// </summary>
        public void TotalBalanceCalc()
        {
            _totalBalance = 0;
            foreach (var account in _accountsList)
                _totalBalance += account.Balance;
        }

        #endregion Methods
    }
}