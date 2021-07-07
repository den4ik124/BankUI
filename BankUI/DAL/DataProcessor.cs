using BankUI.Interfaces;
using Newtonsoft.Json;
using System;
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
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            Formatting = Formatting.Indented,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
        };

        private readonly string _defaultName = "defaultFileName.json";

        public IList<T> DeserializationJSON<T>(string path = "")
        {
            if (path == "")
                path = _defaultName;
            try
            {
                if (!File.Exists(path))
                {
                    //MessageBox.Show("File does not exist!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                //MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.TargetSite, "Load canceled", MessageBoxButton.OK, MessageBoxImage.Stop);
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
    }
}