using BankUI.HelpClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models
{
    public class Transaction<T> where T : AccountBaseModel
    {
        #region Fields

        private int _senderID;
        private int _receiverID;
        private string _senderAccID;
        private string _receiverAccID;
        private decimal _value;
        private DateTime _transactionTime;

        #endregion Fields

        #region Constructor

        [JsonConstructor]
        public Transaction()
        { }

        public Transaction(in T senderAccount, in T receiverAccount, in decimal value)
        {
            #region Old transaction ctor

            //TODO проверить правильность заполнения коллекций транзакций
            //if (senderAccount != null && receiverAccount != null)
            //{
            //    _senderID = senderAccount.HostId;
            //    _receiverID = receiverAccount.HostId;
            //    _senderAccID = senderAccount.Id;
            //    _receiverAccID = receiverAccount.Id;
            //    _value = value;
            //}
            //else
            //{
            //    _senderID = -1;
            //    _receiverID = -1;
            //    _senderAccID = "-1";
            //    _receiverAccID = "-1";
            //    _value = 0;
            //}
            //_transactionTime = DateTime.Now;

            #endregion Old transaction ctor

            _senderID = senderAccount.HostId;
            _receiverID = receiverAccount.HostId;
            _senderAccID = senderAccount.Id;
            _receiverAccID = receiverAccount.Id;
            _value = value;
            _transactionTime = DateTime.Now;
        }

        #endregion Constructor

        #region Properties

        public int SenderID { get => _senderID; set => _senderID = value; }
        public int ReceiverID { get => _receiverID; set => _receiverID = value; }
        public string SenderAccID { get => _senderAccID; set => _senderAccID = value; }
        public string ReceiverAccID { get => _receiverAccID; set => _receiverAccID = value; }
        public decimal Value { get => _value; set => _value = value; }
        public DateTime TransactionTime { get => _transactionTime; set => _transactionTime = value; }

        #endregion Properties

        #region Events

        public delegate void TransactionHandler(Transaction<AccountBaseModel> transaction);

        public event TransactionHandler TransactionCreated;

        #endregion Events

        #region Methods

        public override string ToString() =>
            $"{_value}$ were transeferred from {_senderID} to {_receiverID} at {_transactionTime}";

        #endregion Methods
    }
}