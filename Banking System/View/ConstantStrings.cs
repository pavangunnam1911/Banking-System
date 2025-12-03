namespace Banking_System.View
{   

    public class ConstantStrings
    {

        public string BankSystemWelcome = "Bank System";
        public string ChooseCorrect = "Please Choose the correct option";
        public string EnterUsername = "Please Enter your Username";
        public string EnterPassword = "Please Enter your password";
        public string ValidChoice = "Please enter a Valid Choice";
        public string InvalidCredentials = "Invalid Credentials";


        public string StaffSuccess = "Staff Created Successfully!";
        public string Staffexist = "Staff already exists";
        public string HolderSuccess = "Holder Created Successfully!";
        public string Holderexist = "Staff already exists";
        public string CreateStaff = "Create staff account";
        public string LoginStaff = "Login to your staff Account";
        public string CreateHolder = "Create Holder Account";
        public string LoginHolder = "Login to your Holder Account";


        public string UsernameUpdate = "Enter account username to update";
        public string UsernameDelete = "Enter account username to delete";
        public string Usernotfound = "User not found";
        public string Sendernotfound = "Sender not found";
        public string Receivernotfound = "Receiver not found";
        public string UsernameExist = "Username already exists.";
        public string UpdateSuccess = "Updated successfully";
        public string DeleteSuccess = "Successfully deleted";
        public string CurrencyExist = "Currency Already Exists";
        public string CurrencyAdded = "Currency Added";
        public string Currencynotaccept = "Currency not accepted";
        public string WithdrawSuccess = "Withdraw Successful";
        public string TransferSuccess = "Transfer Successful";
        public string InsufficientFunds = "Insufficient Funds";
        public string Transactionsnotfound = "No transactions Availabl";
        public string Transfertransactionrevert = "Only transfer transactions can be reverted.";
        public string RevertedTransaction = "Transaction Reverted";




        public string EnterBankName = "Please Enter your Bank Name";
        public string EnterBankId = "Please Enter Bank Id:";
        public string EnterBankCountry = "Please Enter the Bank's Country Name";
        public string EnterBankAddress = "Please Enter the Bank's Address";
        public string EnterRTGSother = "Please Enter RTGS rates for tranfering amount to different bank";
        public string EnterIMPSother = "Please Enter IMPS rates for tranfering amount to different bank";
        public string BanknotFound = "Bank not found.";
        public string BanksAvailable = "Available Banks:";
        public string NoBankData = "No Data Found";
        public string Deposited = "Deposited";


        public string WelcomeChoices1 = "1. Create a new Bank";
        public string WelcomeChoices2 = "2. Choose an existing Bank";
        public string WelcomeChoices3 = "3. View all Banks";

        public string UserTypeHeader = "Enter the Choice (1-2)";
        public string UserStaff = "1. Staff";
        public string UserHolder = "2. Account Holder";

        public string StaffMenuHeader = "Welcome Staff(Enter Choice 1-6)";
        public string StaffChoice1 = "1. Create Account Holder";
        public string StaffChoice2 = "2. Update Account";
        public string StaffChoice3 = "3. Delete Account";
        public string StaffChoice4 = "4. Add Currency";
        public string StaffChoice5 = "5. View Transactions";
        public string StaffChoice6 = "6. Revert Transactions";

        public string HolderMenuHeader = "Welcome Holder(Enter Choice 1-5)";
        public string HolderChoice1 = "1. Deposit";
        public string HolderChoice2 = "2. Withdraw";
        public string HolderChoice3 = "3. Transfer";
        public string HolderChoice4 = "4. View Transactions";
        public string HolderChoice5 = "5. View Balance";

        public string LoginChoiceHeader = "Enter the Choice (1-2)";
        public string LoginChoice1 = "1. Create Account";
        public string LoginChoice2 = "2. Login to Existing Account";


        public string EnterCurrencyCode = "Enter currency code";
        public string EnterAmount = "Enter amount";
        public string EnterExchangeRate = "Enter the Exchange rate";
        public string EnterTransactionID = "Enter Transaction ID";
        public string EnterReceiverUsername = "Enter receiver username";


        public string ReturnUserMenu = "Do you want to return to the User main menu? (Y/N)";
        public string ReturnBankMenu = "Do you want to return to the Bank main menu? (Y/N)";
        public string ContinueMainMenu = "Press Y to return to main menu";

        public string chargeschoice= "Enter the Choice (1-4)";
        public string sameIMPS = "1. Same Bank IMPS";
        public string sameRTGS ="2. Same Bank RTGS";
        public string otherIMPS = "3. Other Bank IMPS";
        public string otherRTGS = "4. Other Bank RTGS";

        public string IMPS = "1.IMPS";
        public string RTGS = "2.RTGS";
    }
}