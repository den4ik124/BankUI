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
    public class PersonsListViewModel : ClientsPageViewModel
    {
        private List<ClientModel> _clients = new List<ClientModel>();

        public PersonsListViewModel()
        {
            for (int i = 0; i < 10; i++)
                _clients.Add(new PersonModel(Generator.RandomName(), Generator.RandomVIP(), Generator.RandomSurname(), $"test code{i}", $"000{i}"));
        }

        public string BackgroundColor
        {
            get => base.IsVIPSeleceted ? "LemonChiffon" : "Transparent";
        }

        public List<ClientModel> Clients
        {
            get => _clients;
            //get => _clients.Where(item => item.IsVIP == _isVIP).ToList();
        }
    }
}