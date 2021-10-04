using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BankUI.Core
{
    public class RelayCommand : ICommand, INotifyPropertyChanged
    {
        #region Fields

        private Action _execute;
        private Func<bool> _canExecute;
        private string _displayName;

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion Events

        #region Constructors

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Constructors

        #region Properties

        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (_displayName == value)
                    return;
                _displayName = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion Methods
    }
}