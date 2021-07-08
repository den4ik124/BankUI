using BankUI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankUI.ViewModels
{
    internal class PersonViewModel : ClientViewModel//, INotifyPropertyChanged
    {
        #region Fields

        private PersonModel _person;
        //private string _backgroundColor;

        #endregion Fields

        #region Constructors

        public PersonViewModel(ClientModel person)
        {
            this._person = person as PersonModel;
        }

        #endregion Constructors

        #region Events

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        public override int Id
        {
            get => _person.Id;
            set
            {
                if (_person.Id == value)
                    return;
                _person.Id = value;
                OnPropertyChanged();
            }
        }

        public override string Name
        {
            get => _person.Name;
            set
            {
                if (_person.Name == value)
                    return;
                _person.Name = value;
                OnPropertyChanged();
            }
        }

        public override bool? IsVIP
        {
            get => _person?.IsVIP;
            set
            {
                if (_person?.IsVIP == value)
                    return;

                _person.IsVIP = (bool)value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _person.PhoneNumber;
            set
            {
                if (_person.PhoneNumber == value)
                    return;
                _person.PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public override decimal TotalBalance
        {
            get => _person.TotalBalance;
            set
            {
                if (_person.TotalBalance == value)
                    return;
                _person.TotalBalance = value;
                OnPropertyChanged();
            }
        }

        public IList<AccountModel> AccountsList
        {
            get => _person.AccountsList;
            set
            {
                if (_person.AccountsList == value)
                    return;
                _person.AccountsList = value;
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
        //public string BackgroundColor
        //{
        //    get
        //    {
        //        return IsVIP ? "LemonChiffon" : "White";
        //    }
        //    //set
        //    //{
        //    //    if (_client.IsVIP == false)
        //    //        _backgroundColor = "White";
        //    //    else
        //    //        _backgroundColor = "Gold";

        //    //    OnPropertyChanged();
        //    //}
        //}

        #endregion Properties

        #region Methods

        //private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        #endregion Methods
    }
}