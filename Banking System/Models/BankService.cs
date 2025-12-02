using Banks;

namespace Banking_System.Models
{
    public class BankService
    {
        private readonly Bank bank;

        DisplayChoices choices = new DisplayChoices();


        public BankService(Bank bank)
        {
            this.bank = bank;
            SameBankRTGS = bank.sameBankRTGS;
            SameBankIMPS = bank.sameBankIMPS;
            OtherBankRTGS = bank.otherBankRTGS;
            OtherBankIMPS = bank.otherBankIMPS;
        }


        decimal SameBankRTGS = 0;
        decimal SameBankIMPS = 5;
        decimal OtherBankRTGS = 2;
        decimal OtherBankIMPS = 6;

        public void CreateStaff(string username, string password)
        {
            try
            {
                if (bank.Staff.ContainsKey(username))
                {
                    Console.WriteLine("Staff already exists");
                    return;
                }

                bank.Staff.Add(username, password);
                choices.staffSuccess();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool ValidateStaff(string username, string password)
        {
            try
            {
                return bank.Staff.ContainsKey(username) && bank.Staff[username] == password;
            }
            catch
            {
                return false;
            }
        }

        public void CreateAccountHolder(string bankName, string username, string password)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool ValidateAccountHolder(string username, string password)
        {
            try
            {
                username = username.ToLower();
                return bank.AccountHolders.ContainsKey(username) && bank.AccountHolders[username].Password == password;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateAccount(string username)
        {
            try
            {
                username = username.ToLower();

                if (!bank.AccountHolders.ContainsKey(username))
                {
                    Console.WriteLine("User not found");
                    return;
                }

                string newUsername = InputCheck.ReadString("Enter new username: ").ToLower();

                if (bank.AccountHolders.ContainsKey(newUsername))
                {
                    Console.WriteLine("Username already exists.");
                    return;
                }

                string newPassword = InputCheck.ReadString("Enter new password: ");

                Account account = bank.AccountHolders[username];
                bank.AccountHolders.Remove(username);
                account.Username = newUsername;
                account.Password = newPassword;
                bank.AccountHolders.Add(newUsername, account);

                Console.WriteLine("Updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteAccount(string username)
        {
            try
            {
                if (bank.AccountHolders.ContainsKey(username))
                {
                    bank.AccountHolders.Remove(username);
                    Console.WriteLine("Successfully deleted");
                }
                else
                {
                    Console.WriteLine("User not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddCurrency(string code, decimal rate)
        {
            try
            {
                code = code.ToUpper();

                if (bank.CurrencyRates.ContainsKey(code))
                {
                    Console.WriteLine("Currency Already Exists");
                    return;
                }

                bank.CurrencyRates.Add(code, rate);
                Console.WriteLine("Added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    SameBankIMPS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.sameBankIMPS = SameBankIMPS;
                    break;

                case "2":
                    SameBankRTGS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.sameBankRTGS = SameBankRTGS;
                    break;

                case "3":
                    OtherBankIMPS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.otherBankIMPS = OtherBankIMPS;
                    break;

                case "4":
                    OtherBankRTGS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.otherBankRTGS = OtherBankRTGS;
                    break;

                default:
                    Console.WriteLine("Invalid");
                    return;
            }

            Console.WriteLine("Updated Successfully");
        }


        public void Deposit(string username, string currency, decimal amount)
        {
            try
            {
                currency = currency.ToUpper();
                if (!bank.AccountHolders.TryGetValue(username, out var acc))
                {
                    Console.WriteLine("User not found");
                    return;
                }

                if (!bank.CurrencyRates.TryGetValue(currency, out decimal rate))
                {
                    Console.WriteLine("Currency not accepted");
                    return;
                }

                decimal inr = amount * rate;
                acc.Balance += inr;

                AddTransaction(username, "Bank", inr, "Deposit");
                Console.WriteLine("Deposited");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Withdraw(string username, decimal amount)
        {
            try
            {
                if (!bank.AccountHolders.TryGetValue(username, out var acc))
                {
                    Console.WriteLine("User not found");
                    return;
                }

                if (acc.Balance < amount)
                {
                    Console.WriteLine("Insufficient Funds");
                    return;
                }

                acc.Balance -= amount;


                AddTransaction(username, "Bank", amount, "Withdraw");
                Console.WriteLine("Withdraw Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Transfer(string s, string r, decimal amt)
        {
            try
            {
                if (!bank.AccountHolders.TryGetValue(s, out var sender))
                {
                    Console.WriteLine("Sender not found");
                    return;
                }

                if (!bank.AccountHolders.TryGetValue(r, out var receiver))
                {
                    Console.WriteLine("Receiver not found");
                    return;
                }

                Console.WriteLine("1. IMPS\n2. RTGS");
                string type = Console.ReadLine();

                decimal charge = 0;
                bool sameBank = sender.BankName == receiver.BankName;

                if (sameBank && type == "1") charge = SameBankIMPS;
                else if (sameBank && type == "2") charge = SameBankRTGS;
                else if (!sameBank && type == "1") charge = OtherBankIMPS;
                else if (!sameBank && type == "2") charge = OtherBankRTGS;
                else
                {
                    Console.WriteLine("Invalid type");
                    return;
                }

                decimal total = amt + amt * charge / 100;

                if (sender.Balance < total)
                {
                    Console.WriteLine("Insufficient");
                    return;
                }

                sender.Balance -= total;
                receiver.Balance += total;

                AddTransaction(s, r, total, "Transfer");
                Console.WriteLine("Transfer Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void AddTransaction(string sender, string receiver, decimal amount, string type)
        {
            try
            {
                string bankId = bank.AccountHolders[sender].BankID;
                string accountId = bank.AccountHolders[sender].AccountId;

                Transaction t = new Transaction(bankId, accountId, sender, receiver, amount, type);

                if (bank.AccountHolders.ContainsKey(sender))
                {
                    bank.AccountHolders[sender].Transactions.Add(t);
                }
                    

                if (type == "Transfer" && bank.AccountHolders.ContainsKey(receiver))
                {
                    bank.AccountHolders[receiver].Transactions.Add(t);
                }
                    
            }
            catch { }
        }

        public void ViewUserTransactions(string username)
        {
            try
            {
                if (!bank.AccountHolders.TryGetValue(username, out var account))
                {
                    Console.WriteLine("User not found");
                    return;
                }

                if (!account.Transactions.Any())
                {
                    Console.WriteLine("No transactions");
                    return;
                }

                foreach (var t in account.Transactions)
                    Console.WriteLine($"{t.TransactionId} {t.Type} {t.Amount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowAllTransactions()
        {
            try
            {
                bool found = false;

                foreach (var acc in bank.AccountHolders.Values)
                {
                    foreach (var t in acc.Transactions)
                    {
                        Console.WriteLine($"{t.TransactionId} {t.Type} {t.Amount}");
                        found = true;
                    }
                }

                if (!found)
                    Console.WriteLine("No transactions");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RevertTransaction(string txn)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewBalance(string username)
        {
            try
            {
                if (bank.AccountHolders.ContainsKey(username))
                    Console.WriteLine($"Balance: {bank.AccountHolders[username].Balance}");
                else
                    Console.WriteLine("User not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
