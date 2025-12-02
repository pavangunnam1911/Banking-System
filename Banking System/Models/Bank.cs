
namespace Banks
{
    public class Bank
    {
        public string BankName { get; set; }
        public string BankId { get; set; } 
        public string BankCountry { get; set; }
        public string BankAddress {  get; set; }
        public Dictionary<string, Account> AccountHolders { get; set; }
        public Dictionary<string, string> Staff { get; set; }
        public Dictionary<string, decimal> CurrencyRates { get; set; }

        public Bank(string bankName, string bankCountry,string bankAddress)
        {
            BankName = bankName;
            BankId = bankName.Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyyy");
            BankCountry = bankCountry;
            BankAddress = bankAddress;

            AccountHolders = new Dictionary<string, Account>();
            Staff = new Dictionary<string, string>();
            CurrencyRates = new Dictionary<string, decimal>() { { "INR", 1 } };

        }
    }
}
