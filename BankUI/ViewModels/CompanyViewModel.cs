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

        public override string Name
        {
            get => _companyModel.Name;
            set
            {
                if (_companyModel.Name == value)
                    return;
                _companyModel.Name = value;
                OnPropertyChanged();
            }
        }

        public override string CompanyCode
        {
            get => _companyModel.CompanyCode;
            set
            {
                if (_companyModel.CompanyCode == value)
                    return;
                _companyModel.CompanyCode = value;
                OnPropertyChanged();
            }
        }

        #endregion Constructors

        #region Properties

        public override int Id
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

        public override bool IsVIP
        {
            get => _companyModel.IsVIP;
            set
            {
                if (_companyModel.IsVIP == value)
                    return;

                _companyModel.IsVIP = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}