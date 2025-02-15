namespace Bank.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public double UsdBalance { get; set; }
        public double TjsBalance { get; set; }
        
    }

}
