using Banking_System.View;

namespace Banks
{
    public class DisplayChoices
    {
        ConstantStrings cs = new ConstantStrings();

        public void WelcomeBank()
        {
            cs.BankSystemWelcome.Write();
            cs.WelcomeChoices1.Write();
            cs.WelcomeChoices2.Write();
            cs.WelcomeChoices3.Write();
        }

        public void WelcomeChoice()
        {
            cs.UserTypeHeader.Write();
            cs.UserStaff.Write();
            cs.UserHolder.Write();
        }

        public void staffChoice()
        {
            cs.StaffMenuHeader.Write();
            cs.StaffChoice1.Write();
            cs.StaffChoice2.Write();
            cs.StaffChoice3.Write();
            cs.StaffChoice4.Write();
            cs.StaffChoice5.Write();
            cs.StaffChoice6.Write();
        }

        public void holderChoice()
        {
            cs.HolderMenuHeader.Write();
            cs.HolderChoice1.Write();
            cs.HolderChoice2.Write();
            cs.HolderChoice3.Write();
            cs.HolderChoice4.Write();
            cs.HolderChoice5.Write();
        }

        public void loginChoice()
        {
            cs.LoginChoiceHeader.Write();
            cs.LoginChoice1.Write();
            cs.LoginChoice2.Write();
        }

        public void chooseCorrect() => cs.ChooseCorrect.Write();
        public void usernameEnter() => cs.EnterUsername.Write();
        public void passwordEnter() => cs.EnterPassword.Write();
        public void validEnter() => cs.ValidChoice.Write();
        public void invalidCredentials() => cs.InvalidCredentials.Write();
        public void staffSuccess() => cs.StaffSuccess.Write();
        public void holderSuccess() => cs.HolderSuccess.Write();
        public void createStaff() => cs.CreateStaff.Write();
        public void loginStaff() => cs.LoginStaff.Write();
        public void createHolder() => cs.CreateHolder.Write();
        public void loginHolder() => cs.LoginHolder.Write();
        public void usernameUpdate() => cs.UsernameUpdate.Write();
        public void usernameDelete() => cs.UsernameDelete.Write();
        public void BankEntry() => cs.EnterBankName.Write();
        public void returnMainmenu() => cs.ReturnUserMenu.Write();
        public void returnBankMainmenu() => cs.ReturnBankMenu.Write();
        public void currencyCodeEnter() => cs.EnterCurrencyCode.Write();
        public void AmountEnter() => cs.EnterAmount.Write();
        public void ExchangeRateEnter() => cs.EnterExchangeRate.Write();
        public void TransactionIDEnter() => cs.EnterTransactionID.Write();
        public void Continue() => cs.ContinueMainMenu.Write();
        public void receiverUsernameEnter() => cs.EnterReceiverUsername.Write();
        public void startBank() => cs.EnterBankName.Write();
        public void BankCountry() => cs.EnterBankCountry.Write();
        public void addressAdd() => cs.EnterBankAddress.Write();
    }
}