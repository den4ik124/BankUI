using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUI
{
    public class AccountsDB
    {
        private IList<Account> accounts;

        public IList<Account> Accounts { get => accounts; set => accounts = value; }
    }

    public class ClientsDB
    {
        private IList<Client> clients;

        public IList<Client> Clients { get => clients; set => clients = value; }
    }
}