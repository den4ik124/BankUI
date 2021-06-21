using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUI
{
    public class Entity : Client
    {
        private string entityCode;
        public string EntityCode { get => entityCode; set => entityCode = value; }

        public Entity( string EntityCode)
        {
            entityCode = EntityCode;
        }
    }
}