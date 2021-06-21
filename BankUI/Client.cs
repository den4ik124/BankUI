using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUI
{
    public abstract class Client
    {
        private static int id;
        private bool isVIP;
        private string name;
        private IList<Account> accountsList;
        //private IList<Account<string>> accountsList;
        
        public static int Id { get => id; set => id = value; }
        public bool IsVIP { get => isVIP; set => isVIP = value; }
        public string Name { get => name; set => name = value; }
        public IList<Account> AccountsList { get => accountsList; set => accountsList = value; }
        //public IList<Account<string>> AccountsList { get => accountsList; set => accountsList = value; }
    }
}