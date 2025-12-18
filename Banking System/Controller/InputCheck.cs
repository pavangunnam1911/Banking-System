using System.Text.RegularExpressions;

public class InputCheck
{
    public static decimal ReadDecimal(string message = "")
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

    public static string ReadString(string message = "")
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

    public static string ValidateBankName(string message = "" )
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (input.Length >= 3 && Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                return input;
            }
            Console.WriteLine("Input should only contain alphabates and minimum length of 3");
        }
    }

    public static string ValidateCountry(string message = "")
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)&& input.Length >= 3 && Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                return input;
            }
            Console.WriteLine("Input should only contain alphabates and should not be empty and a minimum length of 4");
        }
    }

    public static string ReadRTGSandIMPS(string message = "")
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Please Enter some Value");
            }
            if (int.TryParse(input, out int number))
            {
                if (number >= 0 && number <= 100)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Input must be a number between 0 and 100.");
                }
            }
            else
            {
                Console.WriteLine("Please Enter whole numbers only");
            }
        }
    }

    public static string ValidateUsername(string message = "")
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)&& input.Length >= 4)
            {
                return input;
            }

            Console.WriteLine("Username criteria: Min 4 chars,should not be empty");
        }
    }

    public static string ValidatePassword(string message = "")
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)&& input.Length >= 8 && input.Any(char.IsUpper) && input.Any(char.IsLower) && input.Any(char.IsDigit))
            {
                return input;
            }
            Console.WriteLine("Password criteria: Min 8 chars, 1 upper, 1 lower, 1 digit,should not be empty");
        }
    }


}
