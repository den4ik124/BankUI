using BankUI.ViewModels;
using System.Windows;

namespace BankUI.Views
{
    /// <summary>
    /// Логика взаимодействия для NewDepositView.xaml
    /// </summary>
    public partial class NewAccountView : Window
    {
        public NewAccountView(ClientViewModel client)
        {
            InitializeComponent();
            DataContext = new NewAccountViewModel(client, this);
        }
    }
}