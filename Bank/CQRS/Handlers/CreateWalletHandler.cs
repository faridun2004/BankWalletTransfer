using Bank.CQRS.Commands;
using Bank.DTOs;
using Bank.Models;
using Bank.Repositories;
using MediatR;

namespace Bank.CQRS.Handlers
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, WalletDTO>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public CreateWalletHandler(IWalletRepository walletRepository, IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
        }

        public async Task<WalletDTO> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return null;

            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Balance = request.InitialBalance
            };

            await _walletRepository.UpdateAsync(wallet);

            return new WalletDTO
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                UserId = wallet.UserId
            };
        }
    }

}
