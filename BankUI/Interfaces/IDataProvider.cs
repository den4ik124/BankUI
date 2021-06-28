using BankUI.Models;
using System.Collections.Generic;

namespace BankUI.Interfaces
{
    internal interface IDataProvider
    {
        IEnumerable<ClientModel> GetClients(bool isTestData = false);

        //IEnumerable<Client> GetTestClients();
        void Load();
    }
}