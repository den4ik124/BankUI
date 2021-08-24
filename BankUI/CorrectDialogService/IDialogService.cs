using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.CorrectDialogService
{
    internal interface IDialogService
    {
        bool? Show<TDialogViewModel>(TDialogViewModel dialogViewModel)
            where TDialogViewModel : ICloseRequest;

        void Register<TDialogViewModel, TDialogView>()
            where TDialogView : IDialog
            where TDialogViewModel : ICloseRequest;
    }
}