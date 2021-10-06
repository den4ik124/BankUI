using BankUI.Interfaces;
using BankUI.Models;
using System.Collections.ObjectModel;

namespace BankUI.ViewModels
{
    public class PersonViewModel : ClientViewModel
    {
        #region Fields

        private PersonModel _person;
        //private string _backgroundColor;

        #endregion Fields

        #region Constructors

        public PersonViewModel(ClientModel person) : base(person)
        {
            this._person = person as PersonModel;
        }

        #endregion Constructors

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

        public override bool IsVIP
        {
            get => _person.IsVIP;
            set
            {
                if (_person.IsVIP == value)
                    return;

                _person.IsVIP = value;
                OnPropertyChanged();
            }
        }

        public override string PhoneNumber
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

        public override ObservableCollection<IAccount> AccountsList
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

        #endregion Properties
    }
}