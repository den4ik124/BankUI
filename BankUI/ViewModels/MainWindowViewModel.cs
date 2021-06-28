using BankUI.DAL;
using BankUI.Interfaces;
using BankUI.Models;
using BankUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace BankUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        private IDataProvider _dataProvider;
        private IDialogService _dialogService;

        private IList<ClientViewModel> _clients;

        private IList<PersonViewModel> _persons;
        private IList<CompanyViewModel> _companies;
        //private IList<Account> _accounts;

        private RelayCommand _showTestClients;
        private RelayCommand _addNewClient;
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

            _clients = new List<ClientViewModel>();
            Clients = CollectionViewSource.GetDefaultView(_clients);

            _persons = new List<PersonViewModel>();
            Persons = CollectionViewSource.GetDefaultView(_persons);
        }

        #endregion Constructors

        #region Properties

        public RelayCommand ShowTestClients => _showTestClients ??
            (_showTestClients = new RelayCommand(TestClientsShow, CanShow));

        public RelayCommand AddNewClient => _addNewClient ??
            (_addNewClient = new RelayCommand(AddNewClientShow, CanShow));

        public ICollectionView Clients { get; }
        public ICollectionView Persons { get; }
        public ICollectionView Entities { get; }

        #endregion Properties

        #region Methods

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanShow()
        {
            return true;
        }

        private void AddNewClientShow()
        {
            //TODO разобраться как работать с окнами
            //NewClientsView addNewClient = new NewClientsView();
            //addNewClient.ShowDialog();

            if (random.Next(0, 2) == 1)
                _persons.Add(new PersonViewModel(new PersonModel("test Name", random.Next(0, 2) == 1, "test Surname", "test111", "phone test")));
            //else
            //_companies.Add(new ClientViewModel(new CompanyModel("TEST COMPANY", "TEST CODE")));

            Clients.Refresh();
            Persons.Refresh();
        }

        private void TestClientsShow()
        {
            UpdatePersons(true);
            //UpdateClients(true);
        }

        private void UpdateClients(bool isTestData = false)
        {
            _clients.Clear();
            foreach (var client in _dataProvider.GetClients(isTestData))
                _clients.Add(new ClientViewModel(client));

            Clients.Refresh();
        }

        private void UpdatePersons(bool isTestData = false)
        {
            _persons.Clear();
            foreach (var client in _dataProvider.GetClients(isTestData))
                _persons.Add(new PersonViewModel(client));

            Persons.Refresh();
        }

        #endregion Methods
    }
}