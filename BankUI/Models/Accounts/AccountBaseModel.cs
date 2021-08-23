using BankUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankUI.Models
{
    //public class  Account<T>
    public abstract class AccountBaseModel : IAccount // : INotifyPropertyChanged
    {
        #region Fields

        private string _id;
        protected static int _nextId = 1;
        private decimal _balance;
        private DateTime _dateOfCreation;
        private IList<Transaction<AccountBaseModel>> _transactionsList;

        private int _hostID;

        #endregion Fields

        #region Constructors

        public AccountBaseModel(int hostId, decimal balance = 0)
        {
            _balance = balance;
            DateOfCreation = DateTime.Now;
            _hostID = hostId; //добавлена как замена _clientData = client; из-за проблемы с десериализацией
            _transactionsList = new List<Transaction<AccountBaseModel>>();
            //_clientData = client;
            //TODO добавить кредитование
            //_credit = null;
        }

        #endregion Constructors

        #region Properties

        public virtual string Id { get => _id; set => _id = value; }
        public decimal Balance { get => _balance; }
        public DateTime DateOfCreation { get => _dateOfCreation; set => _dateOfCreation = value; }

        public int HostId { get => _hostID; set => _hostID = value; }

        public IList<Transaction<AccountBaseModel>> TransactionsList
        {
            get => _transactionsList;
            set => _transactionsList = value;
        }

        #region Events

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #endregion Properties

        #region Methods

        public void UpdateAccountBalance() =>
            _balance = AccountsDBModel.Accounts.Where(item => item.Id == this.Id).FirstOrDefault().Balance;

        /// <summary>
        /// Изменение баланса на счету.
        /// </summary>
        /// <param name="transaction">Совершенная транзакция</param>
        public void ChangeBalance<T>(Transaction<T> transaction) where T : AccountBaseModel
        {
            if (transaction.ReceiverID == transaction.SenderID) //если перевод между своими счетами клиента
            {
                if (_id == transaction.ReceiverAccID)
                    _balance += transaction.Value; //если аккаунт совпал с аккаунтом получателя в тразакции - баланс увеличивается на величину перевода
                else
                    _balance -= transaction.Value; //если аккаунт совпал с аккаунтом отправителя в тразакции - баланс уменьшается на величину перевода
            }
            else if (_hostID == transaction.ReceiverID) //если ID владельца счета совпал с получаетелем в транзакции
                _balance += transaction.Value;          // баланс увеличивается на величину перевода
            else if (_hostID == transaction.SenderID)   //если ID владельца счета совпал с отправителем в транзакции
                _balance -= transaction.Value;          // баланс уменьшается на величину перевода
            _transactionsList.Add(transaction as Transaction<AccountBaseModel>);         //в список переводов добавляется новый перевод.
        }

        #endregion Methods
    }
}