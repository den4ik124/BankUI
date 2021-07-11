using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models
{
    public class Transaction
    {
        #region Fields

        private int _senderID;
        private int _receiverID;
        private decimal _value;
        private DateTime _transactionTime;

        #endregion Fields

        #region Constructor

        public Transaction(AccountModel senderAccount, AccountModel receiverAccount, decimal value)
        {
            //TODO проверить правильность заполнения коллекций транзакций
            if (senderAccount != null || receiverAccount != null)
            {
                _senderID = senderAccount.HostId;
                _receiverID = receiverAccount.HostId;
                _value = value;
                _transactionTime = DateTime.Now;
            }
            else
            {
                _senderID = -1;
                _receiverID = -1;
                _value = 0;
                _transactionTime = DateTime.Now;
            }
        }

        #endregion Constructor

        #region Properties

        public int SenderID { get => _senderID; set => _senderID = value; }
        public int ReceiverID { get => _receiverID; set => _receiverID = value; }
        public decimal Value { get => _value; set => _value = value; }
        public DateTime TransactionTime { get => _transactionTime; set => _transactionTime = value; }

        #endregion Properties
    }
}