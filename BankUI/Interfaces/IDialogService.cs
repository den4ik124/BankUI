using System.Windows;

namespace BankUI.Interfaces
{
    internal interface IDialogService
    {
        string FilePath { get; }

        bool OpenFileDialog();

        bool DeleteClientWindow();

        void MessageBoxShow(string message, string caption, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Warning);
    }
}