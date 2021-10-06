using BankUI.Models;

namespace BankUI.ViewModels
{
    public class PersonViewModel : ClientViewModel
    {
        #region Fields

        private PersonModel _person;

        #endregion Fields

        #region Constructors

        public PersonViewModel(ClientModel person) : base(person)
        {
            this._person = person as PersonModel;
        }

        #endregion Constructors

        #region Properties

        //public override int Id
        //{
        //    get => _person.Id;
        //    set
        //    {
        //        if (_person.Id == value)
        //            return;
        //        _person.Id = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public override string Name
        //{
        //    get => _person.Name;
        //    set
        //    {
        //        if (_person.Name == value)
        //            return;
        //        _person.Name = value;
        //        OnPropertyChanged();
        //    }
        //}

        public override string SurName
        {
            get => _person.SurName;
            set
            {
                if (_person.SurName == value)
                    return;
                _person.SurName = value;
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

        public override string PersonalCode
        {
            get => _person.PersonalCode;
            set
            {
                if (_person.PersonalCode == value)
                    return;
                _person.PersonalCode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Расскомментировать в случае работы со старым UI
        /// </summary>
        //public override bool IsVIP
        //{
        //    get => _person.IsVIP;
        //    set
        //    {
        //        if (_person.IsVIP == value)
        //            return;

        //        _person.IsVIP = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public override decimal TotalBalance
        //{
        //    get => _person.TotalBalance;
        //    set
        //    {
        //        if (_person.TotalBalance == value)
        //            return;
        //        _person.TotalBalance = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public override ObservableCollection<IAccount> AccountsList
        //{
        //    get => _person.AccountsList;
        //    set
        //    {
        //        if (_person.AccountsList == value)
        //            return;
        //        _person.AccountsList = value;
        //        OnPropertyChanged();
        //    }
        //}

        #endregion Properties
    }
}