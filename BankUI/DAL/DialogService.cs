using BankUI.Interfaces;
using Microsoft.Win32;
using System.Windows;

namespace BankUI.DAL
{
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; }

        /// <summary>
        /// Диалоговое окно выбора файла
        /// </summary>
        /// <returns>Результат операции true - файл выбран, false - файл не выбран</returns>
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".json";
            openFileDialog.Filter = "*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Диалоговое окно удаления клиента
        /// </summary>
        /// <returns>True - согласие на удаление клиента, false - отказ удаления клиента.</returns>
        public bool DeleteClientWindow()
        {
            var selection = MessageBox.Show("Действительно удалить этого клиента?\nВернуть данные будет невозможно!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            return selection == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Диалоговое окно удаления счёта (аккаунта)
        /// </summary>
        /// <returns>True - согласие на удаление, false - отказ удаления</returns>
        public bool DeleteAccountWindow()
        {
            var selection = MessageBox.Show("Действительно удалить этот счет?\nВернуть данные будет невозможно!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            return selection == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Открытие MessageBox
        /// </summary>
        /// <param name="message">Сообщение, которое будет отображено пользователю</param>
        /// <param name="caption">Название окна</param>
        /// <param name="button">Тип кнопки в MessageBox</param>
        /// <param name="icon">Иконка MessageBox</param>
        public void MessageBoxShow(string message, string caption, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Warning)
        {
            MessageBox.Show(message, caption, button, icon);
        }

        /// <summary>
        /// Открытие нового окна
        /// </summary>
        /// <param name="dialogWindow">Окно, которое нужно открыть</param>
        public void ShowDialog(Window dialogWindow)
        {
            dialogWindow?.ShowDialog();
        }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="dialogWindow">Окно, которое будет закрыто.</param>
        public void CloseWindow(Window dialogWindow)
        {
            dialogWindow?.Close();
        }
    }
}