using BankUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.ViewModels
{
    public class ModernBankMainViewModel : BaseViewModel
    {
        private object _currentView;
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ClientsListViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ClientsPageViewModel ClientsListVM { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ModernBankMainViewModel()
        {
            HomeVM = new HomeViewModel();
            ClientsListVM = new ClientsPageViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(() =>
            {
                CurrentView = HomeVM;
            });
            ClientsListViewCommand = new RelayCommand(() =>
            {
                CurrentView = ClientsListVM;
            });
        }
    }
}