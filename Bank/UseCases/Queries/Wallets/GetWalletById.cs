using Bank.Contexts;
using Bank.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Queries.Wallets
{
    public class GetWalletById: IRequest<WalletDto>
    {
        public Guid WalletId { get; set; }
    }
    public class GetWalletByIdHandler : IRequestHandler<GetWalletById, WalletDto>
    {
        private readonly BankDbContext _context;
        public GetWalletByIdHandler(BankDbContext context)
        {
            _context = context;
        }
        public async Task<WalletDto> Handle(GetWalletById request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(w => w.Id == request.WalletId, cancellationToken);
            if(wallet == null)
                throw new Exception("Wallet not found");
            return new WalletDto
            {
                Id = wallet.Id,
                AccountId = wallet.AccountId,
                UsdBalance = wallet.UsdBalance,
                TjsBalance = wallet.TjsBalance
            };
        }
    }
}
