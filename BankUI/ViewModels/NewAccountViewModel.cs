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

        private decimal _startBalance;
        private double _interestRateYear;
        private int _depositDuration;
        private int _monthCount;
        private DateTime _depositOpened;
        private bool _isCapitalization;
        private decimal _depositBalanceFinal;

        private IDataProvider<ClientModel> _dataProvider;
        private IDialogService _dialogService;
        private ClientModel _client;

        private RelayCommand _addAccount;

        private RelayCommand _closeWindowCommand;

        private Window _currentWindow;
        private bool _isRegular = true;
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
            _client = _dataProvider.GetClients().First(item => item.Id == client?.Id);
        }

        #endregion Constructors

        #region Properties

        public decimal StartBalance
        {
            get => _startBalance;
            set
            {
                if (_startBalance == value)
                    return;
                _startBalance = value;
                DepositBalanceFinal = GetBalanceAtMonth();
                OnPropertyChanged();
            }
        }

        public double InterestRateYear
        {
            get => _interestRateYear;
            set
            {
                if (_interestRateYear == value)
                    return;
                _interestRateYear = value;
                DepositBalanceFinal = GetBalanceAtMonth();
                OnPropertyChanged();
            }
        }

        public int DepositDuration
        {
            get => _depositDuration;
            set
            {
                if (_depositDuration == value)
                    return;
                _depositDuration = value;
                DepositBalanceFinal = GetBalanceAtMonth();
                OnPropertyChanged();
            }
        }

        public DateTime DepositOpened { get => _depositOpened; set => _depositOpened = value; }

        public bool IsCapitalization
        {
            get => _isCapitalization;
            set
            {
                if (_isCapitalization == value)
                    return;
                _isCapitalization = value;
                DepositBalanceFinal = GetBalanceAtMonth();
                OnPropertyChanged();
            }
        }

        public bool IsRegular { get => _isRegular; set => _isRegular = value; }
        public bool IsDeposit { get => _isDeposit; set => _isDeposit = value; }
        public bool IsCredit { get => _isCredit; set => _isCredit = value; }

        public int MonthCount
        {
            get => _monthCount;
            set
            {
                if (_monthCount == value)
                    return;
                _monthCount = value;
                DepositBalanceFinal = GetBalanceAtMonth();
                OnPropertyChanged();
            }
        }

        public decimal DepositBalanceFinal
        {
            get => _depositBalanceFinal;
            set
            {
                if (_depositBalanceFinal == value)
                    return;
                _depositBalanceFinal = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddAccountCommand => (_addAccount) ??
            (_addAccount = new RelayCommand(AddNewAccount, () =>
            {
                if (IsRegular)
                    return StartBalance > 0;
                else if (IsDeposit)
                    return StartBalance > 0 && DepositDuration > 0 && InterestRateYear > 0;
                else
                    return false;
            }));

        public RelayCommand CloseWindowCommand => (_closeWindowCommand) ??
            (_closeWindowCommand = new RelayCommand(CloseWindow, () => true));

        #endregion Properties

        #region Methods

        public decimal GetBalanceAtMonth() =>
            _isCapitalization ? Capitalization(_monthCount) : NoCapitalization(_monthCount);

        /// <summary>
        /// Расчет баланса счета с капитализацией
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета с капитализацией</returns>
        private decimal Capitalization(int monthCount) =>
            StartBalance * (decimal)Math.Pow(1 + _interestRateYear / 12 / 100, monthCount);

        /// <summary>
        /// Расчет баланса счета без капитализации
        /// </summary>
        /// <param name="monthCount">Число месяцев</param>
        /// <returns>Значение баланса счета с капитализацией</returns>
        private decimal NoCapitalization(int monthCount) =>
            monthCount >= 12 ? StartBalance * (decimal)Math.Pow(1 + _interestRateYear / 100, monthCount / 12) : StartBalance;

        private void AddNewAccount()
        {
            AccountBaseModel newAcc;
            if (_isCredit)
                return;
            //TODO прописать создание кредита
            //newAcc = new CreditAccountModel(_client, StartBalance, InterestRateYear, DepositDuration, IsCapitalization);
            else if (_isDeposit)
                newAcc = new DepositAccountModel(_client.Id, StartBalance, InterestRateYear, DepositDuration, IsCapitalization);
            else
                newAcc = new RegularAccountModel(_client.Id, StartBalance);

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