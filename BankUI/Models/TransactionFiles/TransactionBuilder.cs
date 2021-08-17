using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models.TransactionFiles
{
    public abstract class TransactionBuilder<Self> where Self : TransactionBuilder<Self>
    {
        protected decimal amount;
        protected AccountModel to;
        protected AccountModel from;

        public Self WithAmount(decimal amount)
        {
            this.amount = amount;
            return This();
        }

        public Self To(AccountModel to)
        {
            this.to = to;
            return This();
        }

        public Self From(AccountModel from)
        {
            this.from = from;
            return This();
        }

        protected abstract Self This();

        //public TransactionBuilder To(string to)
        //{
        //    this.to = to;
        //    return this;
        //}
    }
}