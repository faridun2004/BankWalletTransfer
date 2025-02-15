namespace Bank.Models
{
    public class ExchangeRate
    {
        public Guid Id { get; set; }
        public WalletStatus FromCurrency { get; set; }
        public WalletStatus ToCurrency { get; set; }
        public double Rate { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}
