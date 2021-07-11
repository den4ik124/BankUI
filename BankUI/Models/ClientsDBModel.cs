using BankUI.DAL;
using BankUI.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankUI.Models
{
    public static class ClientsDBModel// : IDataProcessor
    {
        #region Fields

        private static readonly string defaultFileName = "ClientsDataBase.json";
        private static IList<ClientModel> _clients = new ObservableCollection<ClientModel>();
        private static IList<PersonModel> _persons = new ObservableCollection<PersonModel>();
        private static IList<CompanyModel> _companies = new ObservableCollection<CompanyModel>();
        private static IDataProcessor _dataProcessor = new DataProcessor();
        //private IDialogService _dialogService;

        #endregion Fields

        #region Properties

        public static string Path { get; set; } = defaultFileName;

        public static IList<ClientModel> Clients
        {
            get
            {
                UpdateClients();
                return _clients;
            }
            //set => _clients = value;
        }

        public static IList<PersonModel> Persons
        {
            get
            {
                UpdateClients();
                return _persons;
            }
            set => _persons = value;
        }

        public static IList<CompanyModel> Companies
        {
            get
            {
                UpdateClients();
                return _companies;
            }
            set => _companies = value;
        }

        //public static IList<PersonModel> Persons { get => _clients.OfType<PersonModel>().ToList(); set => _persons = value; }
        //public static IList<CompanyModel> Companies { get => _clients.OfType<CompanyModel>().ToList(); set => _companies = value; }

        #endregion Properties

        #region Methods

        public static void AddClient(ClientModel client)
        {
            _clients.Add(client);
            if (client.GetType() == typeof(PersonModel))
                foreach (var account in client.AccountsList)
                {
                    //if (!AccountsDBModel.Accounts.Contains(account))
                    AccountsDBModel.AddAccount(account);
                }
            UpdateClients();
            //Serialization(_clients);
        }

        public static void FillDataBase()
        {
            _clients.Clear();
            AccountsDBModel.Accounts.Clear();
            var deserializedClients = _dataProcessor.DeserializationJSON<ClientModel>();
            if (deserializedClients == null)
                return;
            foreach (var client in deserializedClients)
            {
                _clients.Add(client);
                foreach (var account in client.AccountsList)
                    if (!AccountsDBModel.Accounts.Contains(account))
                        AccountsDBModel.AddAccount(account);

                ////TEST
                ////==============================================
                //if (client.GetType() == typeof(PersonModel))
                //    foreach (var account in client.AccountsList)
                //    {
                //        //if (!AccountsDBModel.Accounts.Contains(account))
                //        AccountsDBModel.AddAccount(account);
                //    }
                ////==============================================
            }
        }

        public static void UpdateClients()
        {
            _persons.Clear();
            _companies.Clear();
            _persons = _clients.OfType<PersonModel>().ToList();
            _companies = _clients.OfType<CompanyModel>().ToList();
            //TODO убрать сериализацию отсюда, или оставить ???
            _dataProcessor.Serialization(_clients);
            //Serialization(_clients);
        }

        #endregion Methods
    }
}