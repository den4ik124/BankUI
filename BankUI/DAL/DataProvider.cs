using BankUI.HelpClasses;
using BankUI.Interfaces;
using BankUI.Models;
using System.Collections.Generic;

namespace BankUI.DAL
{
    internal class DataProvider : IDataProvider<ClientModel>
    {
        #region Fields

        private IList<ClientModel> _clients;
        private IList<PersonModel> _persons;
        private IList<AccountModel> _accounts;

        #endregion Fields

        #region Constructors

        public DataProvider()
        {
            _clients = new List<ClientModel>();
            _persons = new List<PersonModel>();
            _accounts = new List<AccountModel>();
            Load();
        }

        #endregion Constructors

        #region Properties

        private IEnumerable<ClientModel> Clients { get; set; }
        private IEnumerable<PersonModel> Persons { get; set; }
        private IEnumerable<AccountModel> Accounts { get; set; }

        #endregion Properties

        #region Methods

        public void Load()
        {
            //TODO Логика считывания коллекции. Десериализация json
            //var clientsList = ClientsDBModel.DeserializationJSON<ClientModel>();
            _clients.Clear();
            ClientsDBModel.DeserializationJSON<ClientModel>();
            foreach (var client in ClientsDBModel.Clients)
            {
                _clients.Add(client);
            }
        }

        public IEnumerable<ClientModel> GetClients(bool isTestData = false)
        {
            //return isTestData ? GetTestClientsData() : _clients;
            if (isTestData == true)
                ClientsDBModel.Clients.Clear();
            return isTestData ? GetTestClientsData() : _clients;
        }

        private IList<ClientModel> GetTestClientsData()
        {
            _clients.Clear();
            foreach (var client in Generator.GetClientsList())
            {
                _clients.Add(client);
                ClientsDBModel.AddClient(client);
            }
            return _clients;
        }

        public IList<ClientModel> DeleteClient(ClientModel client)
        {
            ClientsDBModel.Clients.Remove(client);
            _clients = ClientsDBModel.Clients;
            return _clients;
        }

        public void GetTestAccountsData()
        {
            _accounts.Clear();
            foreach (var account in Generator.GetAccountsList())
            {
                _accounts.Add(account);
            }
        }

        #endregion Methods
    }
}