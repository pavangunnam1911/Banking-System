using Banks;
using Banking_System.Controller;
using Banking_System.View;

public class BankSystemController
{
    private Dictionary<string, Bank> AllBanks = new Dictionary<string, Bank>();

    DisplayChoices choices = new DisplayChoices();

    ConstantStrings cs = new ConstantStrings();

    public void Start()
    {
        string option;
        do
        {
            try
            {
                choices.WelcomeBank();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateNewBank();
                        break;
                    case "2":
                        ChooseBank();
                        break;
                    case "3":
                        ShowBanks();
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

    private void CreateNewBank()
    {
        try
        {
            cs.EnterBankName.Write();
            string name = InputCheck.ValidateBankName();

            cs.EnterBankCountry.Write();
            string country = InputCheck.ValidateCountry();

            cs.EnterBankAddress.Write();
            string address = InputCheck.ReadString();

            cs.EnterRTGSother.Write();
            string RTGSother = InputCheck.ReadRTGSandIMPS();

            cs.EnterIMPSother.Write();
            string IMPSother = InputCheck.ReadRTGSandIMPS();


            Bank newBank = new Bank(name, country, address, RTGSother, IMPSother);
            AllBanks.Add(newBank.BankId, newBank);

            Console.WriteLine($"Bank Created: {newBank.BankName} ({newBank.BankId})");

            BankUsers users = new BankUsers(newBank, AllBanks);
            users.DisplayUserChoice();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ChooseBank()
    {
        try
        {
            cs.EnterBankId.Write();
            string id = InputCheck.ReadString("");
            if (AllBanks.ContainsKey(id))
            {
                Console.WriteLine($"Selected: {AllBanks[id].BankName}");
                BankUsers users = new BankUsers(AllBanks[id], AllBanks);
                users.DisplayUserChoice();
            }
            else
            {
                cs.BanknotFound.Write();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ShowBanks()
    {
        try
        {
            if (AllBanks.Values.Any())
            {
                cs.BanksAvailable.Write();
                foreach (var b in AllBanks.Values)
                    Console.WriteLine($"{b.BankId} - {b.BankName} ({b.BankCountry})");
            }
            else
            {
                cs.NoBankData.Write();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
