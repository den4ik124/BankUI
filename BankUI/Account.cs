using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUI
{
    //public class  Account<T>
    public class  Account
    {
        private string id;
        private decimal balance;
        private DateTime dateOfCreation;
        private Client clientData;
        
        public string Id { get => id; set => id = value; }
        public decimal Balance { get => balance; set => balance = value; }
        public DateTime DateOfCreation { get => dateOfCreation; set => dateOfCreation = value; }
        public Client ClientData { get => clientData; set => clientData = value; }

        public Account(Client client, decimal balance)
        {
            id = Client.Id.ToString();
            this.balance = balance;
            DateOfCreation = DateTime.Now;
            clientData = client;
        }
    }
}