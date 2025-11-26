namespace Bank
{
    public class BankSystem
    {
        DisplayChoices choices = new DisplayChoices();

        Dictionary<string, Account> AccountHolders = new Dictionary<string, Account>();
        Dictionary<string, string> Staff = new Dictionary<string, string>();
        Dictionary<string, decimal> CurrencyRates = new Dictionary<string, decimal>() { { "INR", 1 } };

        decimal SameBankRTGS = 0;
        decimal SameBankIMPS = 5;
        decimal OtherBankRTGS = 2;
        decimal OtherBankIMPS = 6;

        public void CreateStaff(string username, string password)
        {
            if (!Staff.ContainsKey(username))
            {
                Staff.Add(username, password);
                choices.staffSuccess();
            }
        }

        public bool ValidateStaff(string username, string password)
        {
            return Staff.ContainsKey(username) && Staff[username] == password;
        }

        public void CreateAccountHolder(string bank, string username, string password)
        {
            if (AccountHolders.ContainsKey(username))
            {
                Console.WriteLine("Exists");
                return;
            }

            Account account = new Account();
            account.BankName = bank;
            account.Username = username;
            account.Password = password;
            account.AccountId = username.Substring(0, 3).ToUpper() + DateTime.Now.ToString("yyyyMMdd");
            AccountHolders.Add(username, account);
            choices.holderSuccess();
        }

        public bool ValidateAccountHolder(string username, string password)
        {
            return AccountHolders.ContainsKey(username) && AccountHolders[username].Password == password;
        }

        public void UpdateAccount(string username)
        {
            if (AccountHolders.ContainsKey(username))
            {
                Console.WriteLine("Enter new password");
                AccountHolders[username].Password = Console.ReadLine();
                Console.WriteLine("Updated");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }

        public void DeleteAccount(string username)
        {
            if (AccountHolders.ContainsKey(username))
            {
                AccountHolders.Remove(username);
                Console.WriteLine("Successfully deleted user");
            }
            else Console.WriteLine("User not found");
        }

        public void AddCurrency(string CurrencyCode, decimal rate)
        {
            if (!CurrencyRates.ContainsKey(CurrencyCode))
            {
                CurrencyRates.Add(CurrencyCode, rate);
                Console.WriteLine("Added");
            }
        }

        public void Deposit(string username, string currency, decimal amount)
        {
            if (!CurrencyRates.ContainsKey(currency))
            {
                Console.WriteLine("Currency not accepted");
                return;
            }

            decimal inr = amount * CurrencyRates[currency];
            AccountHolders[username].Balance += inr;
            AddTransaction(username, "Bank", inr, "Deposit");
            Console.WriteLine("Amount Deposited Successfully");
        }

        public void Withdraw(string username, decimal amount)
        {
            if (AccountHolders[username].Balance > amount)
            {
                AccountHolders[username].Balance -= amount;
                AddTransaction(username, "Bank", amount, "Withdraw");
                Console.WriteLine("Successful Withdraw");
            }
            else
            {
                Console.WriteLine("Insufficient Funds!");
            }
        }

        public void Transfer(string s, string r, decimal amt)
        {

            if (!AccountHolders.ContainsKey(r))
            {
                Console.WriteLine("Receiver not found");
                return;
            }

            Account sender = AccountHolders[s];
            Account receiver = AccountHolders[r];

            Console.WriteLine("Choose Transfer Type:");
            Console.WriteLine("1. IMPS");
            Console.WriteLine("2. RTGS");
            string type = Console.ReadLine();

            decimal charge = 0;

            bool sameBank = sender.BankName == receiver.BankName;

            if (sameBank)
            {
                if (type == "1") charge = SameBankIMPS;
                else if (type == "2") charge = SameBankRTGS;
                else
                {
                    Console.WriteLine("Invalid Transfer Type");
                    return;
                }
            }
            else
            {
                if (type == "1") charge = OtherBankIMPS;
                else if (type == "2") charge = OtherBankRTGS;
                else
                {
                    Console.WriteLine("Invalid Transfer Type");
                    return;
                }
            }

            if (sender.Balance < amt + charge)
            {
                Console.WriteLine("Insufficient Funds");
                return;
            }

            sender.Balance -= (amt + ((amt / 100) * charge));
            receiver.Balance += amt;

            AddTransaction(s, r, amt, "Transfer");

            Console.WriteLine($"Transfer Successful! Charge = ₹{charge}");

        }

        void AddTransaction(string sender, string receiver, decimal amount, string type)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionId = "TXN" + DateTime.Now.Ticks;
            transaction.Sender = sender;
            transaction.Receiver = receiver;
            transaction.Type = type;
            transaction.Amount = amount;

            if (AccountHolders.ContainsKey(sender))
            {
                AccountHolders[sender].Transactions.Add(transaction);
            }

            if (type == "Transfer" && AccountHolders.ContainsKey(receiver))
            {
                AccountHolders[receiver].Transactions.Add(transaction);
            }

        }

        public void ViewUserTransactions(string username)
        {
            foreach (var transaction in AccountHolders[username].Transactions)
            {
                Console.WriteLine(transaction.TransactionId + " " + transaction.Type + " " + transaction.Amount);
            }
        }

        public void ShowAllTransactions()
        {
            foreach (var accounts in AccountHolders.Values)
            {
                foreach (var transaction in accounts.Transactions)
                {
                    Console.WriteLine(transaction.TransactionId + " " + transaction.Type + " " + transaction.Amount);
                }
            }
        }

        public void RevertTransaction(string txn)
        {
            foreach (var a in AccountHolders.Values)
            {
                var t = a.Transactions.FirstOrDefault(x => x.TransactionId == txn);
                if (t != null)
                {
                    if (AccountHolders.ContainsKey(t.Sender))
                    {
                        AccountHolders[t.Sender].Balance += t.Amount;
                    }

                    if (AccountHolders.ContainsKey(t.Receiver))
                    {
                        AccountHolders[t.Receiver].Balance -= t.Amount;
                    }
                    Console.WriteLine("Reverted");
                    return;
                }
            }
            Console.WriteLine("Transaction not found");
        }

        public void ViewBalace(string username)
        {
            Console.WriteLine($"Your Balance is:- " + AccountHolders[username].Balance);
        }
    }
}