using BankUI.HelpClasses;
using BankUI.Interfaces;
using BankUI.Models;
using System.Collections.Generic;

namespace BankUI.DAL
{
    internal class DataProvider : IDataProvider
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
        }

        public IEnumerable<ClientModel> GetClients(bool isTestData = false)
        {
            return isTestData ? GetTestClientsData() : _clients;
        }

        public IEnumerable<PersonModel> GetPersons(bool isTestData = false)
        {
            return isTestData ? GetTestPersonsData() : _persons;
        }

        //public IEnumerable<Client> GetTestClients()
        //{
        //    GetTestClientsData();
        //    return _clients;
        //}

        private IList<ClientModel> GetTestClientsData()
        {
            _clients.Clear();
            foreach (var client in Generator.GetClientsList())
            {
                _clients.Add(client);
            }
            return _clients;
        }

        private IList<PersonModel> GetTestPersonsData()
        {
            _persons.Clear();
            foreach (var person in Generator.GetPersonsList())
            {
                _persons.Add(person);
            }
            return _persons;
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