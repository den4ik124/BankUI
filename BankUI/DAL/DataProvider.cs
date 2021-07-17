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
                    ClientsDBModel.RemoveClient(client);
                    break;
                }
            }
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client);
        }

        public void DeleteAccount(AccountModel account)
        {
            foreach (var acc in AccountsDBModel.Accounts)
            {
                if (acc.Id == account.Id)
                {
                    AccountsDBModel.Remove(acc);
                    break;
                }
            }
            _accounts.Clear();
            foreach (var acc in AccountsDBModel.Accounts)
                _accounts.Add(acc);
        }

        public void GetTestAccountsData()
        {
            _accounts.Clear();
            foreach (var account in Generator.GetAccountsList())
            {
                _accounts.Add(account);
            }
        }

        public void Delete<Y>(Y element)
        {
            if (typeof(Y) == typeof(AccountModel))
            {
                foreach (var acc in AccountsDBModel.Accounts)
                {
                    if (acc.Id == (element as AccountModel).Id)
                    {
                        AccountsDBModel.Remove(acc);
                        foreach (var client in ClientsDBModel.Clients)
                        {
                            if (client.Id == acc.HostId)
                            {
                                client.RemoveAccount(acc);
                                break;
                            }
                        }
                        break;
                    }
                }
                _accounts.Clear();
                foreach (var acc in AccountsDBModel.Accounts)
                    _accounts.Add(acc);
            }
            else if (typeof(Y) == typeof(ClientViewModel))
            {
                foreach (var client in ClientsDBModel.Clients)
                {
                    if (client.Id == (element as ClientViewModel).Id)
                    {
                        ClientsDBModel.RemoveClient(client);
                        break;
                    }
                }
                //_clients.Clear();
                //foreach (var client in ClientsDBModel.Clients)
                //    _clients.Add(client);
            }
            else
                return;
        }

        #endregion Methods
    }
}