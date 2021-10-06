using BankUI.Core;
using BankUI.DAL;
using BankUI.HelpClasses;
using BankUI.Interfaces;
using BankUI.Models;
using BankUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BankUI.ViewModels
{
    public class ClientsPageViewModel : BaseViewModel
    {
        #region Fields

        private object _currentClientsView;
        private bool _isVIPSelected;
        private bool _isPersonsSelected = true;
        private bool _isCompaniesSelected;
        private RelayCommand _showVIPOnly;
        private RelayCommand _showPersonsOnlyCommand;
        private RelayCommand _showCompaniesOnlyCommand;

        private IDataProvider<ClientModel> _dataProvider;
        private IDistanceMetric _distanceMetric = new Levenshtein();

        private IList<ClientViewModel> _clients;
        private IList<ClientViewModel> _dataToShow;

        #endregion Fields

        #region Constructors

        public ClientsPageViewModel()
        {
            _dataProvider = new DataProvider();
            _clients = new List<ClientViewModel>();
            _dataToShow = new List<ClientViewModel>();
            DataToShow = CollectionViewSource.GetDefaultView(_dataToShow);
            LoadClientsAsync();
        }

        private IEnumerable<ClientModel> GetClients(IDistanceMetric distanceMetric = null, bool isTestData = false)
        {
            IEnumerable<ClientModel> clients;
            clients = _dataProvider.GetClients(isTestData);
            //if (!IsFindClientByNameEmpty && distanceMetric != null)
            //    return clients.Where(client => distanceMetric.FindDistance(client.Name, FindClientsByName) <= 2);
            return clients;
        }

        #endregion Constructors

        #region Properties

        #region Commands

        public RelayCommand ShowVIPOnlyCommand => _showVIPOnly ??
            (_showVIPOnly = new RelayCommand(ShowVIPOnly, () => true));

        public RelayCommand ShowPersonsOnlyCommand => _showPersonsOnlyCommand ??
            (_showPersonsOnlyCommand = new RelayCommand(ShowPersonsOnly, () => true));

        public RelayCommand ShowCompaniesOnlyCommand => _showCompaniesOnlyCommand ??
            (_showCompaniesOnlyCommand = new RelayCommand(ShowCompaniesOnly, () => true));

        #endregion Commands

        public ICollectionView DataToShow { get; set; }

        public IDistanceMetric DistanceMetric
        {
            get => _distanceMetric;
            set => _distanceMetric = value;
        }

        //public PersonViewModel PersonsViewModel { get; set; }
        //public PersonViewModel CompaniesViewModel { get; set; }

        public bool IsVIPSelected
        {
            get => _isVIPSelected;
            set
            {
                _isVIPSelected = !_isVIPSelected;
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

        public object CurrentClientsView
        {
            get => _currentClientsView;
            set
            {
                if (_currentClientsView == value)
                    return;
                _currentClientsView = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        private async void LoadClientsAsync()
        {
            await Task.Run(() =>
            {
                _dataProvider.Load();
            });
            UpdateClientsAsync();
        }

        private async void UpdateClientsAsync(bool isTestData = false)
        {
            IEnumerable<ClientModel> clients = await Task.Factory.StartNew(() => GetClients(DistanceMetric, isTestData));

            DataCollectionsClear();
            IEnumerable<PersonModel> persons;
            IEnumerable<CompanyModel> companies;
            if (IsVIPSelected)
            {
                persons = clients.OfType<PersonModel>().Where(client => client.IsVIP == true);
                companies = clients.OfType<CompanyModel>().Where(client => client.IsVIP == true);
            }
            else
            {
                persons = clients.OfType<PersonModel>();
                companies = clients.OfType<CompanyModel>();
            }

            try
            {
                if (IsPersonsSelected)
                    foreach (var person in persons)
                        _dataToShow.Add(new PersonViewModel(person)); //TODO !!! выяснить почему все клиенты NOT VIP
                else
                    foreach (var company in companies)
                        _dataToShow.Add(new CompanyViewModel(company));

                foreach (var client in clients)
                    _clients.Add(new ClientViewModel(client));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "PROBLEM");
            }
            DataCollectionsRefresh();
        }

        private void DataCollectionsClear()
        {
            _dataToShow.Clear();
        }

        private void DataCollectionsRefresh() =>
            App.Current.Dispatcher.Invoke(() => DataToShow.Refresh());

        private void ShowPersonsOnly()
        {
            UpdateClientsAsync();

            CurrentClientsView = new PersonsListView();
            //CurrentClientsView = new PersonsListViewModel();
        }

        private void ShowCompaniesOnly()
        {
            UpdateClientsAsync();
            CurrentClientsView = new CompaniesListView();
            //CurrentClientsView = new CompaniesListViewModel(_isVIPSeleceted);
        }

        private async void ShowVIPOnly()
        {
            if (_isVIPSelected == true)
            {
                DataCollectionsClear();
                IEnumerable<ClientModel> clients = await Task.Factory.StartNew(() => _dataProvider.GetClients().Where(client => client.IsVIP == true));
                //IEnumerable<ClientModel> clients = _dataProvider.GetClients().Where(client => client.IsVIP == true);
                var persons = clients.OfType<PersonModel>().Where(person => person.IsVIP == true);
                var companies = clients.OfType<CompanyModel>().Where(company => company.IsVIP == true);

                foreach (var client in clients)
                    _clients.Add(new ClientViewModel(client));
                if (IsPersonsSelected)
                    foreach (var person in persons)
                        _dataToShow.Add(new PersonViewModel(person));
                else if (IsCompaniesSelected)
                    foreach (var company in companies)
                        _dataToShow.Add(new CompanyViewModel(company));

                DataCollectionsRefresh();
            }
            else
                UpdateClientsAsync();
            //UpdateClients();
        }

        #endregion Methods
    }
}