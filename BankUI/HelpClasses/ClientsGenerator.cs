using BankUI.Interfaces;
using BankUI.Models;
using BankUI.Models.Accounts;
using System;
using System.Collections.Generic;

namespace BankUI.HelpClasses
{
    public static class ClientsGenerator
    {
        private readonly static Random random = new Random();

        #region Methods

        /// <summary>
        /// Создание списка клиентов.
        /// </summary>
        /// <returns>Коллекцию клиентов </returns>
        public static IList<ClientModel> GetClientsList()
        {
            List<ClientModel> clients = new List<ClientModel>();
            //for (int i = 0; i < random.Next(50, 151); i++)
            for (int i = 0; i < 1000; i++)
                clients.Add(GetClient());
            
            return clients;
        }

        public static IList<ClientModel> GetClientsList(int clientsCount)
        {
            List<ClientModel> clients = new List<ClientModel>();
            for (int i = 0; i < clientsCount; i++)
                clients.Add(GetClient());
            return clients;
        }

        /// <summary>
        /// Создание коллекции тестовых счетов
        /// </summary>
        /// <returns>Коллекция тестовых счетов</returns>
        public static IList<IAccount> GetAccountsList(ClientModel client)
        {
            List<IAccount> accounts = new List<IAccount>();
            for (int i = 0; i < random.Next(6); i++)
                accounts.Add(GetAccount(client));
            return accounts;
        }

        /// <summary>
        /// Создание аккаунта
        /// </summary>
        /// <returns>Новый аккаунт</returns>
        private static IAccount GetAccount(ClientModel client)
        {
            var isVIP = CredentialsGeneratorLibrary.Generator.RandomVIP();
            switch (random.Next(0, 3))
            {
                case 1:
                    return new DepositAccountModel(client.Id, random.Next(50, 101), 12, 12, isVIP);
                //TODO реализовать кредитование
                //case 2:
                //    return new CreditAccountModel(GetClient());
                default:
                    return new RegularAccountModel(client.Id, random.Next(10, 51));
            }
        }

        public static ClientModel GetClient()
        {
            switch (random.Next(0, 2))
            {
                case 0:
                    var name = CredentialsGeneratorLibrary.Generator.RandomName();
                    var surName = CredentialsGeneratorLibrary.Generator.RandomSurname();
                    var isVIP = CredentialsGeneratorLibrary.Generator.RandomVIP();
                    var newPerson = new PersonModel(name, isVIP, surName, random.Next(0, 101).ToString(), $"+380({random.Next(10, 100)})-{random.Next(100, 1000)}-{random.Next(10, 100)}-{random.Next(10, 100)}");
                    foreach (var acc in GetAccountsList(newPerson))
                        newPerson.AddNewAccount(acc);

                    return newPerson;

                default:
                    var newCompany = new CompanyModel($"Company #{random.Next(100)}", Guid.NewGuid().ToString(), random.Next(0, 2) == 1);
                    foreach (var acc in GetAccountsList(newCompany))
                        newCompany.AddNewAccount(acc);

                    return newCompany;
            }
        }

        #endregion Methods
    }
}