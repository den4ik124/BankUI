using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models.Accounts
{
    internal class RegularAccountModel : AccountBaseModel
    {
        public RegularAccountModel(ClientModel client, decimal balance = 0) : base(client, balance)
        {
            Id = "_R" + client.Id.ToString() + $"|{_nextId++}";
        }
    }
}