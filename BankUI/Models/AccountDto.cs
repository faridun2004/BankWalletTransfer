namespace BankUI.Models
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto UserDto { get; set; }
        public WalletDto WalletDto { get; set; }
        public string PhoneNumber { get; set; }
    }
}
