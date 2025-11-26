namespace Bank
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public decimal Balance  { get; set; } = 0;
        public string AccountId { get; set; }
        public string BankName { get; set; }

        public List<Transaction> Transactions = new List<Transaction>();
    }
}