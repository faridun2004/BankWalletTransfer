namespace Bank.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Wallet Wallet { get; set; }
        public string PhoneNumber { get; set; }
    }

}
