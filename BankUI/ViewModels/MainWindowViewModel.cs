using BankUI.DAL;
using BankUI.Interfaces;
using BankUI.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Collections.ObjectModel;
using BankUI.Views;

namespace BankUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        private IDataProvider<ClientModel> _dataProvider;
        private IDialogService _dialogService;

        private IList<ClientViewModel> _clients;
        private IList<ClientViewModel> _dataToShow;

        private IList<PersonViewModel> _persons;
        private IList<CompanyViewModel> _companies;
        private ClientViewModel _concreteClient;
        private AccountModel _selectedAccount;
        private AccountModel _receiverAccount;
        private AccountModel _senderAccount;

        private string _findClientsByName = string.Empty;

        private decimal _transactionValue;

        //private AccountsDBModel _accountsDB;
        //private IList<Account> _accounts;

        private RelayCommand _showTestClients;
        private RelayCommand _addNewClient;
        private RelayCommand _addNewAccount;
        private RelayCommand _removeAccountCommand;

        private RelayCommand _showVIPOnly;
        private RelayCommand _showPersonsOnlyCommand;
        private RelayCommand _showCompaniesOnlyCommand;

        private RelayCommand _deleteClientCommand;
        private RelayCommand _sendMoneyCommand;
        private RelayCommand _addClientCommand;
        private RelayCommand _openDepositCommand;

        private bool _isVIPSeleceted;
        private bool _isPersonsSelected = true;
        private bool _isCompaniesSelected = false;

        private static Random random = new Random();

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public MainWindowViewModel()
        {
            _dataProvider = new DataProvider();
            _dialogService = new DialogService();

            _clients = new ObservableCollection<ClientViewModel>();
            Clients = CollectionViewSource.GetDefaultView(_clients);

            _persons = new ObservableCollection<PersonViewModel>();
            Persons = CollectionViewSource.GetDefaultView(_persons);

            _companies = new ObservableCollection<CompanyViewModel>();
            Companies = CollectionViewSource.GetDefaultView(_companies);

            _dataToShow = new ObservableCollection<ClientViewModel>();
            DataToShow = CollectionViewSource.GetDefaultView(_dataToShow);

            LoadClients();

            //_dialogService.OpenFileDialog();
            //ClientsDBModel.Path = _dialogService.FilePath;

            //_accountsDB = new AccountsDBModel()
            //{
            //    Path = "clients.json"
            //};
        }

        #endregion Constructors

        #region Properties

        #region Commands

        public RelayCommand ShowTestClients => _showTestClients ??
            (_showTestClients = new RelayCommand(TestClientsShow, CanShow));

        public RelayCommand AddNewClient => _addNewClient ??
            (_addNewClient = new RelayCommand(AddNewClientShow, CanShow));

        public RelayCommand AddNewAccount => _addNewAccount ??
            (_addNewAccount = new RelayCommand(AddNewAcc, CanShow));

        public RelayCommand RemoveAccountCommand => _removeAccountCommand ??
            (_removeAccountCommand = new RelayCommand(RemoveAccount, CanShow));

        public RelayCommand ShowVIPOnlyCommand => _showVIPOnly ??
            (_showVIPOnly = new RelayCommand(ShowVIPOnly, CanVIPShow));

        public RelayCommand ShowPersonsOnlyCommand => _showPersonsOnlyCommand ??
           (_showPersonsOnlyCommand = new RelayCommand(ShowPersonsOnly, CanVIPShow));

        public RelayCommand ShowCompaniesOnlyCommand => _showCompaniesOnlyCommand ??
           (_showCompaniesOnlyCommand = new RelayCommand(ShowCompaniesOnly, CanVIPShow));

        public RelayCommand DeleteClientCommand => _deleteClientCommand ??
            (_deleteClientCommand = new RelayCommand(DeleteClient, CanShow));

        public RelayCommand SendMoneyCommand => _sendMoneyCommand ??
            (_sendMoneyCommand = new RelayCommand(SendMoney, CanSend));

        public RelayCommand AddClientCommand => _addClientCommand ??
            (_addClientCommand = new RelayCommand(AddClient, CanShow));

        public RelayCommand OpenDepositCommand => _openDepositCommand ??
            (_openDepositCommand = new RelayCommand(OpenDeposit, CanShow));

        public decimal TransactionValue
        {
            get => _transactionValue;
            set
            {
                if (_transactionValue == value)
                    return;
                _transactionValue = value;
                OnPropertyChanged();
            }
        }

        #endregion Commands

        public ICollectionView Clients { get; }
        public ICollectionView Persons { get; }
        public ICollectionView Companies { get; }
        public ICollectionView DataToShow { get; }

        public ClientViewModel ConcreteClient
        {
            get => _concreteClient;

            set
            {
                if (_concreteClient == value || value == null)
                    return;
                _concreteClient = value;
                OnPropertyChanged();
            }
        }

        public AccountModel ReceiverAccount
        {
            get => _receiverAccount;
            set
            {
                if (_receiverAccount == value)
                    return;
                _receiverAccount = value;
                OnPropertyChanged();
            }
        }

        public AccountModel SenderAccount
        {
            get { return _senderAccount; }
            set
            {
                if (_senderAccount == value)
                    return;
                _senderAccount = value;
                OnPropertyChanged();
            }
        }

        public IList<AccountModel> AccountsList
        {
            //TODO Можно ли так увязывать Model u ViewModel ?
            get => AccountsDBModel.Accounts;

            set => OnPropertyChanged();
        }

        public bool IsVIPSeleceted
        {
            get => _isVIPSeleceted;
            set
            {
                //if (isVIPSeleceted == value)
                //    return;
                _isVIPSeleceted = !_isVIPSeleceted;
                OnPropertyChanged();
            }
        }

        public bool IsPersonsSelected
        {
            get => _isPersonsSelected;
            set
            {
                if (_isPersonsSelected == value)
                    return;
                _isPersonsSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsCompaniesSelected
        {
            get => _isCompaniesSelected;
            set
            {
                if (_isCompaniesSelected == value)
                    return;

                _isCompaniesSelected = value;
                OnPropertyChanged();
            }
        }

        public AccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                if (_selectedAccount == value)
                    return;
                _selectedAccount = value;
                OnPropertyChanged();
            }
        }

        public string FindClientsByName
        {
            get => _findClientsByName;
            set
            {
                _findClientsByName = value;
                UpdateClients();
                OnPropertyChanged();
            }
        }

        public bool IsFindClientByNameEmpty
        {
            get => _findClientsByName.Length > 0 ? false : true;
        }

        #endregion Properties

        #region Methods

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //UpdateClients();
            //DataCollectionsRefresh();
        }

        private void AddClient()
        {
            NewClientsView newClientWindow = new NewClientsView();
            _dialogService.ShowDialog(newClientWindow);
            UpdateClients();
        }

        private void DataCollectionsRefresh()
        {
            Persons.Refresh();
            Clients.Refresh();
            Companies.Refresh();
            DataToShow.Refresh();
        }

        private void DataCollectionsClear()
        {
            _clients.Clear();
            _persons.Clear();
            _companies.Clear();
            _dataToShow.Clear();
        }

        private bool CanSend()
        {
            return TransactionValue > 0 && SenderAccount?.Balance >= TransactionValue && (SenderAccount != ReceiverAccount);
        }

        private void SendMoney()
        {
            if (SenderAccount.Balance < TransactionValue)
                return;
            if (SenderAccount == ReceiverAccount)
                return;
            Transaction transaction = new Transaction(SenderAccount, ReceiverAccount, TransactionValue);
            AccountsDBModel.MoneyTransfer(SenderAccount, ReceiverAccount, transaction);
            ClientsDBModel.UpdateBalances(SenderAccount, ReceiverAccount, transaction);

            ClientsDBModel.UpdateClients();
        }

        private bool CanVIPShow()
        {
            return _clients.Count > 0;
        }

        private bool CanShow()
        {
            return true;
        }

        private void ShowVIPOnly()
        {
            if (_isVIPSeleceted == true)
            {
                DataCollectionsClear();
                IEnumerable<ClientModel> clients = _dataProvider.GetClients().Where(client => client.IsVIP == true);
                var persons = clients.OfType<PersonModel>().Where(person => person.IsVIP == true);
                var companies = clients.OfType<CompanyModel>().Where(company => company.IsVIP == true);

                foreach (var client in clients)
                    _clients.Add(new ClientViewModel(client));
                if (IsPersonsSelected)
                    foreach (var person in persons)
                    {
                        _dataToShow.Add(new PersonViewModel(person));
                        _persons.Add(new PersonViewModel(person));
                    }
                else if (IsCompaniesSelected)
                    foreach (var company in companies)
                    {
                        _dataToShow.Add(new CompanyViewModel(company));
                        _companies.Add(new CompanyViewModel(company));
                    }

                DataCollectionsRefresh();
            }
            else
                UpdateClients();
        }

        private void AddNewAcc()
        {
            var temp = ConcreteClient;
            //TODO проверить почему удаляются "тестовые" сотрудники
            _dataProvider.GetClients().Where(client => client.Id == ConcreteClient.Id).FirstOrDefault()?.AddNewAccount();
            UpdateClients();
            // ConcreteClient.AddNewAccount(random.Next(0, 1000));
        }

        private void RemoveAccount()
        {
            if (_dialogService.DeleteAccountWindow())
            {
                //_dataProvider.DeleteAccount(SelectedAccount);
                if (SelectedAccount == null)
                    return;

                _dataProvider.Delete(SelectedAccount);
                UpdateClients();
            }
            else
                return;
        }

        private void ShowCompaniesOnly()
        {
            UpdateClients();
        }

        private void ShowPersonsOnly()
        {
            UpdateClients();
        }

        private void AddNewClientShow()
        {
            ClientModel client;
            ClientViewModel clientVM;
            if (random.Next(0, 2) == 1)
            {
                client = new PersonModel("test Name", random.Next(0, 2) == 1, "test Surname", "test111", "phone test");
                clientVM = new PersonViewModel(client as PersonModel);
                _persons.Add(clientVM as PersonViewModel);
            }
            else
            {
                client = new CompanyModel("TEST COMPANY", "TEST CODE", random.Next(0, 2) == 1);
                clientVM = new CompanyViewModel(client as CompanyModel);
                _companies.Add(clientVM as CompanyViewModel);
            }
            _clients.Add(clientVM);
            ClientsDBModel.AddClient(client);
            DataCollectionsRefresh();
        }

        private void DeleteClient()
        {
            if (_dialogService.DeleteClientWindow())
            {
                //_dataProvider.DeleteClient(ConcreteClient);
                _dataProvider.Delete(ConcreteClient);
                UpdateClients();
            }
            else
                return;
        }

        private void TestClientsShow()
        {
            _isVIPSeleceted = false;
            OnPropertyChanged();
            UpdateClients(true);
        }

        private void LoadClients()
        {
            UpdateClients();
        }

        /// <summary>
        /// Обновляет список всех клиентов банка
        /// </summary>
        /// <param name="isTestData">Загрузить новые тестовые данные или нет</param>
        private void UpdateClients(bool isTestData = false)
        {
            IEnumerable<ClientModel> clients;
            if (IsFindClientByNameEmpty)
                clients = _dataProvider.GetClients(isTestData);
            else
                clients = _dataProvider.GetClientsFilteredByName(FindClientsByName);
            DataCollectionsClear();
            IEnumerable<PersonModel> persons;
            IEnumerable<CompanyModel> companies;
            if (IsVIPSeleceted)
            {
                persons = clients.OfType<PersonModel>().Where(client => client.IsVIP == true);
                companies = clients.OfType<CompanyModel>().Where(client => client.IsVIP == true);
            }
            else
            {
                persons = clients.OfType<PersonModel>();
                companies = clients.OfType<CompanyModel>();
            }

            if (IsPersonsSelected)
                foreach (var person in persons)
                    _dataToShow.Add(new PersonViewModel(person));
            else
                foreach (var company in companies)
                    _dataToShow.Add(new CompanyViewModel(company));

            foreach (var client in clients)
                _clients.Add(new ClientViewModel(client));
            foreach (var person in persons)
                _persons.Add(new PersonViewModel(person));
            foreach (var company in companies)
                _companies.Add(new CompanyViewModel(company));

            DataCollectionsRefresh();
        }

        private void OpenDeposit()
        {
            NewDepositView newDepositWindow = new NewDepositView();
            _dialogService.ShowDialog(newDepositWindow);
        }

        #endregion Methods
    }
}