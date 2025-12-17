namespace Banks
{
    public class Transaction
    {
        public string TransactionId { get; init; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }

        public Transaction(string bankId, string accountId, string sender, string receiver, decimal amount, string type)
        {
            TransactionId = "TXN" + bankId + accountId + DateTime.Now.ToString("ddMMyyyyHHmmss");
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
            Type = type;
        }
    }
}
