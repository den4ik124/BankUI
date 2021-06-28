using System.Collections.Generic;

namespace BankUI.Models
{
    public abstract class ClientModel
    {
        #region Fields

        private int _id;
        private bool _isVIP;
        private string _name;
        private DepositModel _deposit;
        private decimal _balance;
        private int _depositDuration;

        private IList<AccountModel> _accountsList;
        private static int nextId = 1;

        //private IList<Account<string>> accountsList;

        #endregion Fields

        #region Constructors

        public ClientModel()
        {
        }

        public ClientModel(string name, decimal balance = 0, bool isVIP = false, IList<AccountModel> accounts = null)
        {
            _id = nextId++;
            _name = name;
            _isVIP = isVIP;
            _balance = balance;
            _deposit = null;
            //_accountsList = accounts;
        }

        #endregion Constructors

        #region Properties

        public int Id { get => _id; set => _id = value; }
        public bool IsVIP { get => _isVIP; set => _isVIP = value; }
        public string Name { get => _name; set => _name = value; }

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

        //public IList<Account> AccountsList { get => _accountsList; set => _accountsList = value; }
        //public IList<Account<string>> AccountsList { get => accountsList; set => accountsList = value; }

        #endregion Properties

        #region Methods

        public void OpenDeposit(decimal startBalance, int duration, double interestRateYear)
        {
            _deposit = new DepositModel(startBalance, duration, interestRateYear);
        }

        #endregion Methods
    }
}