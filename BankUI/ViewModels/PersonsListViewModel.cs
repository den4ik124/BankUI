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
    public class PersonsListViewModel : BaseViewModel
    {
        private bool _isVIP;

        public PersonsListViewModel(bool isVIP)
        {
            _isVIP = isVIP;
        }

        private List<ClientModel> _clients = new List<ClientModel> {
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code1","0001"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code2","0003"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code3","0003"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code4","0004"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        new PersonModel(Generator.RandomName(), Generator.RandomVIP(),Generator.RandomSurname(),"test code5","0005"),
        };

        public List<ClientModel> Clients
        {
            get => _clients.Where(item => item.IsVIP == _isVIP).ToList();
        }

        public bool IsVIP { get => _isVIP; set => _isVIP = value; }
    }
}