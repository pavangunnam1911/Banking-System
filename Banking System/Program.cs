using System;
using Banking_System.Controller;
using Banks;

class main
{
    public static void Main(string[] args)
    {
        Bank bank = new Bank("MyBank","India");
        BankUsers myBank = new BankUsers(bank);

        myBank.BankChoice();
    }
}
