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
            {
                clients.Add(GetClient());
            }
            return clients;
        }

        /// <summary>
        /// Создание коллекции тестовых счетов
        /// </summary>
        /// <returns>Коллекция тестовых счетов</returns>
        public static IList<AccountBaseModel> GetAccountsList()
        {
            List<AccountBaseModel> accounts = new List<AccountBaseModel>();
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
        private static AccountBaseModel GetAccount()
        {
            switch (random.Next(0, 3))
            {
                case 1:
                    return new DepositAccountModel(GetClient(), random.Next(0, 101), 12, 12, RandomVIP());

                //case 2:
                //    return new CreditAccountModel(GetClient());

                default:
                    return new RegularAccountModel(GetClient());
            }

            //return new AccountModel(GetClient());
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