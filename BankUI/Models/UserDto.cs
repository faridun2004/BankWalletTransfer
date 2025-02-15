namespace BankUI.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AccountDto Account { get; set; }
    }
}
