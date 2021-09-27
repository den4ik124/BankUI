using System.Windows;

namespace BankUI.HelpClasses
{
    internal static class Logger
    {
        public static void OnTransactionCreated()
        {
            MessageBox.Show("Событие отработало");
        }
    }
}