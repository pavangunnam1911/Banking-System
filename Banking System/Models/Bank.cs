using System;
using System.Collections.Generic;

namespace Banks
{
    public class Bank
    {
        public string BankName { get; set; }
        public string BankId { get; set; } 
        public string BankCountry { get; set; }

        public Dictionary<string, string> IndividualBank { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, Account> AccountHolders { get; set; } = new Dictionary<string, Account>();
        public Dictionary<string, string> Staff { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, decimal> CurrencyRates { get; set; } = new Dictionary<string, decimal>() { { "INR", 1 } };

        public Bank(string bankName, string bankCountry)
        {
            BankName = bankName;
            BankId = bankName.Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyyy");
            BankCountry = bankCountry;
        }

    }
}
