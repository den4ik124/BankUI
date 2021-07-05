using BankUI.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace BankUI.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly ClientModel _client;
        private readonly PersonModel _person;
        //private ClientModel _concreteClient;
        //private bool _isPerson = true;

        //public Color _backgroundColor;

        private RelayCommand _addNewClient;

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public ClientViewModel(ClientModel client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public ClientViewModel()
        {
            //_person = new PersonModel();// ?? throw new ArgumentNullException(nameof(client));
        }

        #endregion Constructors

        #region Properties

        //public ClientModel ConcreteClient
        //{
        //    get => _concreteClient;

        //    set
        //    {
        //        if (_concreteClient == value)
        //            return;
        //        _concreteClient = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public int Id => _client.Id;
        public virtual int Id { get; set; }

        public virtual string Name
        {
            //get => _client.Name;
            get => _client.Name;
            set
            {
                if (_client.Name == value)
                    return;
                _client.Name = value;
                OnPropertyChanged();
            }
        }

        public string SurName
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

        public string PersonalCode
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

        //TODO добавить _company
        //public string CompanyCode
        //{
        //    get => _company.CompanyCode;
        //    set
        //    {
        //        if (_company.CompanyCode == value)
        //            return;
        //        _company.CompanyCode = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public bool IsVIP
        //{
        //    get => _client.IsVIP;
        //    set
        //    {
        //        if (_client.IsVIP == value)
        //            return;

        //        _client.IsVIP = value;
        //        OnPropertyChanged();
        //    }
        //}
        public virtual bool IsVIP
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

        public bool IsPerson { get; set; }
        public bool CanBeClosed { get; set; }

        public string BackgroundColor
        {
            get
            {
                return IsVIP ? "LemonChiffon" : "White";
            }
        }

        public RelayCommand AddNewClient => (_addNewClient) ??
            (_addNewClient = new RelayCommand(NewClientAdd, CanAddNewClient));

        private void NewClientAdd()
        {
            //    if (IsPerson)
            //        ConcreteClient = new PersonModel(Name, IsVIP, SurName, PersonalCode, PhoneNumber);
            //    else
            //        ConcreteClient = new CompanyModel(Name, CompanyCode: "some test code", true);//TODO добавить обработку нового клиента
            //    CanBeClosed = true;
        }

        private bool CanAddNewClient()
        {
            return true;
        }

        #endregion Properties

        #region Methods

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}