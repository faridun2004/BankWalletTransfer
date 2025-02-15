using Bank.CQRS.Commands;
using Bank.Repositories;
using MediatR;

namespace Bank.CQRS.Handlers
{
    public class TransferMoneyHandler : IRequestHandler<TransferMoneyCommand, bool>
    {
        private readonly IWalletRepository _walletRepository;

        public TransferMoneyHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<bool> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            var fromWallet = await _walletRepository.GetByUserIdAsync(request.FromUserId);
            var toWallet = await _walletRepository.GetByUserIdAsync(request.ToUserId);

            if (fromWallet == null || toWallet == null || fromWallet.Balance < request.Amount)
                return false;

            fromWallet.Balance -= request.Amount;
            toWallet.Balance += request.Amount;

            await _walletRepository.UpdateAsync(fromWallet);
            await _walletRepository.UpdateAsync(toWallet);

            return true;
        }
    }

}
