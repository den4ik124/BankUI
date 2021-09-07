﻿using BankUI.DAL;
using BankUI.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                UpdateClientsAsync();
            }
        }

        public static IList<PersonModel> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                UpdateClientsAsync();
            }
        }

        public static IList<CompanyModel> Companies
        {
            get => _companies;
            set
            {
                _companies = value;
                UpdateClientsAsync();
            }
        }

        //public static IList<PersonModel> Persons { get => _clients.OfType<PersonModel>().ToList(); set => _persons = value; }
        //public static IList<CompanyModel> Companies { get => _clients.OfType<CompanyModel>().ToList(); set => _companies = value; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Добавление нового клиента в БД
        /// </summary>
        /// <param name="client">Клиент, который будет добавлен в БД</param>
        public static void AddClient(ClientModel client)
        {
            _clients.Add(client);
            foreach (var account in client.AccountsList)
                if (!AccountsDBModel.Accounts.Contains(account))
                    AccountsDBModel.AddAccount(account); //добавление счетов нового клиента в БД счетов
            UpdateClientsAsync();
        }

        /// <summary>
        /// Удаление клиента из БД
        /// </summary>
        /// <param name="client">Клиент, который будет удален</param>
        public static void RemoveClient(ClientModel client)
        {
            Clients.Remove(client);
            UpdateClientsAsync();
        }

        /// <summary>
        /// Заполнение БД клиентов
        /// </summary>
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

            //foreach (var client in deserializedClients)
            //{
            //    _clients.Add(client);
            //    foreach (var account in client.AccountsList)
            //        if (!AccountsDBModel.Accounts.Contains(account))
            //            AccountsDBModel.AddAccount(account);
            //}
            ////AccountsDBModel.SaveDB();
            UpdateClientsAsync();
        }

        public static void UpdateBalance(ClientModel client)
        {
            if (_clients.Count > 0)
                _clients[_clients.IndexOf(client)].TotalBalanceCalc();
        }

        /// <summary>
        /// Обновление данных в счетах клиентов
        /// </summary>
        /// <param name="senderAccount">Аккаунт отправителя</param>
        /// <param name="receiverAccount">Аккаунт получателя</param>
        /// <param name="transaction">Проведенная транзакция</param>
        public static void UpdateBalances<T>(T senderAccount, T receiverAccount, Transaction<T> transaction) where T : AccountBaseModel
        {
            if (senderAccount.HostId != receiverAccount.HostId) //если отправитель и получатель - разные люди
            {
                foreach (var client in Clients)
                {
                    if (client.Id == senderAccount.HostId) //если клиент - отправитель
                    {
                        foreach (var acc in client.AccountsList)
                            if (acc.Id == senderAccount.Id) // обновляем данные о балансе после перевода (данные из БД)
                            {
                                acc.UpdateAccountBalance();
                                client.TotalBalanceCalc();
                                break;
                            }
                        continue;
                    }
                    else if (client.Id == receiverAccount.HostId) //если клиент - получатель
                    {
                        foreach (var acc in client.AccountsList)
                            if (acc.Id == receiverAccount.Id) // обновляем данные о балансе после перевода (данные из БД)
                            {
                                acc.UpdateAccountBalance();
                                client.TotalBalanceCalc();
                                break;
                            }
                        continue;
                    }
                }
            }
            else // если отправитель и получатель - один человек. Перевод между своими счетами
            {
                foreach (var client in Clients)
                {
                    if (client.Id == senderAccount.HostId)
                    {
                        foreach (var acc in client.AccountsList)
                        {
                            if (acc.Id == senderAccount.Id)
                            {
                                //acc.Balance = UpdateAccountBalance(acc); //AccountsDBModel.Accounts.Where(item => item.Id == acc.Id).FirstOrDefault().Balance;
                                acc.UpdateAccountBalance();
                                client.TotalBalanceCalc();
                                continue;
                            }
                            else if (acc.Id == receiverAccount.Id)
                            {
                                //acc.Balance = UpdateAccountBalance(acc); // AccountsDBModel.Accounts.Where(item => item.Id == acc.Id).FirstOrDefault().Balance;
                                acc.UpdateAccountBalance();
                                client.TotalBalanceCalc();
                                continue;
                            }
                        }
                        break;
                    }
                }
            }
            UpdateClientsAsync();
        }

        /// <summary>
        /// Обновление списков клиентов физ.лиц и юр.лиц и сохранение в .json файл
        /// </summary>
        public static async void UpdateClientsAsync()
        {
            await Task.Run(() =>
            {
                _persons.Clear();
                _companies.Clear();
                _persons = _clients.OfType<PersonModel>().ToList();
                _companies = _clients.OfType<CompanyModel>().ToList();
                //TODO убрать сериализацию отсюда, или оставить ???
                _dataProcessor.Serialization(_clients);
                //Serialization(_clients);
            });
        }

        #endregion Methods
    }
}