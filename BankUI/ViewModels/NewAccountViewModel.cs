using BankUI.DAL;
using BankUI.Interfaces;
using BankUI.Models;
using BankUI.Models.Accounts;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BankUI.ViewModels
{
    public class NewAccountViewModel : INotifyPropertyChanged
    {
        #region Fields

        private decimal _balanceAtMonth;

        private decimal _startBalance;
        private double _interestRateYear;
        private int _depositDuration;
        private DateTime _depositOpened;
        private bool _isCapitalization;

        private IDataProvider<ClientModel> _dataProvider;
        private IDialogService _dialogService;
        private ClientModel _client;

        private RelayCommand _addAccount;

        private RelayCommand _closeWindowCommand;

        private Window _currentWindow;
        private bool _isRegular;
        private bool _isDeposit;
        private bool _isCredit;

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public NewAccountViewModel(ClientViewModel client, Window currentWindow)
        {
            //DepositModel deposit = new DepositModel();
            _currentWindow = currentWindow;
            _dataProvider = new DataProvider();
            _dialogService = new DialogService();
            //TODO проверить на null
            _client = _dataProvider.GetClients().First(item => item.Id == client.Id);
        }

        #endregion Constructors

        #region Properties

        public decimal StartBalance { get => _startBalance; set => _startBalance = value; }
        public double InterestRateYear { get => _interestRateYear; set => _interestRateYear = value; }
        public int DepositDuration { get => _depositDuration; set => _depositDuration = value; }
        public DateTime DepositOpened { get => _depositOpened; set => _depositOpened = value; }
        public bool IsCapitalization { get => _isCapitalization; set => _isCapitalization = value; }
        public bool IsRegular { get => _isRegular; set => _isRegular = value; }
        public bool IsDeposit { get => _isDeposit; set => _isDeposit = value; }
        public bool IsCredit { get => _isCredit; set => _isCredit = value; }

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

        public RelayCommand AddAccountCommand => (_addAccount) ??
            (_addAccount = new RelayCommand(AddNewAccount, () => true));

        public RelayCommand CloseWindowCommand => (_closeWindowCommand) ??
            (_closeWindowCommand = new RelayCommand(CloseWindow, () => true));

        #endregion Properties

        #region Methods

        private void AddNewAccount()
        {
            AccountBaseModel newAcc;
            if (_isCredit)
                return;
            //TODO прописать создание кредита
            //newAcc = new CreditAccountModel(_client, StartBalance, InterestRateYear, DepositDuration, IsCapitalization);
            else if (_isDeposit)
                newAcc = new DepositAccountModel(_client, StartBalance, InterestRateYear, DepositDuration, IsCapitalization);
            else
                newAcc = new RegularAccountModel(_client, StartBalance);

            //TODO проверить правильность добавления в базу. Сериализацию/Десериализацию
            _dataProvider.GetClients().Where(client => client.Id == _client.Id).FirstOrDefault()?.AddNewAccount(newAcc);
            CloseWindow();
        }

        private void CloseWindow()
        {
            _dialogService.CloseWindow(_currentWindow);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}