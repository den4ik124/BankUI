using System.Collections.Generic;
using System;

namespace BankUI.Models
{
    public abstract class ClientModel
    {
        #region Fields

        private int _id;
        private bool _isVIP;
        private string _name;

        private IList<AccountModel> _accountsList;
        private static int nextId = 1;

        private static Random random = new Random();

        //private IList<Account<string>> accountsList;

        #endregion Fields

        #region Constructors

        //public ClientModel()
        //{
        //}

        public ClientModel(string name, bool isVIP = false)
        {
            _id = nextId++;
            _name = name;
            _isVIP = isVIP;
            _accountsList = new List<AccountModel>();
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

        public IList<AccountModel> AccountsList { get => _accountsList; set => _accountsList = value; }
        //public IList<Account<string>> AccountsList { get => accountsList; set => accountsList = value; }

        #endregion Properties

        #region Methods

        public void AddNewAccount(decimal balance = 0)
        {
            _accountsList.Add(new AccountModel(this, balance));
        }

        #endregion Methods
    }
}