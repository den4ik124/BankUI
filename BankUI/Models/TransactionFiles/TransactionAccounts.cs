using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models.TransactionFiles
{
    public class TransactionAccounts : TransactionBuilder<TransactionAccounts>
    {
        public TransactionAccounts(AccountBaseModel from, AccountBaseModel to)
        {
            this.from = from;
            this.to = to;
        }

        public Transaction<AccountBaseModel> GetTransaction() => new Transaction<AccountBaseModel>(from, to, amount);

        protected override TransactionAccounts This() => this;
    }
}