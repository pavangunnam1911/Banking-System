using Banking_System.Models;
using Banking_System.View;
using Banks;

namespace Banking_System.Controller
{
    public class BankUsers
    {
        DisplayChoices choices = new DisplayChoices();
        ConstantStrings cs = new ConstantStrings();
        BankService bankservice;
        Bank bank;
        Dictionary<string, Bank> allBanks;

        public BankUsers(Bank bank, Dictionary<string, Bank> allBanks)
        {
            this.bank = bank;
            this.allBanks = allBanks;
            bankservice = new BankService(bank, allBanks);
        }

        public void staffAccountCreate()
        {
            try
            {
                cs.CreateStaff.Write();
                cs.EnterUsername.Write();
                string staffUsername = InputCheck.ReadString("");
                cs.EnterPassword.Write();
                string staffPassword = InputCheck.ValidatePassword("");
                bankservice.CreateStaff(staffUsername, staffPassword);
                StaffActions(staffUsername);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void staffAccountLogin()
        {
            try
            {
                cs.LoginStaff.Write();
                cs.EnterUsername.Write();
                string staffUsername = InputCheck.ReadString("");
                cs.EnterPassword.Write();
                string staffPassword = InputCheck.ReadString("");

                if (bankservice.ValidateStaff(staffUsername, staffPassword)) 
                {
                    StaffActions(staffUsername);
                }
                    
                else
                {
                    cs.InvalidCredentials.Write();
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.Message);
            }
        }

        public void HolderAccountCreate()
        {
            try
            {
                cs.CreateHolder.Write();
                string HolderBankname = bank.BankName;
                cs.EnterUsername.Write();
                string HolderUsername = InputCheck.ReadString("");
                cs.EnterPassword.Write();
                string HolderPassword = InputCheck.ValidatePassword("");
                bankservice.CreateAccountHolder(HolderBankname, HolderUsername, HolderPassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void HolderAccountLogin()
        {
            try
            {
                cs.LoginHolder.Write();
                cs.EnterUsername.Write();
                string HolderUsername = InputCheck.ReadString("");
                cs.EnterPassword.Write();
                string HolderPassword = InputCheck.ReadString("");

                if (bankservice.ValidateAccountHolder(HolderUsername, HolderPassword))
                {
                    HolderActions(HolderUsername);
                }
                    
                else
                {
                    cs.InvalidCredentials.Write();
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.Message);
            }
        }

        public void optionChooseStaff()
        {
            try
            {
                choices.loginChoice();
                string Accountchoice = Console.ReadLine();
                if (int.TryParse(Accountchoice, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            staffAccountCreate();
                            break;
                        case 2:
                            staffAccountLogin();
                            break;
                        default:
                            cs.ValidChoice.Write();
                            break;
                    }
                }
                else
                {
                    cs.ValidChoice.Write();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayUserChoice()
        {
            string mainOption;
            do
            {
                try
                {
                    choices.WelcomeChoice();
                    string roleChoose = Console.ReadLine();
                    if (int.TryParse(roleChoose, out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                optionChooseStaff();
                                break;
                            case 2:
                                HolderAccountLogin();
                                break;
                            default:
                                cs.ValidChoice.Write();
                                break;
                        }
                    }
                    else
                    {
                        cs.ChooseCorrect.Write();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                cs.ReturnUserMenu.Write();
                mainOption = Console.ReadLine().ToUpper();
            } while (mainOption == "Y");
        }

        public void StaffActions(string username)
        {
            string option;
            do
            {
                try
                {
                    choices.staffChoice();
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            HolderAccountCreate();
                            break;
                        case "2":
                            cs.UsernameUpdate.Write();
                            bankservice.UpdateAccount(Console.ReadLine());
                            break;
                        case "3":
                            cs.UsernameDelete.Write();
                            bankservice.DeleteAccount(Console.ReadLine());
                            break;
                        case "4":
                            cs.EnterCurrencyCode.Write();
                            string currencycode = Console.ReadLine().ToUpper();
                            cs.EnterAmount.Write();
                            decimal rate = InputCheck.ReadDecimal("");
                            bankservice.AddCurrency(currencycode, rate);
                            break;
                        case "5":
                            Console.WriteLine("Enter user details");
                            string name = Console.ReadLine();
                            bankservice.ViewUserTransactions(name);
                            break;
                        case "6":
                            cs.EnterTransactionID.Write();
                            bankservice.RevertTransaction(Console.ReadLine());
                            break;
                        case "7":
                            bankservice.UpdateServiceCharge();
                            break;
                        default:
                            cs.ValidChoice.Write();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                cs.ContinueMainMenu.Write();
                option = Console.ReadLine().ToUpper();
            } while (option == "Y");
        }

        public void HolderActions(string username)
        {
            string option;
            do
            {
                try
                {
                    choices.holderChoice();
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            cs.EnterCurrencyCode.Write();
                            string currency = Console.ReadLine();
                            cs.EnterAmount.Write();
                            decimal amount = InputCheck.ReadDecimal("");
                            bankservice.Deposit(username, currency, amount);
                            break;
                        case "2":
                            cs.EnterAmount.Write();
                            decimal amt = InputCheck.ReadDecimal("");
                            bankservice.Withdraw(username, amt);
                            break;
                        case "3":
                            cs.EnterReceiverUsername.Write();
                            string rec = Console.ReadLine();
                            cs.EnterAmount.Write();
                            decimal transferAmt = InputCheck.ReadDecimal("");
                            bankservice.Transfer(username, rec, transferAmt);
                            break;
                        case "4":
                            bankservice.ViewUserTransactions(username);
                            break;
                        case "5":
                            bankservice.ViewBalance(username);
                            break;
                        default:
                            cs.ValidChoice.Write();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                cs.ContinueMainMenu.Write();
                option = Console.ReadLine().ToUpper();
            } while (option == "Y");
        }
    }
}
