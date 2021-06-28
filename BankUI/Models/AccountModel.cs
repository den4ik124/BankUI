using System;

namespace BankUI.Models
{
    //public class  Account<T>
    public class AccountModel
    {
        private string id;
        private decimal balance;
        private DateTime dateOfCreation;
        private ClientModel clientData;

        public string Id { get => id; set => id = value; }
        public decimal Balance { get => balance; set => balance = value; }
        public DateTime DateOfCreation { get => dateOfCreation; set => dateOfCreation = value; }
        public ClientModel ClientData { get => clientData; set => clientData = value; }

        public AccountModel(ClientModel client, decimal balance)
        {
            id = client.Id.ToString();
            this.balance = balance;
            DateOfCreation = DateTime.Now;
            clientData = client;
        }
    }
}