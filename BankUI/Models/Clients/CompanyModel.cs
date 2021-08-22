namespace BankUI.Models
{
    public class CompanyModel : ClientModel
    {
        private string _companyCode;
        public string CompanyCode { get => _companyCode; set => _companyCode = value; }

        public CompanyModel(string Name, string CompanyCode, bool isVIP) : base(Name, isVIP)
        {
            _companyCode = CompanyCode;
        }
    }
}