using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.CorrectDialogService
{
    internal class TestViewModel
    {
        private IDialogService _dialogService;

        public TestViewModel()
        {
            System.Windows.Window owner = new System.Windows.Window();
            _dialogService = new DialogService(owner);
        }
    }
}