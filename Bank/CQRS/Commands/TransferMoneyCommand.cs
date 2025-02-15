using MediatR;

namespace Bank.CQRS.Commands
{
    public class TransferMoneyCommand : IRequest<bool>
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public decimal Amount { get; set; }
    }

}
