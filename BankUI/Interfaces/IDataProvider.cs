using BankUI.Models;
using System.Collections.Generic;

namespace BankUI.Interfaces
{
    internal interface IDataProvider<T>
    {
        IEnumerable<T> GetClients(bool isTestData = false);

        IList<T> DeleteClient(T client);

        //IEnumerable<Client> GetTestClients();
        void Load();
    }
}