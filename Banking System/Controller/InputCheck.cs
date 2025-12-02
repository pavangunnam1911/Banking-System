public static class InputCheck
{
    public static decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal d))
            {
                return d;
            }
            Console.WriteLine("Invalid amount.");
        }
    }

    public static string ReadString(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
                
            Console.WriteLine("Input cannot be empty.");
        }
    }
}