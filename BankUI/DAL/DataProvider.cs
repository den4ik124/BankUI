using BankUI.HelpClasses;
using BankUI.Interfaces;
using BankUI.Models;
using BankUI.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BankUI.DAL
{
    internal class DataProvider : IDataProvider<ClientModel>
    {
        #region Fields

        private IList<ClientModel> _clients;
        private IList<PersonModel> _persons;
        private IList<AccountModel> _accounts;
        //private IDataProcessor _dataProcessor = new DataProcessor();

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

        public IList<ClientModel> Clients { get => _clients; set => _clients = value; }
        public IList<PersonModel> Persons { get => _persons; set => _persons = value; }
        public IList<AccountModel> Accounts { get => _accounts; set => _accounts = value; }

        #endregion Properties

        #region Methods

        public void Load()
        {
            //TODO Логика считывания коллекции. Десериализация json
            ClientsDBModel.FillDataBase();
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client);
        }

        public IEnumerable<ClientModel> GetClients(bool isTestData = false)
        {
            if (isTestData == true)
            {
                ClientsDBModel.Clients.Clear();
                AccountsDBModel.Accounts.Clear();
            }
            // test ---------------
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client);
            // test ---------------

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

        public void DeleteClient(ClientViewModel clientVM)
        {
            //int index = -1;
            foreach (var client in ClientsDBModel.Clients)
            {
                if (client.Id == clientVM.Id)
                {
                    //index = ClientsDBModel.Clients.IndexOf(client);
                    ClientsDBModel.RemoveClient(client);
                    break;
                }
            }
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client);
            // ClientsDBModel.Clients.RemoveAt(index);
            //ClientsDBModel.Clients.Remove(client);
            //return _clients;
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