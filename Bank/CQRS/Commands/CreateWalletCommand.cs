using Bank.DTOs;
using MediatR;

namespace Bank.CQRS.Commands
{
    public class CreateWalletCommand : IRequest<WalletDTO>
    {
        public Guid UserId { get; set; }
        public decimal InitialBalance { get; set; }
    }

}
