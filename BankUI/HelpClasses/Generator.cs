using BankUI.Models;
using System;
using System.Collections.Generic;

namespace BankUI.HelpClasses
{
    public static class Generator
    {
        private static Random random = new Random();

        #region Methods

        public static IList<ClientModel> GetClientsList()
        {
            List<ClientModel> clients = new List<ClientModel>();
            for (int i = 0; i < random.Next(10); i++)
            {
                clients.Add(GetClient());
            }
            return clients;
        }

        public static IList<PersonModel> GetPersonsList()
        {
            List<PersonModel> clients = new List<PersonModel>();
            for (int i = 0; i < random.Next(15); i++)
            {
                clients.Add(GetPerson());
            }
            return clients;
        }

        public static IList<AccountModel> GetAccountsList()
        {
            List<AccountModel> accounts = new List<AccountModel>();
            for (int i = 0; i < random.Next(6); i++)
            {
                accounts.Add(GetAccount());
            }
            return accounts;
        }

        private static AccountModel GetAccount()
        {
            return new AccountModel(GetClient(), (decimal)random.NextDouble() * 1000);
        }

        private static ClientModel GetClient()
        {
            return new PersonModel(RandomName(), isVIP: RandomVIP(), RandomSurname(), random.Next(0, 101).ToString(), $"+380({random.Next(10, 100)})-{random.Next(100, 1000)}-{random.Next(10, 100)}-{random.Next(10, 100)}");
        }

        private static PersonModel GetPerson()
        {
            return new PersonModel(RandomName(), isVIP: RandomVIP(), RandomSurname(), random.Next(0, 101).ToString(), $"+380({random.Next(10, 100)})-{random.Next(100, 1000)}-{random.Next(10, 100)}-{random.Next(10, 100)}");
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