namespace BankUI.Models
{
    public class PersonModel : ClientModel
    {
        #region Fields

        private string surName;
        private string personalCode;
        private string phoneNumber;

        #endregion Fields

        #region Constructors

        public PersonModel()
        {
        }

        public PersonModel(string Name, bool isVIP, string SurName, string PersonalCode, string PhoneNumber) : base(Name, 0, isVIP)
        {
            surName = SurName;
            personalCode = PersonalCode;
            phoneNumber = PhoneNumber;
        }

        #endregion Constructors

        #region Properties

        public string SurName { get => surName; set => surName = value; }
        public string PersonalCode { get => personalCode; set => personalCode = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        #endregion Properties
    }
}