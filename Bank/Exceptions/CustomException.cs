namespace Bank.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) 
        {
             message = "Custom exception";
        }
    }
}
