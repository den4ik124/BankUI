using BankUI.Interfaces;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.DAL
{
    public class DataProcessor : IDataProcessor
    {
        #region Fields

        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            Formatting = Formatting.Indented,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
        };

        private readonly string _defaultName = "defaultFileName.json";
        private IDialogService _dialogService;

        #endregion Fields

        #region Constructors

        public DataProcessor()
        {
            _dialogService = new DialogService();
        }

        #endregion Constructors

        #region Methods

        public IList<T> DeserializationJSON<T>(string path = "")
        {
            if (path == "")
                path = _defaultName;
            try
            {
                if (!File.Exists(path))
                {
                    _dialogService.MessageBoxShow("File does not exist!", "Warning!");
                    return null;
                }
                else
                {
                    string json = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<IList<T>>(json, jsonSettings);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(new string('=', 50) + "\n" + e.Message + "\n" + new string('=', 50));
                _dialogService.MessageBoxShow(e.Message + "\n" + e.Source + "\n" + e.TargetSite,
                                              "Load canceled",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Stop);
                return null;
            }
        }

        public void Serialization<T>(T toSerializeObject, string path = "")
        {
            string json = JsonConvert.SerializeObject(toSerializeObject, jsonSettings);
            if (path == "")
                path = Environment.CurrentDirectory + $"\\{_defaultName}";

            File.WriteAllText(path, json);
            Debug.WriteLine($"\nДанные записаны в файл:\n{path}\n");
        }

        #endregion Methods
    }
}