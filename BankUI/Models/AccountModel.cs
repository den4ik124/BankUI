using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankUI.Models
{
    //public class  Account<T>
    public class AccountModel// : INotifyPropertyChanged
    {
        #region Fields

        private string _id;
        private static int _nextId = 1;
        private decimal _balance;
        private DateTime _dateOfCreation;
        private IList<Transaction<AccountModel>> _transactionsList;

        //private ClientModel _clientData;
        private int _hostID;

        private DepositModel _deposit;

        #endregion Fields

        #region Constructors

        [JsonConstructor]
        public AccountModel() { }

        public AccountModel(ClientModel client, decimal balance = 0)
        {
            _id = "_A" + client.Id.ToString() + $"|{_nextId++}";
            this._balance = balance;
            DateOfCreation = DateTime.Now;
            //_clientData = client;
            _hostID = client.Id; //добавлена как замена _clientData = client; из-за проблемы с десериализацией
            _deposit = null;
            _transactionsList = new List<Transaction<AccountModel>>();
            //TODO добавить кредитование
            //_credit = null;
        }

        #endregion Constructors

        #region Properties

        public string Id { get => _id; set => _id = value; }
        public decimal Balance { get => _balance; set => _balance = value; }
        public DateTime DateOfCreation { get => _dateOfCreation; set => _dateOfCreation = value; }

        //public ClientModel ClientData { get => _clientData; set => _clientData = value; }
        public int HostId { get => _hostID; set => _hostID = value; }

        public DepositModel Deposit
        {
            get => _deposit;
            set
            {
                if (_deposit == value)
                    return;
                _deposit = value;
            }
        }

        public IList<Transaction<AccountModel>> TransactionsList
        {
            get => _transactionsList;
            set => _transactionsList = value;
        }

        #region Events

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #endregion Properties

        #region Methods

        /// <summary>
        /// Изменение баланса на счету.
        /// </summary>
        /// <param name="transaction">Совершенная транзакция</param>
        public void ChangeBalance<T>(Transaction<T> transaction) where T : AccountModel
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
            _transactionsList.Add(transaction as Transaction<AccountModel>);         //в список переводов добавляется новый перевод.
        }

        /// <summary>
        /// Открытие вклада
        /// </summary>
        /// <param name="startBalance">Начальная сумма вклада</param>
        /// <param name="duration">Продолжительность депозита</param>
        /// <param name="interestRateYear">% ставка за год</param>
        public void OpenDeposit(decimal startBalance, int duration, double interestRateYear)
        {
            _deposit = new DepositModel(startBalance, duration, interestRateYear);
        }

        //private void OnPropertyChanged([CallerMemberName] string propName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        //}

        #endregion Methods
    }
}