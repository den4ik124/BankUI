using BankUI.DAL;
using BankUI.Interfaces;
using System.Collections.ObjectModel;

namespace BankUI.Models
{
    public static class AccountsDBModel
    {
        #region Fields

        private static ObservableCollection<IAccount> _accounts = new ObservableCollection<IAccount>();
        private readonly static IDataProcessor _dataProcessor = new DataProcessor();

        #endregion Fields

        #region Properties

        public static string FileName { get; set; } = "accountsDB.json";

        public static ObservableCollection<IAccount> Accounts { get => _accounts; set => _accounts = value; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Добавление счета в БД
        /// </summary>
        /// <param name="account">Счет, который должен быть добавлен в БД</param>
        public static void AddAccount(IAccount account)
        {
            if (_accounts.Contains(account))
                return;
            _accounts.Add(account);
            SaveDB();
        }

        /// <summary>
        /// Сериализация счетов в .json файл
        /// </summary>
        internal static void SaveDB() =>
            _dataProcessor.Serialization(_accounts, FileName);

        /// <summary>
        /// Перевод средств между счетами
        /// </summary>
        /// <param name="sender">Счет отправителя</param>
        /// <param name="receiver">Счет получателя</param>
        /// <param name="transaction">Транзакция</param>
        public static void MoneyTransfer<T>(T sender, T receiver, Transaction<T> transaction) where T : AccountBaseModel
        {
            _accounts[_accounts.IndexOf(sender)].ChangeBalance(transaction);
            _accounts[_accounts.IndexOf(receiver)].ChangeBalance(transaction);

            SaveDB();

            //_accounts[_accounts.IndexOf(sender)].Balance -= transactionValue;
            //_accounts[_accounts.IndexOf(receiver)].Balance += transactionValue;
        }

        /// <summary>
        /// Удаление счета из БД
        /// </summary>
        /// <param name="acc">Счет, который будет удален</param>
        internal static void Remove(IAccount acc)
        {
            Accounts.Remove(acc);
            SaveDB();
        }

        #endregion Methods
    }
}