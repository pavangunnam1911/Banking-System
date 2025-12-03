using Banks;
using Banking_System.Controller;

public class BankSystem
{
    private Dictionary<string, Bank> AllBanks = new Dictionary<string, Bank>();
    DisplayChoices choices = new DisplayChoices();

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
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Return to Menu? (Y/N)");
            option = Console.ReadLine().ToUpper();
        } while (option == "Y");
    }

    private void CreateNewBank()
    {
        try
        {
            choices.BankEntry();
            string name = InputCheck.ReadString();

            choices.BankCountry();
            string country = InputCheck.ReadString();

            choices.addressAdd();
            string address = InputCheck.ReadString();

            choices.RTGSotherAdd();
            string RTGSother = InputCheck.ReadString();

            choices.IMPSSotherAdd();
            string IMPSother = InputCheck.ReadString();


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
            Console.WriteLine("Enter Bank ID:");
            string id = InputCheck.ReadString("");
            if (AllBanks.ContainsKey(id))
            {
                Console.WriteLine($"Selected: {AllBanks[id].BankName}");
                BankUsers users = new BankUsers(AllBanks[id], AllBanks);
                users.DisplayUserChoice();
            }
            else
            {
                Console.WriteLine("Bank not found.");
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
                Console.WriteLine("Available Banks:");
                foreach (var b in AllBanks.Values)
                    Console.WriteLine($"{b.BankId} - {b.BankName} ({b.BankCountry})");
            }
            else
            {
                Console.WriteLine("No Data Found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
