using BankUI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankUI.ViewModels
{
    internal class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields

        private PersonModel _client;
        //private string _backgroundColor;

        #endregion Fields

        #region Constructors

        public PersonViewModel(ClientModel client)
        {
            this._client = client as PersonModel;
        }

        #endregion Constructors

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        public int Id
        {
            get => _client.Id;
            set
            {
                if (_client.Id == value)
                    return;
                _client.Id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _client.Name;
            set
            {
                if (_client.Name == value)
                    return;
                _client.Name = value;
                OnPropertyChanged();
            }
        }

        public bool IsVIP
        {
            get => _client.IsVIP;
            set
            {
                if (_client.IsVIP == value)
                    return;

                _client.IsVIP = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _client.PhoneNumber;
            set
            {
                if (_client.PhoneNumber == value)
                    return;
                _client.PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        //public Color BackgroundColor
        //{
        //    get
        //    {
        //        return IsVIP ? Color.FromRgb(255, 215, 0) : Color.FromRgb(0, 0, 0);
        //    }
        //}
        public string BackgroundColor
        {
            get
            {
                return IsVIP ? "LemonChiffon" : "White";
            }
            //set
            //{
            //    if (_client.IsVIP == false)
            //        _backgroundColor = "White";
            //    else
            //        _backgroundColor = "Gold";

            //    OnPropertyChanged();
            //}
        }

        #endregion Properties

        #region Methods

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}