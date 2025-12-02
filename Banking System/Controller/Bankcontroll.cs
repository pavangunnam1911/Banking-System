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
            choices.createStaff();
            choices.usernameEnter();
            string staffUsername = Console.ReadLine();
            choices.passwordEnter();
            string staffPassword = Console.ReadLine();
            bankservice.CreateStaff(staffUsername, staffPassword);
            StaffActions(staffUsername);
        }

        public void staffAccountLogin()
        {
            try
            {
                choices.loginStaff();
                choices.usernameEnter();
                string staffUsername = Console.ReadLine();
                choices.passwordEnter();
                string staffPassword = Console.ReadLine();

                if (bankservice.ValidateStaff(staffUsername, staffPassword))
                {
                    StaffActions(staffUsername);
                }
                else
                {
                    choices.invalidCredentials();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.Message);
            }
        }

        public void HolderAccountCreate()
        {
            choices.createHolder();
            string HolderBankname = bank.BankName;
            choices.usernameEnter();
            string HolderUsername = Console.ReadLine();
            choices.passwordEnter();
            string HolderPassword = Console.ReadLine();
            bankservice.CreateAccountHolder(HolderBankname, HolderUsername, HolderPassword);
        }

        public void HolderAccountLogin()
        {
            try
            {
                choices.loginHolder();
                choices.usernameEnter();
                string HolderUsername = Console.ReadLine();
                choices.passwordEnter();
                string HolderPassword = Console.ReadLine();

                if (bankservice.ValidateAccountHolder(HolderUsername, HolderPassword))
                {
                    HolderActions(HolderUsername);
                }
                else
                {
                    choices.invalidCredentials();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.Message);
            }
        }

        public void optionChooseStaff()
        {
            choices.loginChoice();
            string Accountchoice = Console.ReadLine();

            if (int.TryParse(Accountchoice, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        {
                            staffAccountCreate();
                            break;
                        }

                    case 2:
                        {
                            staffAccountLogin();
                            break;
                        }

                    default:
                        {
                            choices.validEnter();
                            break;
                        }
                }
            }
        }


        public void DisplayUserChoice()
        {
            string mainOption;

            do
            {
                choices.WelcomeChoice();
                string roleChoose = Console.ReadLine();

                if (int.TryParse(roleChoose, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                optionChooseStaff();
                                break;
                            }

                        case 2:
                            {
                                HolderAccountLogin();
                                break;
                            }

                        default:
                            {
                                choices.validEnter();
                                break;
                            }
                    }
                }
                else
                {
                    choices.chooseCorrect();
                }

                choices.returnMainmenu();
                mainOption = Console.ReadLine().ToUpper();

            }
            while (mainOption == "Y");
        }

        public void StaffActions(string username)
        {
            string option;

            do
            {
                choices.staffChoice();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            HolderAccountCreate();
                            break;
                        }

                    case "2":
                        {
                            choices.usernameUpdate();
                            bankservice.UpdateAccount(Console.ReadLine());
                            break;
                        }

                    case "3":
                        {
                            choices.usernameDelete();
                            bankservice.DeleteAccount(Console.ReadLine());
                            break;
                        }

                    case "4":
                        {
                            choices.currencyCodeEnter();
                            string currencycode = Console.ReadLine().ToUpper();

                            try
                            {
                                choices.AmountEnter();
                                decimal rate = Convert.ToDecimal(Console.ReadLine());
                                bankservice.AddCurrency(currencycode, rate);
                            }
                            catch
                            {
                                Console.WriteLine("Invalid rate value.");
                            }
                            break;
                        }

                    case "5":
                        {
                            bankservice.ShowAllTransactions();
                            break;
                        }

                    case "6":
                        {
                            choices.TransactionIDEnter();
                            bankservice.RevertTransaction(Console.ReadLine());
                            break;
                        }

                    case "7":
                        {
                            bankservice.UpdateServiceCharge();
                            break;
                        }

                    default:
                        {
                            choices.validEnter();
                            break;
                        }
                }

                choices.Continue();
                option = Console.ReadLine().ToUpper();

            }
            while (option == "Y");
        }

        public void HolderActions(string username)
        {
            string option;

            do
            {
                choices.holderChoice();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            choices.currencyCodeEnter();
                            string currency = Console.ReadLine();

                            try
                            {
                                choices.AmountEnter();
                                decimal amount = Convert.ToDecimal(Console.ReadLine());
                                bankservice.Deposit(username, currency, amount);
                            }
                            catch
                            {
                                Console.WriteLine("Invalid amount");
                            }
                            break;
                        }

                    case "2":
                        {
                            try
                            {
                                choices.AmountEnter();
                                decimal amt = Convert.ToDecimal(Console.ReadLine());
                                bankservice.Withdraw(username, amt);
                            }
                            catch
                            {
                                Console.WriteLine("Invalid amount");
                            }
                            break;
                        }

                    case "3":
                        {
                            choices.receiverUsernameEnter();
                            string rec = Console.ReadLine();

                            try
                            {
                                choices.AmountEnter();
                                decimal transferAmt = Convert.ToDecimal(Console.ReadLine());
                                bankservice.Transfer(username, rec, transferAmt);
                            }
                            catch
                            {
                                Console.WriteLine("Invalid amount");
                            }
                            break;
                        }

                    case "4":
                        {
                            bankservice.ViewUserTransactions(username);
                            break;
                        }

                    case "5":
                        {
                            bankservice.ViewBalance(username);
                            break;
                        }

                    default:
                        {
                            choices.validEnter();
                            break;
                        }
                }

                choices.Continue();
                option = Console.ReadLine().ToUpper();

            }
            while (option == "Y");
        }
    }
}
