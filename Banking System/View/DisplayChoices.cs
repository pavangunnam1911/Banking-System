using System;

namespace Banks
{
    public class DisplayChoices
    {
        public void WelcomeBank()
        {
            Console.WriteLine("Welcome to Bank");
            Console.WriteLine("1.Create New Bank");
            Console.WriteLine("2.Go to existing Bank");
        }

        public void WelcomeChoice()
        {
            Console.WriteLine("Enter the Choice (1-2)");
            Console.WriteLine("1.Staff");
            Console.WriteLine("2.Account Holder");
        }

        public void staffChoice()
        {
            Console.WriteLine("Welcome Staff");
            Console.WriteLine("1.Create Account Holder");
            Console.WriteLine("2.Update Account");
            Console.WriteLine("3.Delete Account");
            Console.WriteLine("4.Add Currency");
            Console.WriteLine("5.View Transactions");
            Console.WriteLine("6.Revert Transactions");
        }

        public void holderChoice()
        {
            Console.WriteLine("Welcome Holder");
            Console.WriteLine("1.Deposit");
            Console.WriteLine("2.Withdraw");
            Console.WriteLine("3.Transfer");
            Console.WriteLine("4.View Transactions");
            Console.WriteLine("5.View Balance");
        }

        public void loginChoice()
        {
            Console.WriteLine("Enter the Choice (1-2)");
            Console.WriteLine("1.Create Account");
            Console.WriteLine("2.Login to Existing Account");
        }

        public void chooseCorrect() => Console.WriteLine("Please Choose the correct option");
        public void usernameEnter() => Console.WriteLine("Please Enter your UserName");
        public void passwordEnter() => Console.WriteLine("Please Enter your password");
        public void validEnter() => Console.WriteLine("Please enter a Valid Choice");
        public void invalidCredentials() => Console.WriteLine("Invalid Credentials");
        public void staffSuccess() => Console.WriteLine("Staff Created Successfully!");
        public void holderSuccess() => Console.WriteLine("Holder Created Successfully!");
        public void createStaff() => Console.WriteLine("Create staff account");
        public void loginStaff() => Console.WriteLine("Login to your staff Account");
        public void createHolder() => Console.WriteLine("Create Holder Account");
        public void loginHolder() => Console.WriteLine("Login to your Holder Account");
        public void usernameUpdate() => Console.WriteLine("Enter account username to update");
        public void usernameDelete() => Console.WriteLine("Enter account username to delete");
        public void BankEntry() => Console.WriteLine("Please Enter your BankName");
        public void returnMainmenu() => Console.WriteLine("\nDo you want to return to the main menu? (Y/N)");
        public void currencyCodeEnter() => Console.WriteLine("Enter currency code");
        public void AmountEnter() => Console.WriteLine("Enter amount");
        public void TransactionIDEnter() => Console.WriteLine("Enter Transaction ID");
        public void Continue() => Console.WriteLine("Press Y to return to main menu");
        public void receiverUsernameEnter() => Console.WriteLine("Enter receiver username");

        public void startBank() => Console.WriteLine("Please Enter your Bank Name");
        public void BankCountry() => Console.WriteLine("Please Enter the Bank's Country Name");
    }
}
