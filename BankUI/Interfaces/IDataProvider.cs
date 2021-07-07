using BankUI.Models;
using BankUI.ViewModels;
using System.Collections.Generic;

namespace BankUI.Interfaces
{
    internal interface IDataProvider<T>
    {
        IEnumerable<T> GetClients(bool isTestData = false);

        void DeleteClient(ClientViewModel client);

        //IEnumerable<Client> GetTestClients();
        void Load();
    }
}