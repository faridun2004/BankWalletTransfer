using Bank.Contexts;
using Bank.Models;
using Bank.UseCases.Commands;
using MediatR;

namespace Bank.UseCases.Handlers
{
    public class CreateWalletCommandHandler:IRequestHandler<CreateWalletCommand, Guid>
    {
        private readonly BankDbContext _context;
        public CreateWalletCommandHandler(BankDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null) throw new Exception("Account not found");
            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                AccountId = request.AccountId,
                UsdBalance = request.UsdBalance,
                TjsBalance = request.TjsBalance
            };
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync(cancellationToken);
            return wallet.Id;
        }
    }
}
