using BankUI.Models;
using System.Collections.Generic;

namespace BankUI.Interfaces
{
    internal interface IDataProvider
    {
        IEnumerable<ClientModel> GetClients(bool isTestData = false);

        IList<ClientModel> DeleteClient(ClientModel client);

        //IEnumerable<Client> GetTestClients();
        void Load();
    }
}