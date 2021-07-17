﻿using BankUI.DAL;
using BankUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankUI.Models
{
    public static class AccountsDBModel
    {
        private static IList<AccountModel> _accounts = new ObservableCollection<AccountModel>();
        private readonly static IDataProcessor _dataProcessor = new DataProcessor();

        public static string FileName { get; set; } = "accountsDB.json";

        public static IList<AccountModel> Accounts { get => _accounts; set => _accounts = value; }

        public static void AddAccount(AccountModel account)
        {
            if (_accounts.Contains(account))
                return;
            _accounts.Add(account);
            SaveDB();
        }

        private static void SaveDB()
        {
            _dataProcessor.Serialization(_accounts, FileName);
        }

        public static void MoneyTransfer(AccountModel sender, AccountModel receiver, Transaction transaction)
        {
            _accounts[_accounts.IndexOf(sender)].ChangeBalance(transaction);
            _accounts[_accounts.IndexOf(receiver)].ChangeBalance(transaction);

            SaveDB();

            //_accounts[_accounts.IndexOf(sender)].Balance -= transactionValue;
            //_accounts[_accounts.IndexOf(receiver)].Balance += transactionValue;
        }

        internal static void Remove(AccountModel acc)
        {
            Accounts.Remove(acc);
            SaveDB();
        }
    }
}