using BankUI.Core;
using BankUI.Models;
using CredentialsGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.ViewModels
{
    public class ClientsPageViewModel : BaseViewModel
    {
        #region Fields

        private object _currentClientsView;
        private bool _isVIPSeleceted;
        private bool _isPersonsSelected = true;
        private bool _isCompaniesSelected;
        private RelayCommand _showVIPOnly;
        private RelayCommand _showPersonsOnlyCommand;
        private RelayCommand _showCompaniesOnlyCommand;

        #endregion Fields

        #region Constructors

        public ClientsPageViewModel()
        {
            CurrentClientsView = new PersonsListViewModel(false);
        }

        #endregion Constructors

        #region Properties

        #region Commands

        //public RelayCommand ShowVIPOnlyCommand => _showVIPOnly ??
        //    (_showVIPOnly = new RelayCommand(ShowVIPOnly, CanVIPShow));

        public RelayCommand ShowPersonsOnlyCommand => _showPersonsOnlyCommand ??
            (_showPersonsOnlyCommand = new RelayCommand(ShowPersonsOnly, () => true));

        public RelayCommand ShowCompaniesOnlyCommand => _showCompaniesOnlyCommand ??
            (_showCompaniesOnlyCommand = new RelayCommand(ShowCompaniesOnly, () => true));

        #endregion Commands

        #endregion Properties

        public PersonViewModel PersonsViewModel { get; set; }
        public PersonViewModel CompaniesViewModel { get; set; }

        public bool IsVIPSeleceted
        {
            get => _isVIPSeleceted;
            set
            {
                _isVIPSeleceted = !_isVIPSeleceted;
                OnPropertyChanged();
            }
        }

        public bool IsPersonsSelected
        {
            get => _isPersonsSelected;
            set
            {
                if (_isPersonsSelected == value)
                    return;
                _isPersonsSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsCompaniesSelected
        {
            get => _isCompaniesSelected;
            set
            {
                if (_isCompaniesSelected == value)
                    return;

                _isCompaniesSelected = value;
                OnPropertyChanged();
            }
        }

        public object CurrentClientsView
        {
            get => _currentClientsView;
            set
            {
                _currentClientsView = value;
                OnPropertyChanged();
            }
        }

        #region Methods

        private void ShowPersonsOnly()
        {
            CurrentClientsView = new PersonsListViewModel(_isVIPSeleceted);
        }

        private void ShowCompaniesOnly()
        {
            CurrentClientsView = new CompaniesListViewModel(_isVIPSeleceted);
        }

        #endregion Methods
    }
}