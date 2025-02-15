using Bank.Models;

namespace Bank.DTOs
{
    public class CurrencyExchangeDto
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public double Amount { get; set; }
        public WalletStatus CurrencyFrom { get; set; }
        public WalletStatus CurrencyTo { get; set; }
        public Guid WalletSentId { get; set; }
        public double ExchangeRate { get; set; }
        public DateTime ExchangeDate { get; set; }
    }
}
