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
using BankUI.HelpClasses;
using BankUI.Models.TransactionFiles;

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
        private AccountBaseModel _selectedAccount;
        private AccountBaseModel _receiverAccount;
        private AccountBaseModel _senderAccount;

        private string _findClientsByName = string.Empty;

        private decimal _transactionValue;

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

        private bool _isVIPSeleceted;
        private bool _isPersonsSelected = true;
        private bool _isCompaniesSelected = false;

        private static Random random = new Random();
        private IDistanceMetric _distanceMetric = new Levenshtein();

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        //public event EventHandler CollectionDataChanged;  // TODO Использовать событие вместо обновления всей коллекции DataToShow

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
        }

        #endregion Constructors

        #region Properties

        #region Commands

        public RelayCommand ShowTestClients => _showTestClients ??
            (_showTestClients = new RelayCommand(TestClientsShow, CanShow));

        public RelayCommand AddNewClient => _addNewClient ??
            (_addNewClient = new RelayCommand(AddNewClientShow, CanShow));

        public RelayCommand AddNewAccount => _addNewAccount ??
            (_addNewAccount = new RelayCommand(AddNewAcc, () => ConcreteClient != null));

        public RelayCommand RemoveAccountCommand => _removeAccountCommand ??
            (_removeAccountCommand = new RelayCommand(RemoveAccount, () => ConcreteClient?.AccountsList.Count > 0));

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

        public AccountBaseModel ReceiverAccount
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

        public AccountBaseModel SenderAccount
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

        public ObservableCollection<IAccount> AccountsList
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

        public AccountBaseModel SelectedAccount
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

        public IDistanceMetric DistanceMetric
        {
            get => _distanceMetric;
            set => _distanceMetric = value;
        }

        #endregion Properties

        #region Methods

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            DataToShow.Refresh();

            //UpdateClients();
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
            if (SenderAccount == null || ReceiverAccount == null) return;
            if (SenderAccount.Balance < TransactionValue) return;
            if (SenderAccount == ReceiverAccount) return;
            //https://github.com/den4ik124/FBQ.git

            Transaction<AccountBaseModel> transactionFBQ = new TransactionAccounts(SenderAccount, ReceiverAccount)
                                                .WithAmount(TransactionValue)
                                                .GetTransaction();

            //Transaction<AccountModel> transaction = new Transaction<AccountModel>(SenderAccount, ReceiverAccount, TransactionValue);

            AccountsDBModel.MoneyTransfer(SenderAccount, ReceiverAccount, transactionFBQ);
            ClientsDBModel.UpdateBalances(SenderAccount, ReceiverAccount, transactionFBQ);

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
            NewAccountView newDepositWindow = new NewAccountView(ConcreteClient);
            _dialogService.ShowDialog(newDepositWindow);
            UpdateClients();
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
            _dataProvider.Load();
            UpdateClients();
        }

        /// <summary>
        /// Обновляет список всех клиентов банка
        /// </summary>
        /// <param name="isTestData">Загрузить новые тестовые данные или нет</param>
        private void UpdateClients(bool isTestData = false)
        {
            IEnumerable<ClientModel> clients = GetClients(DistanceMetric, isTestData);

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
            DataCollectionsRefresh();
        }

        private IEnumerable<ClientModel> GetClients(IDistanceMetric distanceMetric = null, bool isTestData = false)
        {
            IEnumerable<ClientModel> clients;
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            clients = _dataProvider.GetClients(isTestData);
            //stopWatch.Stop();
            //Debug.WriteLine($"На заполнение данные: {stopWatch.ElapsedMilliseconds} ms.\n" + new string('=', 50));
            if (!IsFindClientByNameEmpty && distanceMetric != null)
                return clients.Where(client => distanceMetric.FindDistance(client.Name, FindClientsByName) <= 2);
            return clients;
        }

        #endregion Methods
    }
}