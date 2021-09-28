using System.Windows;

namespace BankUI.HelpClasses
{
    internal static class Logger
    {
        // TODO Добавить правильное логирование вместо MessageBox
        public static void OnTransactionCreated()
        {
            //логика сохранения данных в файл
            MessageBox.Show("Событие отработало");
        }
    }
}