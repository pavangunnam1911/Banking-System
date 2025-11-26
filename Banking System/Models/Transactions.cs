namespace Bank
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }
}