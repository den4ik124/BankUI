using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankUI.CorrectDialogService
{
    internal class DialogService : IDialogService
    {
        private Window _owner;

        public Dictionary<Type, Type> ViewModelMap { get; }

        public DialogService(Window owner)
        {
            ViewModelMap = new Dictionary<Type, Type>();
            this._owner = owner;
        }

        public void Register<TDialogViewModel, TDialogView>()
            where TDialogViewModel : ICloseRequest
            where TDialogView : IDialog
        {
            if (ViewModelMap.ContainsKey(typeof(TDialogViewModel)))
            {
                throw new ArgumentException("ViewModel already mapped");
            }
            ViewModelMap.Add(typeof(TDialogViewModel), typeof(TDialogView));
        }

        public bool? Show<TDialogViewModel>(TDialogViewModel dialogViewModel) where TDialogViewModel : ICloseRequest
        {
            throw new NotImplementedException();
        }
    }
}