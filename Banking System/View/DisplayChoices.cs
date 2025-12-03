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

        public void chargesChoice()
        {
            cs.chargeschoice.Write();
            cs.sameIMPS.Write();
            cs.sameRTGS.Write();
            cs.otherIMPS.Write();
            cs.otherRTGS.Write();
        }

        public void chargesTypes()
        {
            cs.UserTypeHeader.Write();
            cs.IMPS.Write();
            cs.RTGS.Write();
        }

    }
}