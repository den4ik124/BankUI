using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Models.Accounts
{
    public class DepositAccountModel : AccountBaseModel
    {
        private decimal _startBalance;
        private double _interestRateYear;
        private int _depositDuration;
        private bool _isCapitalization;

        public DepositAccountModel(ClientModel client, decimal startBalance, double interestRateYear, int depositDuration, bool isCapitalization) : base(client, startBalance)
        {
            Id = "_D" + client.Id.ToString() + $"|{_nextId++}";
            _startBalance = startBalance;
            _interestRateYear = interestRateYear;
            _depositDuration = depositDuration;
            _isCapitalization = isCapitalization;
        }
    }
}