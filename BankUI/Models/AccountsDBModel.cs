using System.Collections.Generic;

namespace BankUI.Models
{
    public class AccountsDBModel
    {
        private IList<AccountModel> accounts;

        public IList<AccountModel> Accounts { get => accounts; set => accounts = value; }
    }
}