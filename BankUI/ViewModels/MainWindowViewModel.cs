﻿using BankUI.DAL;
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

        //private AccountsDBModel _accountsDB;
        //private IList<Account> _accounts;

        private RelayCommand _showTestClients;
        private RelayCommand _addNewClient;
        private RelayCommand _addNewAccount;
        private RelayCommand _showVIPOnly;
        private RelayCommand _deleteClientCommand;

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

        private void DeleteClient()
        {
            DataCollectionsClear();
            //TODO добавить удаление клиента
            //==============================================
            var clients = _dataProvider.DeleteClient(null);
            //==============================================
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