using System.Collections.Generic;
using System.Linq;

namespace BankUI.Models
{
    public static class ClientsDBModel
    {
        #region Fields

        private static IList<ClientModel> _clients;
        private static IList<PersonModel> _persons;
        private static IList<CompanyModel> _companies;

        #endregion Fields

        #region Properties

        public static IList<ClientModel> Clients { get => _clients; set => _clients = value; }
        public static IList<PersonModel> Persons { get => _clients.OfType<PersonModel>().ToList(); set => _persons = value; }
        public static IList<CompanyModel> Companies { get => _clients.OfType<CompanyModel>().ToList(); set => _companies = value; }

        #endregion Properties
    }
}