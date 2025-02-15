namespace BankUI.Models
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public AccountDto Account { get; set; }
        public double UsdBalance { get; set; }
        public double TjsBalance { get; set; }
    }
}
