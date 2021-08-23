using BankUI.DAL;
using BankUI.Interfaces;
using BankUI.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BankUI.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        #region Fields

        private Window _currentDialogWindow;
        private ClientModel _client;
        private IDialogService _dialogService;

        private string _name;
        private string _surName;
        private string _phoneNumber;
        private string _personalCode;
        private string _companyCode;

        private bool _isVIP;

        private RelayCommand _addNewClient;

        private RelayCommand _closeWindowCommand;

        #endregion Fields

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

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

        public virtual int Id { get; set; }

        public virtual string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public virtual string SurName
        {
            get => _surName;
            set
            {
                if (_surName == value)
                    return;
                _surName = value;
                OnPropertyChanged();
            }
        }

        public virtual string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber == value)
                    return;
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public virtual string PersonalCode
        {
            get => _personalCode;
            set
            {
                if (_personalCode == value)
                    return;
                _personalCode = value;
                OnPropertyChanged();
            }
        }

        public virtual string CompanyCode
        {
            get => _companyCode;
            set
            {
                if (_companyCode == value)
                    return;
                _companyCode = value;
                OnPropertyChanged();
            }
        }

        public virtual bool IsVIP
        {
            get => _isVIP;
            set
            {
                if (_isVIP == value)
                    return;
                _isVIP = value;
                OnPropertyChanged();
            }
        }

        public virtual ObservableCollection<IAccount> AccountsList { get; set; }

        public bool IsPerson { get; set; }
        public bool CanBeClosed { get; set; }

        public string BackgroundColor
        {
            get => IsVIP ? "LemonChiffon" : "White";
        }

        public virtual decimal TotalBalance { get; set; }

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
            ClientsDBModel.AddClient(concreteClient);
            _dialogService.CloseWindow(_currentDialogWindow);
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