
namespace Banks
{
    public class Bank
    {
        public string BankName { get; set; }
        public string BankId { get; init; } 
        public string BankCountry { get; set; }
        public string BankAddress {  get; set; }
        public decimal otherBankRTGS { get; set; }
        public decimal otherBankIMPS { get; set; }
        public decimal sameBankRTGS { get; set; }
        public decimal sameBankIMPS { get; set; }
        public Dictionary<string, Account> AccountHolders { get; set; }
        public Dictionary<string, string> Staff { get; set; }
        public Dictionary<string, decimal> CurrencyRates { get; set; }

        public Bank(string bankName, string bankCountry,string bankAddress,string RTGSother,string IMPSother,int RTGSsame =0,int IMPSsame=5)
        {
            BankName = bankName;
            BankId = bankName.Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyyy");
            BankCountry = bankCountry;
            BankAddress = bankAddress;
            otherBankRTGS = decimal.Parse(RTGSother);
            otherBankIMPS = decimal.Parse(IMPSother);
            sameBankRTGS = RTGSsame;
            sameBankIMPS = IMPSsame;

            AccountHolders = new Dictionary<string, Account>();
            Staff = new Dictionary<string, string>();
            CurrencyRates = new Dictionary<string, decimal>() { { "INR", 1 } };

        }
    }
}
