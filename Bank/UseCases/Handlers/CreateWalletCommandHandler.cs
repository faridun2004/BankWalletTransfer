using Bank.Contexts;
using Bank.Exceptions;
using Bank.Models;
using Bank.UseCases.Commands;
using MediatR;

namespace Bank.UseCases.Handlers
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Guid>
    {
        private readonly BankDbContext _context;
        private readonly ILogger<CreateWalletCommandHandler> _logger;
        public CreateWalletCommandHandler(BankDbContext context, ILogger<CreateWalletCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(request.AccountId);
                if (account == null)
                    throw new AccountNotFoundException(request.AccountId);

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
            catch (AccountNotFoundException ex)
            {
                _logger.LogWarning(ex, "Account not found with ID: {AccountId}", request.AccountId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating wallet for account {AccountId}", request.AccountId);
                throw;
            }
        }
    }
}
