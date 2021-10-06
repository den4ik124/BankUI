using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankUI.Core
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //private protected IDataProvider<ClientModel> _dataProvider = new DataProvider();
    }
}