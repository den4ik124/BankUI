using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Interfaces
{
    internal interface IDistanceMetric
    {
        int FindDistance(string horizontal, string vertical);
    }
}