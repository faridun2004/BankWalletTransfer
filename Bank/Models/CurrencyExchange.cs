namespace Bank.Models
{
    public class CurrencyExchange
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public Wallet? Wallet { get; set; }
        public double Amount { get; set; }
        public double ExchangeRate { get; set; }
        public WalletStatus CurrencyFrom { get; set; } 
        public WalletStatus CurrencyTo { get; set; } 
        public Guid WalletSentId { get; set; }
        public DateTime ExchangeDate { get; set; } = DateTime.UtcNow;
    }
}
