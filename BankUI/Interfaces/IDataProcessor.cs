using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Interfaces
{
    public interface IDataProcessor
    {
        //JsonSerializerSettings jsonSettings { get; set; }

        void Serialization<T>(T toSerializeObject, string path = "");

        IList<T> DeserializationJSON<T>(string path = "");
    }
}