using BankUI.Models;
using System.Collections.Generic;

namespace BankUI.Interfaces
{
    internal interface IClient
    {
        int Id { get; set; }
        bool IsVIP { get; set; }
        string Name { get; set; }
        IList<AccountModel> AccountsList { get; set; }
    }
}