namespace Bank.Exceptions
{
    public class AccountNotFoundException: Exception
    {
        public AccountNotFoundException(Guid accountId)
            : base($"Account with ID: {accountId} not found") { }

        public AccountNotFoundException(string message)
            : base(message) { }

        public AccountNotFoundException(string message, Exception exception)
            : base(message, exception) { }
    }
}
