using Bank.CQRS.Queries;
using Bank.DTOs;
using Bank.Repositories;
using MediatR;

namespace Bank.CQRS.Handlers
{
    public class GetUserWalletHandler : IRequestHandler<GetUserWalletQuery, WalletDTO>
    {
        private readonly IWalletRepository _walletRepository;

        public GetUserWalletHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletDTO> Handle(GetUserWalletQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(request.UserId);

            if (wallet == null)
                return null;

            return new WalletDTO
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                UserId = wallet.UserId
            };
        }
    }

}
