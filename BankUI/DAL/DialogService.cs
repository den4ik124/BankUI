using BankUI.Interfaces;
using Microsoft.Win32;
using System.Windows;

namespace BankUI.DAL
{
    internal class DialogService : IDialogService
    {
        public string FilePath { get; set; }

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

        public bool DeleteClientWindow()
        {
            var selection = MessageBox.Show("Действительно удалить этого клиента?\nВернуть данные будет невозможно!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            return selection == MessageBoxResult.Yes;
        }

        public void MessageBoxShow(string message, string caption, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Warning)
        {
            MessageBox.Show(message, caption, button, icon);
        }

        public void ShowDialog(Window dialogWindow)
        {
            dialogWindow?.ShowDialog();
        }

        public void CloseWindow(Window dialogWindow)
        {
            dialogWindow?.Close();
        }
    }
}