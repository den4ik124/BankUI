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
    public class CompaniesListViewModel : BaseViewModel
    {
        private bool _isVIP;

        public CompaniesListViewModel(bool isVIP)
        {
            _isVIP = isVIP;
        }

        private List<ClientModel> _clients = new List<ClientModel> {
        new CompanyModel("Company #1",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #2",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #3",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #4",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #5",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #6",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #7",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #8",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        new CompanyModel("Company #9",Guid.NewGuid().ToString(),Generator.RandomVIP()),
        };

        public List<ClientModel> Clients
        {
            get => _clients;
            //get => _clients.Where(item => item.IsVIP == _isVIP).ToList();
        }

        public bool IsVIP { get => _isVIP; set => _isVIP = value; }
    }
}