using System;
using System.Collections.Generic;

namespace Banks
{
    public class Account
    {
        public string BankName { get; set; }
        public string BankID { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account(string bankName, string username, string password)
        {
            BankName = bankName;
            Username = username.ToLower();
            Password = password;
            BankID = bankName.Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyyy");
            AccountId = username.Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyyy");
            Balance = 0;
            Transactions = new List<Transaction>();
        }
    }
}
