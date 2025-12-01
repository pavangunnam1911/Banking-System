using System;
using System.Linq;
using Banks;

namespace Banking_System.Controller
{
    public class BankService
    {
        private Bank bank;
        DisplayChoices choices = new DisplayChoices();
        

        decimal SameBankRTGS = 0;
        decimal SameBankIMPS = 5;
        decimal OtherBankRTGS = 2;
        decimal OtherBankIMPS = 6;

        public BankService(Bank bank)
        {
            this.bank = bank;
        }

        public void createBank(string bankName,string bankCountry)
        {
            bankName = bankName.ToUpper();
            bankCountry = bankCountry.ToUpper();

            bool exists = bank.IndividualBank.Values.Any(b => b.ToUpper() == bankName);

            if (exists)
            {
                Console.WriteLine("Bank Already Exists");
            }
            else
            {
                string bankId = bankName.Substring(0, 3).ToUpper() + DateTime.Now.ToString("ddMMyyyy");

                bank.IndividualBank.Add(bankId, bankName);
                bank.IndividualBank.Add(bankName, bankCountry);
            }
        }

        public bool chooseBank(string bankName)
        {
            bankName = bankName.ToUpper();

            bool exists = bank.IndividualBank.Values.Any(b => b.ToUpper() == bankName);

            if (exists)
            {
                Console.WriteLine($"Welcome to {bankName} bank");
                return true;
            }
            else
            {
                Console.WriteLine("Bank Doesn't Exist");
                return false;
            }
        }


        public void CreateStaff(string username, string password)
        {
            if (bank.Staff.ContainsKey(username))
            {
                Console.WriteLine("Staff already exists");
            }
            else
            {
                bank.Staff.Add(username, password);
                choices.staffSuccess();
            }
        }

        public bool ValidateStaff(string username, string password)
        {
            if (bank.Staff.ContainsKey(username) && bank.Staff[username] == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateAccountHolder(string bankName, string username, string password)
        {
            username = username.ToLower();

            if (bank.AccountHolders.ContainsKey(username))
            {
                Console.WriteLine("Account Holder Already Exists");
                return;
            }

            Account account = new Account(bankName, username, password);
            bank.AccountHolders.Add(username, account);
            choices.holderSuccess();
        }


        public bool ValidateAccountHolder(string username, string password)
        {
            username = username.ToLower();

            if (bank.AccountHolders.ContainsKey(username) && bank.AccountHolders[username].Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateAccount(string username)
        {
            username = username.ToLower();

            if (!bank.AccountHolders.ContainsKey(username))
            {
                Console.WriteLine("User not found");
                return;
            }

            Console.WriteLine("1. Update Username");
            Console.WriteLine("2. Update Password");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                {
                    Console.WriteLine("Enter new username:");
                    string newUsername = Console.ReadLine().ToLower();

                    if (bank.AccountHolders.ContainsKey(newUsername))
                    {
                        Console.WriteLine("Username already exists");
                    }
                    else
                    {
                        Account acc = bank.AccountHolders[username];
                        bank.AccountHolders.Remove(username);
                        acc.Username = newUsername;
                        bank.AccountHolders.Add(newUsername, acc);
                        Console.WriteLine("Username updated successfully");
                    }
                    break;
                }

                case "2":
                {
                    Console.WriteLine("Enter new password:");
                    bank.AccountHolders[username].Password = Console.ReadLine();
                    Console.WriteLine("Password updated successfully");
                    break;
                }

                default:
                {
                    Console.WriteLine("Please enter a valid choice");
                    break;
                }
            }
        }

        public void DeleteAccount(string username)
        {
            if (bank.AccountHolders.ContainsKey(username))
            {
                bank.AccountHolders.Remove(username);
                Console.WriteLine("Successfully deleted user");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }

        public void AddCurrency(string code, decimal rate)
        {
            code = code.ToUpper();

            if (bank.CurrencyRates.ContainsKey(code))
            {
                Console.WriteLine("Currency Already Exists");
            }
            else
            {
                bank.CurrencyRates.Add(code, rate);
                Console.WriteLine("Added");
            }
        }

        public void UpdateServiceCharge()
        {
            Console.WriteLine("1. Same Bank IMPS");
            Console.WriteLine("2. Same Bank RTGS");
            Console.WriteLine("3. Other Bank IMPS");
            Console.WriteLine("4. Other Bank RTGS");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                {
                    Console.WriteLine("Enter new charge:");
                    SameBankIMPS = Convert.ToDecimal(Console.ReadLine());
                    break;
                }

                case "2":
                {
                    Console.WriteLine("Enter new charge:");
                    SameBankRTGS = Convert.ToDecimal(Console.ReadLine());
                    break;
                }

                case "3":
                {
                    Console.WriteLine("Enter new charge:");
                    OtherBankIMPS = Convert.ToDecimal(Console.ReadLine());
                    break;
                }

                case "4":
                {
                    Console.WriteLine("Enter new charge:");
                    OtherBankRTGS = Convert.ToDecimal(Console.ReadLine());
                    break;
                }

                default:
                {
                    Console.WriteLine("Invalid choice");
                    break;
                }
            }

            Console.WriteLine("Updated Successfully");
        }

        public void Deposit(string username, string currency, decimal amount)
        {
            currency = currency.ToUpper();

            if (!bank.CurrencyRates.ContainsKey(currency))
            {
                Console.WriteLine("Currency not accepted");
                return;
            }

            decimal inr = amount * bank.CurrencyRates[currency];
            bank.AccountHolders[username].Balance += inr;

            AddTransaction(username, "Bank", inr, "Deposit");

            Console.WriteLine("Amount Deposited Successfully");
        }

        public void Withdraw(string username, decimal amount)
        {
            if (bank.AccountHolders[username].Balance >= amount)
            {
                bank.AccountHolders[username].Balance -= amount;
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
            if (!bank.AccountHolders.ContainsKey(r))
            {
                Console.WriteLine("Receiver not found");
                return;
            }

            Account sender = bank.AccountHolders[s];
            Account receiver = bank.AccountHolders[r];

            Console.WriteLine("1. IMPS");
            Console.WriteLine("2. RTGS");
            string type = Console.ReadLine();

            decimal charge = 0;
            bool sameBank = sender.BankName == receiver.BankName;

            if (sameBank)
            {
                if (type == "1")
                {
                    charge = SameBankIMPS;
                }
                else if (type == "2")
                {
                    charge = SameBankRTGS;
                }
                else
                {
                    Console.WriteLine("Invalid Transfer Type");
                    return;
                }
            }
            else
            {
                if (type == "1")
                {
                    charge = OtherBankIMPS;
                }
                else if (type == "2")
                {
                    charge = OtherBankRTGS;
                }
                else
                {
                    Console.WriteLine("Invalid Transfer Type");
                    return;
                }
            }

            decimal total = amt + (amt * charge / 100);

            if (sender.Balance < total)
            {
                Console.WriteLine("Insufficient Funds");
                return;
            }

            sender.Balance -= total;
            receiver.Balance += amt;

            AddTransaction(s, r, amt, "Transfer");

            Console.WriteLine("Transfer Successful!");
        }

        void AddTransaction(string sender, string receiver, decimal amount, string type)
        {
            string bankId = bank.AccountHolders[sender].BankID;
            string accountId = bank.AccountHolders[sender].AccountId;
            Transaction transaction = new Transaction(bankId, accountId, sender, receiver, amount, type);

            if (bank.AccountHolders.ContainsKey(sender))
            {
                bank.AccountHolders[sender].Transactions.Add(transaction);
            }

            if (type == "Transfer" && bank.AccountHolders.ContainsKey(receiver))
            {
                bank.AccountHolders[receiver].Transactions.Add(transaction);
            }
        }


        public void ViewUserTransactions(string username)
        {
            if (!bank.AccountHolders[username].Transactions.Any())
            {
                Console.WriteLine("No transactions available.");
                return;
            }

            foreach (var t in bank.AccountHolders[username].Transactions)
            {
                Console.WriteLine(t.TransactionId + " " + t.Type + " " + t.Amount);
            }
        }

        public void ShowAllTransactions()
        {
            bool found = false;

            foreach (var acc in bank.AccountHolders.Values)
            {
                foreach (var t in acc.Transactions)
                {
                    Console.WriteLine(t.TransactionId + " " + t.Type + " " + t.Amount);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No transactions found.");
            }
        }

        public void RevertTransaction(string txn)
        {
            foreach (var account in bank.AccountHolders.Values)
            {
                Transaction t = account.Transactions.FirstOrDefault(x => x.TransactionId == txn);

                if (t != null)
                {
                    if (t.Type != "Transfer")
                    {
                        Console.WriteLine("Only transfer transactions can be reverted.");
                        return;
                    }

                    bank.AccountHolders[t.Sender].Balance += t.Amount;
                    bank.AccountHolders[t.Receiver].Balance -= t.Amount;

                    Console.WriteLine("Reverted");
                    return;
                }
            }

            Console.WriteLine("Transaction not found");
        }

        public void ViewBalance(string username)
        {
            Console.WriteLine("Your Balance is: " + bank.AccountHolders[username].Balance);
        }
    }
}
