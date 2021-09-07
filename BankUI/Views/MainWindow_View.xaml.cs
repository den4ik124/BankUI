using BankUI.Models;
using System.Windows;

namespace BankUI.Views
{
    /// <summary>
    /// Логика взаимодействия для testWindow.xaml
    /// </summary>
    public partial class MainWindow_View : Window
    {
        public MainWindow_View()
        {
            InitializeComponent();
        }

        //TODO ПОДУМАТЬ КАК ВТУЛИТЬ СОБЫТИЕ "закрытие окна" В MVVM !!!
        private void Window_Closed(object sender, System.EventArgs e)
        {
            ClientsDBModel.UpdateClientsAsync();
        }
    }
}