using Banking_System.Models;
using Banks;

namespace Banking_System.Controller
{
    public class BankUsers
    {
        DisplayChoices choices = new DisplayChoices();
        BankService bankservice;
        Bank bank;

        public BankUsers(Bank bank)
        {
            this.bank = bank;
            bankservice = new BankService(bank);
        }

        public void staffAccountCreate()
        {
            try
            {
                choices.createStaff();
                choices.usernameEnter();
                string staffUsername = InputCheck.ReadString("");
                choices.passwordEnter();
                string staffPassword = InputCheck.ReadString("");
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
                choices.loginStaff();
                choices.usernameEnter();
                string staffUsername = InputCheck.ReadString("");
                choices.passwordEnter();
                string staffPassword = InputCheck.ReadString("");

                if (bankservice.ValidateStaff(staffUsername, staffPassword))
                    StaffActions(staffUsername);
                else
                    choices.invalidCredentials();
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
                choices.createHolder();
                string HolderBankname = bank.BankName;
                choices.usernameEnter();
                string HolderUsername = InputCheck.ReadString("");
                choices.passwordEnter();
                string HolderPassword = InputCheck.ReadString("");
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
                choices.loginHolder();
                choices.usernameEnter();
                string HolderUsername = InputCheck.ReadString("");
                choices.passwordEnter();
                string HolderPassword = InputCheck.ReadString("");

                if (bankservice.ValidateAccountHolder(HolderUsername, HolderPassword))
                    HolderActions(HolderUsername);
                else
                    choices.invalidCredentials();
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
                            choices.validEnter();
                            break;
                    }
                }
                else
                {
                    choices.validEnter();
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
                                choices.validEnter();
                                break;
                        }
                    }
                    else
                    {
                        choices.chooseCorrect();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                choices.returnMainmenu();
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
                            choices.usernameUpdate();
                            bankservice.UpdateAccount(Console.ReadLine());
                            break;

                        case "3":
                            choices.usernameDelete();
                            bankservice.DeleteAccount(Console.ReadLine());
                            break;

                        case "4":
                            choices.currencyCodeEnter();
                            string currencycode = Console.ReadLine().ToUpper();
                            choices.AmountEnter();
                            decimal rate = InputCheck.ReadDecimal("");
                            bankservice.AddCurrency(currencycode, rate);
                            break;

                        case "5":
                            Console.WriteLine("Enter user details");
                            string name = Console.ReadLine();
                            bankservice.ViewUserTransactions(name);
                            break;

                        case "6":
                            choices.TransactionIDEnter();
                            bankservice.RevertTransaction(Console.ReadLine());
                            break;

                        case "7":
                            bankservice.UpdateServiceCharge();
                            break;

                        default:
                            choices.validEnter();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                choices.Continue();
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
                            choices.currencyCodeEnter();
                            string currency = Console.ReadLine();
                            choices.AmountEnter();
                            decimal amount = InputCheck.ReadDecimal("");
                            bankservice.Deposit(username, currency, amount);
                            break;

                        case "2":
                            choices.AmountEnter();
                            decimal amt = InputCheck.ReadDecimal("");
                            bankservice.Withdraw(username, amt);
                            break;

                        case "3":
                            choices.receiverUsernameEnter();
                            string rec = Console.ReadLine();
                            choices.AmountEnter();
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
                            choices.validEnter();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                choices.Continue();
                option = Console.ReadLine().ToUpper();

            } while (option == "Y");
        }
    }
}
