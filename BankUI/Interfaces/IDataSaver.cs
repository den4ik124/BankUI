using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Interfaces
{
    internal interface IDataSaver<T>
    {
        IEnumerable<T> dataCollection { get; set; }

        void Save(IEnumerable<T> collectionToSave);
    }
}