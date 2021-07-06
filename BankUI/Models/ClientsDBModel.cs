using BankUI.DAL;
using BankUI.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace BankUI.Models
{
    public static class ClientsDBModel// : IDataProcessor
    {
        #region Fields

        private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            Formatting = Formatting.Indented,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
        };

        private static string defaultFileName = "ClientsDataBase.json";
        private static IList<ClientModel> _clients = new List<ClientModel>();
        private static IList<PersonModel> _persons = new List<PersonModel>();
        private static IList<CompanyModel> _companies = new List<CompanyModel>();
        //private IDialogService _dialogService;

        #endregion Fields

        #region Properties

        public static string Path { get; set; } = defaultFileName;

        public static IList<ClientModel> Clients
        {
            get
            {
                UpdateClients();
                return _clients;
            }
            set => _clients = value;
        }

        public static IList<PersonModel> Persons
        {
            get
            {
                UpdateClients();
                return _persons;
            }
            set => _persons = value;
        }

        public static IList<CompanyModel> Companies
        {
            get
            {
                UpdateClients();
                return _companies;
            }
            set => _companies = value;
        }

        //public static IList<PersonModel> Persons { get => _clients.OfType<PersonModel>().ToList(); set => _persons = value; }
        //public static IList<CompanyModel> Companies { get => _clients.OfType<CompanyModel>().ToList(); set => _companies = value; }

        #endregion Properties

        #region Methods

        public static void AddClient(ClientModel client)
        {
            _clients.Add(client);
            UpdateClients();
            //Serialization(_clients);
        }

        private static void Serialization<T>(T toSerializeObject)
        {
            string json = JsonConvert.SerializeObject(toSerializeObject, jsonSettings);
            if (Path == defaultFileName)
                Path = Environment.CurrentDirectory + $"\\{defaultFileName}";

            File.WriteAllText(Path, json);
            Debug.WriteLine($"\nДанные записаны в файл:\n{Path}\n");
        }

        //public static IList<T> DeserializationJSON<T>(string path = "")
        public static void DeserializationJSON<T>(string path = "")
        {
            try
            {
                if (!File.Exists(Path))
                {
                    //MessageBox.Show("File does not exist!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    string json = File.ReadAllText(Path);
                    IList<T> result = JsonConvert.DeserializeObject<IList<T>>(json, jsonSettings);
                    foreach (T client in result)
                    {
                        _clients.Add(client as ClientModel);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(new string('=', 50) + "\n" + e.Message + "\n" + new string('=', 50));
                MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.TargetSite, "Load canceled", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
        }

        private static void UpdateClients()
        {
            _persons.Clear();
            _companies.Clear();
            _persons = _clients.OfType<PersonModel>().ToList();
            _companies = _clients.OfType<CompanyModel>().ToList();
            //TODO убрать сериализацию отсюда, или оставить ???
            Serialization(_clients);
        }

        #endregion Methods
    }
}