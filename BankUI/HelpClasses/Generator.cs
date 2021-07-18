using BankUI.Models;
using System;
using System.Collections.Generic;

namespace BankUI.HelpClasses
{
    public static class Generator
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
            for (int i = 0; i < random.Next(100); i++)
            {
                clients.Add(GetClient());
            }
            return clients;
        }

        /// <summary>
        /// Создание коллекции тестовых счетов
        /// </summary>
        /// <returns>Коллекция тестовых счетов</returns>
        public static IList<AccountModel> GetAccountsList()
        {
            List<AccountModel> accounts = new List<AccountModel>();
            for (int i = 0; i < random.Next(6); i++)
            {
                accounts.Add(GetAccount());
            }
            return accounts;
        }

        /// <summary>
        /// Создание аккаунта
        /// </summary>
        /// <returns>Новый аккаунт</returns>
        private static AccountModel GetAccount()
        {
            return new AccountModel(GetClient());
        }

        private static ClientModel GetClient()
        {
            switch (random.Next(0, 2))
            {
                case 0:
                    return new PersonModel(RandomName(), isVIP: RandomVIP(), RandomSurname(), random.Next(0, 101).ToString(), $"+380({random.Next(10, 100)})-{random.Next(100, 1000)}-{random.Next(10, 100)}-{random.Next(10, 100)}");

                default:
                    return new CompanyModel($"Company #{random.Next(100)}", Guid.NewGuid().ToString(), random.Next(0, 2) == 1);
            }
        }

        private static bool RandomVIP()
        {
            return random.Next(0, 2) == 1;
        }

        private static string RandomName()
        {
            string[] names = { "Юрий",
                                "Богдан",
                                "Оскар",
                                "Родион",
                                "Филипп",
                                "Макар",
                                "Йошка",
                                "Лука",
                                "Цефас",
                                "Владлен",
                                "Йомер",
                                "Цицерон",
                                "Йоган",
                                "Яков",
                                "Нестор" };
            return names[random.Next(0, names.Length - 1)];
        }

        private static string RandomSurname()
        {
            string[] surnames = { "Недбайло",
                                  "Зайцев",
                                  "Назаров",
                                    "Кличко",
                                    "Молчанов",
                                    "Лукашенко",
                                    "Шарапов",
                                    "Панов",
                                    "Логинов",
                                    "Дементьев",
                                    "Владимиров",
                                    "Спивак",
                                    "Мишин",
                                    "Семёнов",
                                    "Гребневский" };
            return surnames[random.Next(0, surnames.Length - 1)];
        }

        #endregion Methods
    }
}