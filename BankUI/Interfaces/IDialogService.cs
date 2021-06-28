namespace BankUI.Interfaces
{
    internal interface IDialogService
    {
        string FilePath { get; }

        bool OpenFileDialog();
    }
}