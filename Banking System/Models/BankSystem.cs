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

            Console.WriteLine("Return to Menu? (Y/N)");
            option = Console.ReadLine().ToUpper();

        } while (option == "Y");
    }

    private void CreateNewBank()
    {
        choices.BankEntry();
        string name = Console.ReadLine();
        choices.BankCountry();
        string country = Console.ReadLine();
        choices.addressAdd();
        string address = Console.ReadLine();
        choices.RTGSotherAdd();
        string RTGSother = Console.ReadLine();
        choices.IMPSSotherAdd();
        string IMPSother = Console.ReadLine();

        Bank newBank = new Bank(name, country, address,RTGSother,IMPSother);
        AllBanks.Add(newBank.BankId, newBank);

        Console.WriteLine($"Bank Created: {newBank.BankName} ({newBank.BankId})");

        BankUsers users = new BankUsers(newBank);
        users.DisplayUserChoice();
    }

    private void ChooseBank()
    {
        Console.WriteLine("Enter Bank ID:");
        string id = Console.ReadLine();

        if (AllBanks.ContainsKey(id))
        {
            Console.WriteLine($"Selected Bank: {AllBanks[id].BankName}");
            BankUsers users = new BankUsers(AllBanks[id]);
            users.DisplayUserChoice();
        }
        else
        {
            Console.WriteLine("Bank does not exist.");
        }
    }

    private void ShowBanks()
    {   if (AllBanks.Values.Any())
        {
            Console.WriteLine("Available Banks");

            foreach (var b in AllBanks.Values)
            {
                Console.WriteLine($"{b.BankId} - {b.BankName} ({b.BankCountry})");
            }
        }

        else
        {
            Console.WriteLine("No Data Found");
        }

        
    }
}
