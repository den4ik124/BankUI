using System.Windows;

namespace BankUI.Interfaces
{
    internal interface IDialogService
    {
        string FilePath { get; }

        bool OpenFileDialog();

        bool DeleteClientWindow();

        bool DeleteAccountWindow();

        void MessageBoxShow(string message, string caption, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Warning);

        void ShowDialog(Window dialogWindow);

        void CloseWindow(Window dialogWindow);
    }
}