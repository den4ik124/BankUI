using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.CorrectDialogService
{
    internal interface ICloseRequest
    {
        event EventHandler<CloseRequestArgs> CloseRequested;
    }

    internal class CloseRequestArgs : EventArgs
    {
        public bool? DialogResult { get; set; }

        public CloseRequestArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}