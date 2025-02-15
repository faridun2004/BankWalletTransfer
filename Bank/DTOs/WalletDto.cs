namespace Bank.DTOs
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public double UsdBalance { get; set; }
        public double TjsBalance { get; set; }
    }
}
