using BankUI.Models;
using BankUI.ViewModels;
using System.Collections.Generic;

namespace BankUI.Interfaces
{
    internal interface IDataProvider<T>
    {
        IEnumerable<T> GetClients(bool isTestData = false);

        IEnumerable<T> GetClientsFilteredByName(string nameFromUI);

        void Delete<Y>(Y element);

        void DeleteClient(ClientViewModel client);

        void DeleteAccount(AccountModel account);

        //IEnumerable<Client> GetTestClients();
        void Load();
    }
}