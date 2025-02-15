namespace Bank.DTOs
{
    public class TransferDTO
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
