using BankUI.DAL;
using BankUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankUI.Models
{
    public static class AccountsDBModel
    {
        #region Fields

        private static IList<AccountModel> _accounts = new ObservableCollection<AccountModel>();
        private readonly static IDataProcessor _dataProcessor = new DataProcessor();

        #endregion Fields

        #region Properties

        public static string FileName { get; set; } = "accountsDB.json";

        public static IList<AccountModel> Accounts { get => _accounts; set => _accounts = value; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Добавление счета в БД
        /// </summary>
        /// <param name="account">Счет, который должен быть добавлен в БД</param>
        public static void AddAccount(AccountModel account)
        {
            if (_accounts.Contains(account))
                return;
            _accounts.Add(account);
            SaveDB();
        }

        /// <summary>
        /// Сериализация счетов в .json файл
        /// </summary>
        private static void SaveDB()
        {
            _dataProcessor.Serialization(_accounts, FileName);
        }

        /// <summary>
        /// Перевод средств между счетами
        /// </summary>
        /// <param name="sender">Счет отправителя</param>
        /// <param name="receiver">Счет получателя</param>
        /// <param name="transaction">Транзакция</param>
        public static void MoneyTransfer(AccountModel sender, AccountModel receiver, Transaction transaction)
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
        internal static void Remove(AccountModel acc)
        {
            Accounts.Remove(acc);
            SaveDB();
        }

        #endregion Methods
    }
}