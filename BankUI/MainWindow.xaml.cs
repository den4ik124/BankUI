using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Client> Clients { get; set; }
        
        public MainWindow()
        {
            Clients = new List<Client> {
        new Person("surName 1","personal code #1","phone number 1"),
        new Person("surName 2","personal code #2","phone number 2"),
        new Person("surName 3","personal code #3","phone number 3")};


            InitializeComponent();


        }
    }
}
