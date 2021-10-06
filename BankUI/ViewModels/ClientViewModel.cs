using BankUI.Core;
using BankUI.DAL;
using BankUI.HelpClasses;
using BankUI.Interfaces;
using BankUI.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace BankUI.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        #region Fields

        private Window _currentDialogWindow;
        private ClientModel _client;
        private IDialogService _dialogService;

        //private string _name;
        //private string _surName;
        //private string _phoneNumber;

        //private string _personalCode;
        //private string _companyCode;

        private bool _isVIP;

        private RelayCommand _addNewClient;

        private RelayCommand _closeWindowCommand;

        #endregion Fields

        #region Constructors

        public ClientViewModel(Window openedWindow)
        {
            _currentDialogWindow = openedWindow;
            _dialogService = new DialogService();
        }

        public ClientViewModel(ClientModel client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        #endregion Constructors

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
            //get => _name;
            //set
            //{
            //    if (_name == value)
            //        return;
            //    _name = value;
            //    OnPropertyChanged();
            //}
        }

        public virtual string SurName { get; set; }
        //{
        //    get => _surName;
        //    set
        //    {
        //        if (_surName == value)
        //            return;
        //        _surName = value;
        //        OnPropertyChanged();
        //    }
        //}

        public virtual string PhoneNumber { get; set; }
        //{
        //    get => _phoneNumber;
        //    set
        //    {
        //        if (_phoneNumber == value)
        //            return;
        //        _phoneNumber = value;
        //        OnPropertyChanged();
        //    }
        //}

        public virtual string PersonalCode { get; set; }

        public virtual string CompanyCode { get; set; }
        //{
        //    get => _companyCode;
        //    set
        //    {
        //        if (_companyCode == value)
        //            return;
        //        _companyCode = value;
        //        OnPropertyChanged();
        //    }
        //}

        public bool IsVIP
        {
            get => _isVIP;
            set
            {
                if (_isVIP == value)
                    return;
                _isVIP = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public virtual ObservableCollection<IAccount> AccountsList { get; set; }

        public bool IsPerson { get; set; }
        public bool CanBeClosed { get; set; }

        public string BackgroundColor
        {
            get => IsVIP ? "#FF738D00" : "Transparent";
        }

        public string FontColor
        {
            get => IsVIP ? "Black" : "White";
        }

        public decimal TotalBalance// { get; set; }
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
            ClientModel concreteClient;
            if (IsPerson)
                concreteClient = new PersonModel(Name, IsVIP, SurName, PersonalCode, PhoneNumber);
            else
                concreteClient = new CompanyModel(Name, CompanyCode, IsVIP);
            concreteClient.AddClientToDB();
            ClientsDBModel.UpdateClients();
            _dialogService.CloseWindow(_currentDialogWindow);
        }

        private bool CanAddNewClient()
        {
            return true;
        }

        #endregion Methods
    }
}