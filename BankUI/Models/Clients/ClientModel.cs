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
        private IList<AccountBaseModel> _accountsList;
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
            _accountsList = new ObservableCollection<AccountBaseModel>();
            //for (int i = 0; i < random.Next(5); i++)
            //{
            //    AddNewAccount(random.Next(1000));
            //}

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

        public IList<AccountBaseModel> AccountsList { get => _accountsList; set => _accountsList = value; }
        //public IList<Account<string>> AccountsList { get => accountsList; set => accountsList = value; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Добавление счета клиенту
        /// </summary>
        /// <param name="balance">Баланс при открытии счета</param>
        public void AddNewAccount(AccountBaseModel account, decimal balance = 0)
        {
            //Вызывать окно создания аккаунта
            //AccountBaseModel account = new AccountBaseModel(this, balance); //TODO Создавать конкретный тип аккаунта : регулярный или депозитный
            _accountsList.Add(account);
            if (!AccountsDBModel.Accounts.Contains(account))
                AccountsDBModel.AddAccount(account);
            //AccountsDBModel.Accounts.Add(account); //исправить на это, если появятся проблемы с сохранением счетов в БД
            TotalBalanceCalc();
        }

        /// <summary>
        /// Удаление счета из списка счетов клиента
        /// </summary>
        /// <param name="account">Счет, который будет удален</param>
        public void RemoveAccount(AccountBaseModel account)
        {
            AccountsList.Remove(account);
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