﻿using BankUI.HelpClasses;
using BankUI.Interfaces;
using BankUI.Models;
using BankUI.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BankUI.DAL
{
    internal class DataProvider : IDataProvider<ClientModel>
    {
        #region Fields

        private IList<ClientModel> _clients;
        private IList<PersonModel> _persons;
        private IList<AccountModel> _accounts;
        private IDistanceMetric _distanceMetric;
        //private IDataProcessor _dataProcessor = new DataProcessor();

        #endregion Fields

        #region Constructors

        public DataProvider()
        {
            _clients = new List<ClientModel>();
            _persons = new List<PersonModel>();
            _accounts = new List<AccountModel>();
            _distanceMetric = new Levenshtein();
            Load();
        }

        #endregion Constructors

        #region Properties

        public IList<ClientModel> Clients { get => _clients; set => _clients = value; }
        public IList<PersonModel> Persons { get => _persons; set => _persons = value; }
        public IList<AccountModel> Accounts { get => _accounts; set => _accounts = value; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Заполнение БД клиентами.
        /// </summary>
        public void Load()
        {
            ClientsDBModel.FillDataBase(); //Десериализация данных и заполнение БД.
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client); //заполнение списка клиентов для дальнейшей передачи
        }

        /// <summary>
        /// Возвращает коллекцию клиентов
        /// </summary>
        /// <param name="isTestData">Используется для генерации тестовых клиентов. true - создавать тестовых клиентов, false - вернуть существующих клиентов</param>
        /// <returns>Коллекция клиентов</returns>
        public IEnumerable<ClientModel> GetClients(bool isTestData = false)
        {
            if (isTestData == true)
            {
                ClientsDBModel.Clients.Clear();
                AccountsDBModel.Accounts.Clear();
            }
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client);

            return isTestData ? GetTestClientsData() : _clients;
        }

        /// <summary>
        /// Получение тестовых клиентов из генератора.
        /// </summary>
        /// <returns>Коллекцию тестовых клиентов</returns>
        private IList<ClientModel> GetTestClientsData()
        {
            _clients.Clear();
            foreach (var client in Generator.GetClientsList())
            {
                _clients.Add(client);
                ClientsDBModel.AddClient(client); //добавление клиента в БД
            }
            return _clients;
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="clientVM">ViewModel клиента из View.</param>
        public void DeleteClient(ClientViewModel clientVM)
        {
            //int index = -1;
            foreach (var client in ClientsDBModel.Clients)
            {
                if (client.Id == clientVM.Id)
                {
                    ClientsDBModel.RemoveClient(client); //Если клиент найден - он удаляется из БД.
                    break;
                }
            }
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client);
        }

        /// <summary>
        /// Удаление аккаунта (счета) клиента
        /// </summary>
        /// <param name="account">Аккаунт, который будет удален</param>
        public void DeleteAccount(AccountModel account)
        {
            foreach (var acc in AccountsDBModel.Accounts)
            {
                if (acc.Id == account.Id)
                {
                    AccountsDBModel.Remove(acc); //удаление счета из БД счетов.
                    break;
                }
            }
            _accounts.Clear();
            foreach (var acc in AccountsDBModel.Accounts)
                _accounts.Add(acc);
        }

        /// <summary>
        /// Создание тестового счета
        /// </summary>
        public void GetTestAccountsData()
        {
            _accounts.Clear();
            foreach (var account in Generator.GetAccountsList())
            {
                //AccountsDBModel.AddAccount(account);
                _accounts.Add(account);
            }
        }

        /// <summary>
        /// Удаление элемента из БД
        /// </summary>
        /// <typeparam name="Y">Тип удаляемого объекта</typeparam>
        /// <param name="element">Элемент, который будет удален</param>
        public void Delete<Y>(Y element)
        {
            // TODO подумать, что лучше: один такой обобщенный метод или 2 отдельных не обощенных.
            // если тип - аккаунт
            if (typeof(Y) == typeof(AccountModel))
            {
                foreach (var acc in AccountsDBModel.Accounts)
                {
                    if (acc.Id == (element as AccountModel).Id)
                    {
                        AccountsDBModel.Remove(acc);
                        foreach (var client in ClientsDBModel.Clients)
                        {
                            if (client.Id == acc.HostId)
                            {
                                client.RemoveAccount(acc);
                                break;
                            }
                        }
                        break;
                    }
                }
                _accounts.Clear();
                foreach (var acc in AccountsDBModel.Accounts)
                    _accounts.Add(acc);
            }
            //если тип - клиент.
            else if (typeof(Y) == typeof(ClientViewModel))
            {
                foreach (var client in ClientsDBModel.Clients)
                {
                    if (client.Id == (element as ClientViewModel).Id)
                    {
                        ClientsDBModel.RemoveClient(client);
                        break;
                    }
                }
            }
            else
                return;
        }

        #endregion Methods
    }
}