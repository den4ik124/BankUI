using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUI
{
    public class Person : Client
    {
        private string surName;
        private string personalCode;
        private string phoneNumber;
        
        public string SurName { get => surName; set => surName = value; }
        public string PersonalCode { get => personalCode; set => personalCode = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public Person(string SurName, string PersonalCode, string PhoneNumber)
        {
            surName = SurName;
            personalCode = PersonalCode;
            phoneNumber = PhoneNumber;
        }
    }


}