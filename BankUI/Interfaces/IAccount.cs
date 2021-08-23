using BankUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Interfaces
{
    public interface IAccount
    {
        string Id { get; set; }
        decimal Balance { get; }
        int HostId { get; set; }

        void ChangeBalance<T>(Transaction<T> transaction) where T : AccountBaseModel;

        void UpdateAccountBalance();
    }
}