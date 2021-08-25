using BankUI.Interfaces;
using BankUI.Models;
using BankUI.Models.Accounts;
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
            for (int i = 0; i < random.Next(50, 151); i++)
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
            switch (random.Next(0, 3))
            {
                case 1:
                    return new DepositAccountModel(client.Id, random.Next(50, 101), 12, 12, RandomVIP());
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
                    var newPerson = new PersonModel(RandomName(), isVIP: RandomVIP(), RandomSurname(), random.Next(0, 101).ToString(), $"+380({random.Next(10, 100)})-{random.Next(100, 1000)}-{random.Next(10, 100)}-{random.Next(10, 100)}");
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

        private static bool RandomVIP() =>
            random.Next(0, 2) == 1;

        private static string RandomName()
        {
            string[] names = { "Юрий","Богдан","Оскар","Родион","Филипп","Макар","Йошка","Лука","Цефас","Владлен","Йомер","Цицерон",
                                "Йоган","Яков","Нестор","Адам","Роман","Степан","Елисей","Даниил","Сергей","Максим","Станислав","Георгий",
                                "Дмитрий","Артём","Фёдор","Михаил","Леон","Лев","Александр","Захар","Иван","Роберт","Алексей","Давид",
                                "Владимир","Лука","Глеб","Матвей","Никита","Егор","Марк","Руслан","Вячеслав","Тимофей","Мирон","Виктор",
                                "Илья","Савелий","Андрей","Кирилл","Даниэль","Эмиль","Павел","Константин","Денис","Игорь","Арсений",
                                "Владислав","Семён","Филипп","Николай","Юрий","Демид","Гордей","Евгений","Святослав","Родион","Пётр",
                                "Богдан","Билал","Вадим","Платон","Олег","Тихон","Демьян","Данил","Артемий","Всеволод","Ярослав",
                                "Леонид","Али","Марат","Данила","Антон","Тимур","Дамир","Макар","Василий","Григорий","Анатолий","Ян",
                                "Герман","Артур","Валерий","Назар","Савва","Борис","Ибрагим","Эмир","Эрик","Данис","Виталий","Арсен",
                                "Камиль","Марсель","Мирослав","Яков","Эмин","Ростислав","Тигран","Рустам","Мартин","Серафим"};
            return names[random.Next(0, names.Length - 1)];
        }

        private static string RandomSurname()
        {
            string[] surnames = { "Недбайло","Зайцев","Назаров","Кличко","Молчанов",
                                    "Лукашенко","Шарапов","Панов","Логинов","Дементьев",
                                    "Владимиров","Спивак","Мишин","Семёнов","Гребневский" };
            return surnames[random.Next(0, surnames.Length - 1)];
        }

        #endregion Methods
    }
}