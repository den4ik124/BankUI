﻿using BankUI.DAL;
using BankUI.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankUI.Models
{
    public static class ClientsDBModel// : IDataProcessor
    {
        #region Fields

        private static readonly string defaultFileName = "ClientsDataBase.json";
        private static IList<ClientModel> _clients = new ObservableCollection<ClientModel>();
        private static IList<PersonModel> _persons = new ObservableCollection<PersonModel>();
        private static IList<CompanyModel> _companies = new ObservableCollection<CompanyModel>();
        private static IDataProcessor _dataProcessor = new DataProcessor();
        //private IDialogService _dialogService;

        #endregion Fields

        #region Properties

        public static string Path { get; set; } = defaultFileName;

        public static IList<ClientModel> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                UpdateClients();
            }
        }

        public static IList<PersonModel> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                UpdateClients();
            }
        }

        public static IList<CompanyModel> Companies
        {
            get => _companies;
            set
            {
                _companies = value;
                UpdateClients();
            }
        }

        //public static IList<PersonModel> Persons { get => _clients.OfType<PersonModel>().ToList(); set => _persons = value; }
        //public static IList<CompanyModel> Companies { get => _clients.OfType<CompanyModel>().ToList(); set => _companies = value; }

        #endregion Properties

        #region Methods

        public static void AddClient(ClientModel client)
        {
            _clients.Add(client);
            //if (client.GetType() == typeof(PersonModel))
            foreach (var account in client.AccountsList)
            {
                if (!AccountsDBModel.Accounts.Contains(account))
                    AccountsDBModel.AddAccount(account);
            }
            UpdateClients();
            //Serialization(_clients);
        }

        public static void RemoveClient(ClientModel client)
        {
            Clients.Remove(client);
            UpdateClients();
        }

        public static void FillDataBase()
        {
            _clients.Clear();
            var deserializedClients = _dataProcessor.DeserializationJSON<ClientModel>();
            if (deserializedClients == null)
                return;
            AccountsDBModel.Accounts.Clear();
            foreach (var client in deserializedClients)
            {
                _clients.Add(client);
                foreach (var account in client.AccountsList)
                    if (!AccountsDBModel.Accounts.Contains(account))
                        AccountsDBModel.AddAccount(account);
            }
            UpdateClients();
        }

        public static void UpdateBalance(AccountModel account)
        {
            foreach (var client in Clients)
                if (client.Id == account.HostId)
                {
                    foreach (var acc in client.AccountsList)
                        if (acc.Id == account.Id)
                            acc.Balance = account.Balance;
                    client.TotalBalanceCalc();
                }
            UpdateClients();
        }

        public static void UpdateBalances_test(AccountModel senderAccount, AccountModel receiverAccount, Transaction transaction)
        {
            if (senderAccount.HostId != receiverAccount.HostId)
            {
                foreach (var client in Clients)
                {
                    if (client.Id == senderAccount.HostId)
                    {
                        foreach (var acc in client.AccountsList)
                            if (acc.Id == senderAccount.Id)
                            {
                                acc.Balance = AccountsDBModel.Accounts.Where(item => item.Id == acc.Id).FirstOrDefault().Balance;
                                client.TotalBalanceCalc();
                                break;
                            }
                        continue;
                    }
                    else if (client.Id == receiverAccount.HostId)
                    {
                        foreach (var acc in client.AccountsList)
                            if (acc.Id == receiverAccount.Id)
                            {
                                acc.Balance = AccountsDBModel.Accounts.Where(item => item.Id == acc.Id).FirstOrDefault().Balance;
                                client.TotalBalanceCalc();
                                break;
                            }
                        continue;
                    }
                }
            }
            else
            {
                foreach (var client in Clients)
                {
                    if (client.Id == senderAccount.HostId)
                    {
                        foreach (var acc in client.AccountsList)
                        {
                            if (acc.Id == senderAccount.Id)
                            {
                                acc.Balance = AccountsDBModel.Accounts.Where(item => item.Id == acc.Id).FirstOrDefault().Balance;
                                client.TotalBalanceCalc();
                                continue;
                            }
                            else if (acc.Id == receiverAccount.Id)
                            {
                                acc.Balance = AccountsDBModel.Accounts.Where(item => item.Id == acc.Id).FirstOrDefault().Balance;
                                client.TotalBalanceCalc();
                                continue;
                            }
                        }
                        break;
                    }
                }
            }
            UpdateClients();
        }

        public static void UpdateClients()
        {
            _persons.Clear();
            _companies.Clear();
            _persons = _clients.OfType<PersonModel>().ToList();
            _companies = _clients.OfType<CompanyModel>().ToList();
            //TODO убрать сериализацию отсюда, или оставить ???
            _dataProcessor.Serialization(_clients);
            //Serialization(_clients);
        }

        #endregion Methods
    }
}