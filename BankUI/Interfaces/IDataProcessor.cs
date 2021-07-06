using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Interfaces
{
    public interface IDataProcessor
    {
        void Serialization<T>(T toSerializeObject);

        IList<T> DeserializationJSON<T>(string path = "");
    }
}