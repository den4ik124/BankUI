using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessorLibrary
{
    public interface IDataProcessor
    {
        void Serialization<T>(T toSerializeObject, string path = "");

        IList<T> DeserializationJSON<T>(string path = "");
    }
}