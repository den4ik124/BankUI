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

namespace BankUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        private IDataProvider<ClientModel> _dataProvider;
        private IDialogService _dialogService;

        private IList<ClientViewModel> _clients;

        private IList<PersonViewModel> _persons;
        private IList<CompanyViewModel> _companies;
        private ClientViewModel _concreteClient;
        private AccountModel _receiverAccount;
        private AccountModel _senderAccount;

        private decimal _transactionValue;

        //private AccountsDBModel _accountsDB;
        //private IList<Account> _accounts;

        private RelayCommand _showTestClients;
        private RelayCommand _addNewClient;
        private RelayCommand _addNewAccount;
        private RelayCommand _showVIPOnly;
        private RelayCommand _deleteClientCommand;
        private RelayCommand _sendMoneyCommand;

        private bool _isVIPSeleceted;

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

        public RelayCommand ShowVIPOnlyCommand => _showVIPOnly ??
            (_showVIPOnly = new RelayCommand(ShowVIPOnly, CanVIPShow));

        public RelayCommand DeleteClientCommand => _deleteClientCommand ??
            (_deleteClientCommand = new RelayCommand(DeleteClient, CanShow));

        public RelayCommand SendMoneyCommand => _sendMoneyCommand ??
            (_sendMoneyCommand = new RelayCommand(SendMoney, CanSend));

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

        public ClientViewModel ConcreteClient
        {
            get => _concreteClient;

            set
            {
                if (_concreteClient == value)
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

        #endregion Properties

        #region Methods

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DataCollectionsRefresh()
        {
            Persons.Refresh();
            Clients.Refresh();
            Companies.Refresh();
        }

        private void DataCollectionsClear()
        {
            _clients.Clear();
            _persons.Clear();
            _companies.Clear();
        }

        private bool CanSend()
        {
            return TransactionValue > 0 && SenderAccount?.Balance >= TransactionValue;
        }

        private void SendMoney()
        {
            //TODO после десериализации креш при переводе денег
            if (SenderAccount.Balance < TransactionValue)
                return;

            AccountsDBModel.MoneyTransfer(SenderAccount, ReceiverAccount, TransactionValue);

            int indexClientDB_sender = 0;
            int indexClientDB_receiver = 0;

            foreach (var client in ClientsDBModel.Clients)
            {
                if (client.Id == SenderAccount.ClientData.Id)
                {
                    indexClientDB_sender = ClientsDBModel.Clients.IndexOf(client);
                }
                else if (client.Id == ReceiverAccount.ClientData.Id)
                {
                    indexClientDB_receiver = ClientsDBModel.Clients.IndexOf(client);
                }
            }

            //У конкретного клиента в БД клиентов (ОТПРАВИТЕЛЬ) меняем баланс на нужном аккаунте
            foreach (var accSender in ClientsDBModel.Clients[indexClientDB_sender].AccountsList)
            {
                if (accSender.Id == SenderAccount.Id)
                {
                    accSender.Balance = SenderAccount.Balance;
                    break;
                }
            }
            //У конкретного клиента в БД клиентов (ПОЛУЧАТЕЛЬ) меняем баланс на нужном аккаунте
            foreach (var accReceiver in ClientsDBModel.Clients[indexClientDB_receiver].AccountsList)
            {
                if (accReceiver.Id == ReceiverAccount.Id)
                {
                    accReceiver.Balance = ReceiverAccount.Balance;
                    break;
                }
            }
            ClientsDBModel.UpdateClients();

            ////SenderAccount.Balance -= TransactionValue;
            ////ReceiverAccount.Balance += TransactionValue;
            //foreach (var acc in AccountsDBModel.Accounts)
            //{
            //    if (acc.Id == SenderAccount.Id)
            //    {
            //        acc.Balance -= TransactionValue;
            //        SenderAccount = acc;
            //    }
            //    else if (acc.Id == ReceiverAccount.Id)
            //    {
            //        acc.Balance += TransactionValue;
            //        ReceiverAccount = acc;
            //    }

            //    foreach (var client in ClientsDBModel.Clients)
            //    {
            //        if (client.Id == SenderAccount.ClientData.Id)
            //            foreach (var account in client.AccountsList)
            //            {
            //                if (account.Id == SenderAccount.Id)
            //                {
            //                    account.Balance = SenderAccount.Balance;
            //                    continue;
            //                }
            //                else if (client.Id == ReceiverAccount.ClientData.Id)
            //                {
            //                    account.Balance = ReceiverAccount.Balance;
            //                    continue;
            //                }
            //            }
            //        client.TotalBalanceCalc();
            //    }
            //    //Почему-то UI не обновляется после транзакции
            //}
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
                foreach (var person in persons)
                    _persons.Add(new PersonViewModel(person));
                foreach (var company in companies)
                    _companies.Add(new CompanyViewModel(company));

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

        private void AddNewClientShow()
        {
            //TODO разобраться как работать с окнами
            //NewClientsView addNewClient = new NewClientsView();
            //addNewClient.ShowDialog();
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
                _dataProvider.DeleteClient(ConcreteClient);
                UpdateClients();
            }
            else
                return;
        }

        private void TestClientsShow()
        {
            //UpdatePersons(true);
            _isVIPSeleceted = false;
            OnPropertyChanged();
            UpdateClients(true);
        }

        private void LoadClients()
        {
            DataCollectionsClear();
            IEnumerable<ClientModel> clients = _dataProvider.GetClients();
            var persons = clients.OfType<PersonModel>();
            var companies = clients.OfType<CompanyModel>();

            foreach (var client in clients)
                _clients.Add(new ClientViewModel(client));
            foreach (var person in persons)
                _persons.Add(new PersonViewModel(person));
            foreach (var company in companies)
                _companies.Add(new CompanyViewModel(company));

            DataCollectionsRefresh();
        }

        /// <summary>
        /// Обновляет список всех клиентов банка
        /// </summary>
        /// <param name="isTestData">Загрузить новые тестовые данные или нет</param>
        private void UpdateClients(bool isTestData = false)
        {
            DataCollectionsClear();
            IEnumerable<ClientModel> clients = _dataProvider.GetClients(isTestData);
            var persons = clients.OfType<PersonModel>();
            var companies = clients.OfType<CompanyModel>();

            foreach (var client in clients)
                _clients.Add(new ClientViewModel(client));
            foreach (var person in persons)
                _persons.Add(new PersonViewModel(person));
            foreach (var company in companies)
                _companies.Add(new CompanyViewModel(company));

            DataCollectionsRefresh();
        }

        #endregion Methods
    }
}