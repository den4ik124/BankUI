using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models.Accounts
{
    internal class RegularAccountModel : AccountBaseModel
    {
        //[JsonConstructor]
        //public RegularAccountModel()
        //{
        //    _nextId++;
        //}

        public RegularAccountModel(int clientID, decimal balance = 0) : base(clientID, balance)
        {
            Id = $"_R{clientID}|{_nextId++}";
        }
    }
}