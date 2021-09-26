using BankUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankUI.HelpClasses
{
    internal static class Logger
    {
        public static Transaction<AccountBaseModel> TransactionProp
        {
            set
            {
                value.TransactionCreated += OnTransactionCreated;
            }
        }

        public static void OnTransactionCreated<T>(T transaction) where T : Transaction<AccountBaseModel>
        {
            MessageBox.Show(transaction.ToString());
        }
    }
}