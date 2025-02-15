using Bank.Models;

namespace Bank.DTOs
{
    public class ExchangeRateDto
    {
        public Guid Id { get; set; }
        public WalletStatus FromCurrency { get; set; }
        public WalletStatus ToCurrency { get; set; }
        public double Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
