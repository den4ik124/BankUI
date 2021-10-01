using BankUI.HelpClasses;
using DataProcessorLibrary;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankUI.Models
{
    public static class ClientsDBModel// : IDataProcessor
    {
        #region Fields

        private static readonly string defaultFileName = "ClientsDataBase.json";

        private static IList<ClientModel> _clients;
        private static IList<PersonModel> _persons;
        private static IList<CompanyModel> _companies;

        private static IDataProcessor _dataProcessor = new DataProcessor();
        //private IDialogService _dialogService;

        #endregion Fields

        static ClientsDBModel()
        {
            _clients = new ObservableCollection<ClientModel>();
            _persons = new ObservableCollection<PersonModel>();
            _companies = new ObservableCollection<CompanyModel>();
        }

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

        /// <summary>
        /// Заполнение БД клиентов
        /// </summary>
        public static void FillDataBase()
        {
            _clients.Clear();
            var deserializedClients = _dataProcessor.DeserializationJSON<ClientModel>(defaultFileName);
            if (deserializedClients == null)
                return;
            AccountsDBModel.Accounts.Clear();

            foreach (var client in deserializedClients)
                client.AddClientToDB();
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
                                acc.UpdateAccountBalance();
                                client.TotalBalanceCalc();
                                continue;
                            }
                            else if (acc.Id == receiverAccount.Id)
                            {
                                acc.UpdateAccountBalance();
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

        /// <summary>
        /// Обновление списков клиентов физ.лиц и юр.лиц и сохранение в .json файл
        /// </summary>
        public static void UpdateClients()
        {
            _persons.Clear();
            _companies.Clear();
            _persons = _clients.OfType<PersonModel>().ToList();
            _companies = _clients.OfType<CompanyModel>().ToList();
            //TODO убрать сериализацию отсюда, или оставить ???
            _clients.Save(defaultFileName);
            //_dataProcessor.Serialization(_clients);

            #region Try UpdateAsync

            //await Task.Run(() =>
            //{
            //    _persons.Clear();
            //    _companies.Clear();
            //    _persons = _clients.OfType<PersonModel>().ToList();
            //    _companies = _clients.OfType<CompanyModel>().ToList();
            //    //TODO убрать сериализацию отсюда, или оставить ???
            //    _dataProcessor.Serialization(_clients);
            //    //Serialization(_clients);
            //});

            #endregion Try UpdateAsync
        }

        #endregion Methods
    }
}