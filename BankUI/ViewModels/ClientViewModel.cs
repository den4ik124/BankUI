using BankUI.DAL;
using BankUI.Interfaces;
using BankUI.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace BankUI.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        #region Fields

        private Window _currentDialogWindow;
        private readonly ClientModel _client;
        private IDialogService _dialogService;

        private bool _isVIP;

        //private readonly PersonModel _person;
        //private ClientModel _concreteClient;
        //private bool _isPerson = true;

        //public Color _backgroundColor;

        private RelayCommand _addNewClient;

        private RelayCommand _closeWindowCommand;

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public ClientViewModel()
        {
        }

        public ClientViewModel(Window openedWindow)
        {
            _currentDialogWindow = openedWindow;
            _dialogService = new DialogService();
            //_person = new PersonModel();// ?? throw new ArgumentNullException(nameof(client));
        }

        public ClientViewModel(ClientModel client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
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
            get => _client?.Name;
            set
            {
                if (_client.Name == value)
                    return;
                _client.Name = value;
                OnPropertyChanged();
            }
        }

        #region useless code

        //public string SurName
        //{
        //    get => _person.SurName;
        //    set
        //    {
        //        if (_person.SurName == value)
        //            return;
        //        _person.SurName = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public string PhoneNumber
        //{
        //    get => _person.PhoneNumber;
        //    set
        //    {
        //        if (_person.PhoneNumber == value)
        //            return;
        //        _person.PhoneNumber = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public string PersonalCode
        //{
        //    get => _person.PersonalCode;
        //    set
        //    {
        //        if (_person.PersonalCode == value)
        //            return;
        //        _person.PersonalCode = value;
        //        OnPropertyChanged();
        //    }
        //}

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

        #endregion useless code

        //public bool IsVIPSelected { get; set; } = false;

        public virtual bool IsVIP
        {
            get => _isVIP;
            set
            {
                if (_isVIP == value)
                    return;

                //TODO креш при переключении CheckBox isVIP
                _isVIP = value;
                OnPropertyChanged();
            }
        }

        public bool IsPerson { get; set; }
        public bool CanBeClosed { get; set; }

        public string BackgroundColor
        {
            get
            {
                return (bool)IsVIP ? "LemonChiffon" : "White";
            }
        }

        public virtual decimal TotalBalance
        {
            get => _client.TotalBalance;
            set
            {
                if (_client.TotalBalance == value)
                    return;
                _client.TotalBalance = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddNewClient => (_addNewClient) ??
            (_addNewClient = new RelayCommand(NewClientAdd, CanAddNewClient));

        public RelayCommand CloseWindowCommand => (_closeWindowCommand) ??
            (_closeWindowCommand = new RelayCommand(CloseWindow, CanAddNewClient));

        public Window CurrentDialogWindow { get => _currentDialogWindow; set => _currentDialogWindow = value; }

        #endregion Properties

        #region Methods

        private void CloseWindow()
        {
            _dialogService.CloseWindow(CurrentDialogWindow);
        }

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

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}