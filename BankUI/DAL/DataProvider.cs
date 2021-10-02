using BankUI.HelpClasses;
using BankUI.Interfaces;
using BankUI.Models;
using BankUI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankUI.DAL
{
    internal class DataProvider : IDataProvider<ClientModel>
    {
        #region Fields

        private IList<ClientModel> _clients;
        private IList<ClientModel> _testClients;
        private IList<PersonModel> _persons;
        private IList<IAccount> _accounts;

        #endregion Fields

        #region Constructors

        public DataProvider()
        {
            _clients = new List<ClientModel>();
            _testClients = new List<ClientModel>();
            _persons = new List<PersonModel>();
            _accounts = new List<IAccount>();
        }

        #endregion Constructors

        #region Properties

        public IList<ClientModel> Clients { get => _clients; set => _clients = value; }
        public IList<PersonModel> Persons { get => _persons; set => _persons = value; }
        public IList<IAccount> Accounts { get => _accounts; set => _accounts = value; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Заполнение БД клиентами.
        /// </summary>
        public void Load()
        {
            //App.Current.Dispatcher.Invoke(() => ClientsDBModel.FillDataBase()); //Десериализация данных и заполнение БД.
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
                GetTestClientsData();
            }
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
            {
                client.AddClientToDB();
                _clients.Add(client);
            }
            //TODO подумать как убрать обновление БД при каждом клике на Person / Company
            ClientsDBModel.UpdateClients();
            return isTestData ?  _testClients : _clients;
        }

        /// <summary>
        /// Получение тестовых клиентов из генератора.
        /// </summary>
        /// <returns>Коллекцию тестовых клиентов</returns>
        private void GetTestClientsData()
        {
            _testClients.Clear();
            var genData = ClientsGenerator.GetClientsList();
            foreach (var client in genData)
            {
                _testClients.Add(client);
                client.AddClientToDB();
            }
        }
        private async void GetTestClientsDataAsync()
        {
            _clients.Clear();
            await Task.Factory.StartNew(()=> { 
            var genData = ClientsGenerator.GetClientsList();
            foreach (var client in genData)
            {
                _testClients.Add(client);
                client.AddClientToDB();
            }
            });
            ClientsDBModel.UpdateClients();
            AccountsDBModel.SaveDB();
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
                    client.RemoveClientFromDB();
                    break;
                }
            }
            ClientsDBModel.UpdateClients();
            _clients.Clear();
            foreach (var client in ClientsDBModel.Clients)
                _clients.Add(client);
        }

        /// <summary>
        /// Удаление аккаунта (счета) клиента
        /// </summary>
        /// <param name="account">Аккаунт, который будет удален</param>
        public void DeleteAccount(IAccount account)
        {
            foreach (var acc in AccountsDBModel.Accounts)
            {
                if (acc.Id == account.Id)
                {
                    acc.RemoveAccountFromDB();
                    break;
                }
            }
            AccountsDBModel.SaveDB();
            _accounts.Clear();
            foreach (var acc in AccountsDBModel.Accounts)
                _accounts.Add(acc);
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
            if (typeof(Y) == typeof(AccountBaseModel))
            {
                foreach (var acc in AccountsDBModel.Accounts)
                {
                    if (acc.Id == (element as AccountBaseModel).Id)
                    {
                        acc.RemoveAccountFromDB();
                        break;
                    }
                }
                AccountsDBModel.SaveDB();
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
                        client.RemoveClientFromDB();
                        break;
                    }
                }
                ClientsDBModel.UpdateClients();
            }
            else
                return;
        }

        #endregion Methods
    }
}