using Banking_System.View;
using Banks;
namespace Banking_System.Models
{
    public class BankService
    {
        private readonly Bank bank;

        private readonly Dictionary<string, Bank> allBanks;

        ConstantStrings cs = new ConstantStrings();

        DisplayChoices choices = new DisplayChoices();

        decimal SameBankRTGS;
        decimal SameBankIMPS;
        decimal OtherBankRTGS;
        decimal OtherBankIMPS;

        public BankService(Bank bank, Dictionary<string, Bank> allBanks)
        {
            this.bank = bank;
            this.allBanks = allBanks;
            SameBankRTGS = bank.SameBankRTGS;
            SameBankIMPS = bank.SameBankIMPS;
            OtherBankRTGS = bank.OtherBankRTGS;
            OtherBankIMPS = bank.OtherBankIMPS;
        }


        public void CreateStaff(string username, string password)
        {
            try
            {
                if (bank.Staff.ContainsKey(username))
                {
                    cs.Staffexist.Write();
                    return;
                }
                bank.Staff.Add(username, password);
                cs.StaffSuccess.Write();
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
                    cs.Holderexist.Write();
                    return;
                }
                Account account = new Account(bankName, username, password);
                bank.AccountHolders.Add(username, account);
                cs.HolderSuccess.Write();
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
                    cs.Usernotfound.Write();
                    return;
                }
                string newUsername = InputCheck.ReadString("Enter new username: ").ToLower();
                if (bank.AccountHolders.ContainsKey(newUsername))
                {
                    cs.UsernameExist.Write();
                    return;
                }
                string newPassword = InputCheck.ReadString("Enter new password: ");
                Account account = bank.AccountHolders[username];
                bank.AccountHolders.Remove(username);
                account.Username = newUsername;
                account.Password = newPassword;
                bank.AccountHolders.Add(newUsername, account);
                cs.UpdateSuccess.Write();
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
                    cs.DeleteSuccess.Write();
                }
                else
                {
                    cs.Usernotfound.Write();
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
                    cs.CurrencyExist.Write();
                    return;
                }
                bank.CurrencyRates.Add(code, rate);
                cs.CurrencyAdded.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateServiceCharge()
        {
            choices.chargesChoice();
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SameBankIMPS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.SameBankIMPS = SameBankIMPS;
                    break;
                case "2":
                    SameBankRTGS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.SameBankRTGS = SameBankRTGS;
                    break;
                case "3":
                    OtherBankIMPS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.OtherBankIMPS = OtherBankIMPS;
                    break;
                case "4":
                    OtherBankRTGS = InputCheck.ReadDecimal("Enter new charge: ");
                    bank.OtherBankRTGS = OtherBankRTGS;
                    break;
                default:
                    cs.ValidChoice.Write();
                    return;
            }
            cs.UpdateSuccess.Write();
        }

        public void Deposit(string username, string currency, decimal amount)
        {
            try
            {
                currency = currency.ToUpper();
                if (!bank.AccountHolders.TryGetValue(username, out var acc))
                {
                    cs.Usernotfound.Write();
                    return;
                }
                if (!bank.CurrencyRates.TryGetValue(currency, out decimal rate))
                {
                    cs.Currencynotaccept.Write();
                    return;
                }
                decimal inr = amount * rate;
                acc.Balance += inr;
                AddTransaction(username, "Bank", inr, "Deposit");
                cs.Deposited.Write();
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
                    cs.Usernotfound.Write();
                    return;
                }
                if (acc.Balance < amount)
                {
                    cs.InsufficientFunds.Write();
                    return;
                }
                acc.Balance -= amount;
                AddTransaction(username, "Bank", amount, "Withdraw");
                cs.WithdrawSuccess.Write();
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
                    cs.Sendernotfound.Write();
                    return;
                }

                Account receiver = null;
                Bank receiverBank = null;

                foreach (var b in allBanks.Values)
                {
                    if (b.AccountHolders.ContainsKey(r))
                    {
                        receiver = b.AccountHolders[r];
                        receiverBank = b;
                        break;
                    }
                }

                if (receiver == null)
                {
                    cs.Receivernotfound.Write();
                    return;
                }

                choices.chargesTypes();
                string type = Console.ReadLine();

                decimal charge = 0;
                bool sameBank = sender.BankName == receiver.BankName;

                if (sameBank && type == "1")
                {
                    charge = SameBankIMPS;
                }
                else if (sameBank && type == "2")
                {
                    charge = SameBankRTGS;
                }
                else if (!sameBank && type == "1")
                {
                    charge = OtherBankIMPS;
                }
                else if (!sameBank && type == "2")
                { 
                    charge = OtherBankRTGS; 
                }
                else
                {
                    Console.WriteLine("Invalid type");
                    return;
                }

                decimal total = amt + amt * charge / 100;

                if (sender.Balance < total)
                {
                    cs.InsufficientFunds.Write();
                    return;
                }

                sender.Balance -= total;
                receiver.Balance += total;

                AddTransaction(s, r, total, "Transfer");
                cs.TransferSuccess.Write();
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
                string senderBankId = bank.AccountHolders[sender].BankID;
                string senderAccountId = bank.AccountHolders[sender].AccountId;

                Transaction senderTxn = new Transaction(senderBankId, senderAccountId, sender, receiver, amount, type);

                if (bank.AccountHolders.ContainsKey(sender))
                {
                    bank.AccountHolders[sender].Transactions.Add(senderTxn);
                }

                foreach (var b in allBanks.Values)
                {
                    if (b.AccountHolders.ContainsKey(receiver))
                    {
                        string receiverBankId = b.AccountHolders[receiver].BankID;
                        string receiverAccountId = b.AccountHolders[receiver].AccountId;

                        Transaction receiverTxn = new Transaction(receiverBankId, receiverAccountId, sender, receiver, amount, type);

                        b.AccountHolders[receiver].Transactions.Add(receiverTxn);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewUserTransactions(string username)
        {
            try
            {
                if (!bank.AccountHolders.TryGetValue(username, out var account))
                {
                    cs.Usernotfound.Write();
                    return;
                }
                if (!account.Transactions.Any())
                {
                    cs.Transactionsnotfound.Write();
                    return;
                }
                foreach (var t in account.Transactions)
                {
                    Console.WriteLine($"{t.TransactionId} {t.Type} {t.Amount}");
                }
                    
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
                Transaction found = null;
                Account sender = null;
                Account receiver = null;
                Bank senderBank = null;
                Bank receiverBank = null;

                foreach (var bank in allBanks.Values)
                {
                    foreach (var account in bank.AccountHolders.Values)
                    {
                        found = account.Transactions.FirstOrDefault(x => x.TransactionId == txn);
                        if (found != null)
                        {
                            senderBank = bank;
                            sender = bank.AccountHolders[found.Sender];
                            break;
                        }
                    }
                    if (found != null)
                    {
                        break;
                    }
                        
                }

                if (found == null)
                {
                    cs.Transactionsnotfound.Write();
                    return;
                }

                if (found.Type != "Transfer")
                {
                    cs.Transfertransactionrevert.Write();
                    return;
                }

                foreach (var bank in allBanks.Values)
                {
                    if (bank.AccountHolders.ContainsKey(found.Receiver))
                    {
                        receiverBank = bank;
                        receiver = bank.AccountHolders[found.Receiver];
                        break;
                    }
                }

                if (sender == null || receiver == null)
                {
                    cs.Usernotfound.Write();
                    return;
                }

                sender.Balance += found.Amount;
                receiver.Balance -= found.Amount;

                cs.RevertedTransaction.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewAllTransactions()
        {
            try
            {
                bool found = false;

                foreach (var acc in bank.AccountHolders.Values)
                {
                    foreach (var t in acc.Transactions)
                    {
                        Console.WriteLine($"{t.TransactionId} {t.Sender} {t.Receiver} {t.Type} {t.Amount}");
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No transactions found.");
                }
                    
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
                    cs.Usernotfound.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
