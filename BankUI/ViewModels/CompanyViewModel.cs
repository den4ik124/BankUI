using BankUI.Models;

namespace BankUI.ViewModels
{
    internal class CompanyViewModel : ClientViewModel
    {
        #region Fields

        private CompanyModel _companyModel;

        #endregion Fields

        #region Constructors

        public CompanyViewModel(ClientModel company)
        {
            _companyModel = company as CompanyModel;
        }

        public new string Name => _companyModel.Name;
        public string CompanyCode => _companyModel.CompanyCode;

        #endregion Constructors

        #region Properties

        public new int Id
        {
            get => _companyModel.Id;
            set
            {
                if (_companyModel.Id == value)
                    return;
                _companyModel.Id = value;
                OnPropertyChanged();
            }
        }

        public override bool? IsVIP
        {
            get => _companyModel?.IsVIP;
            set
            {
                if (_companyModel?.IsVIP == value)
                    return;

                _companyModel.IsVIP = (bool)value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}