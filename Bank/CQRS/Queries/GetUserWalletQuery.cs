using Bank.DTOs;
using MediatR;

namespace Bank.CQRS.Queries
{
    public class GetUserWalletQuery : IRequest<WalletDTO>
    {
        public Guid UserId { get; set; }
    }

}
