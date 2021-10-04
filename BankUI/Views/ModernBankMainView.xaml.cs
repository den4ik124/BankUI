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
using System.Windows.Shapes;

namespace BankUI.Views
{
    /// <summary>
    /// Interaction logic for ModernBankMainView.xaml
    /// </summary>
    public partial class ModernBankMainView : Window
    {
        public ModernBankMainView()
        {
            InitializeComponent();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Ellipse_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}