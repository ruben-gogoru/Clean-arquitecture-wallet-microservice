namespace wallet_microservice_dotnet._4.Presentation.ModelsDTOs
{
    public class WalletTransactionDTO
    {
        public long Id { get; set; }
        public long WalletId { get; set; }
        public long Amount { get; set; }
        public string TransactionType { get; set; }
        public string PaymentId { get; set; }
        public DateTime Updated { get; set; }

    }
}
