using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace BankUI.ViewModels
{
    public class PersonsListViewModel : ClientsPageViewModel
    {
        private List<PersonViewModel> _persons;

        public ICollectionView Persons { get; set; }

        public PersonsListViewModel()
        {
            _persons = new List<PersonViewModel>();
            Persons = CollectionViewSource.GetDefaultView(_persons);
        }

        //public List<ClientModel> Clients
        //{
        //    get => _persons;
        //    //get => _clients.Where(item => item.IsVIP == _isVIP).ToList();
        //}
    }
}