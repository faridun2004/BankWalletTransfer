using Bank.Contexts;
using Bank.Exceptions;
using Bank.Models;
using Bank.UseCases.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Handlers
{
    public class TransferByPhoneCommandHandler:IRequestHandler<TransferByPhoneCommand, Guid>
    {
        private readonly BankDbContext _context;
        public TransferByPhoneCommandHandler(BankDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(TransferByPhoneCommand command, CancellationToken cancellationToken)
        {
            var senderWallet = await _context.Wallets.FindAsync(command.SenderWalletId);
            if (senderWallet == null) throw new CustomException("Недостаточно средств в USD кошельке");

            var receiverWallet = await _context.Wallets
                .FirstOrDefaultAsync(w => w.Account.PhoneNumber == command.PhoneNumber, cancellationToken);
            if (receiverWallet == null) throw new CustomException("Недостаточно средств в USD кошельке");

            if (command.Currency == WalletStatus.USD && senderWallet.UsdBalance < command.Amount)
                throw new CustomException("");
            if (command.Currency == WalletStatus.TJS && senderWallet.TjsBalance < command.Amount)
                throw new CustomException("");

            if (command.Currency == WalletStatus.USD)
            {
                senderWallet.UsdBalance -= command.Amount;
                receiverWallet.UsdBalance += command.Amount;
            }
            else
            {

                senderWallet.TjsBalance -= command.Amount;
                receiverWallet.TjsBalance += command.Amount;
            }

            var transfer = new CurrencyExchange
            {
                Id = Guid.NewGuid(),
                WalletId = senderWallet.Id,
                WalletSentId = receiverWallet.Id,
                Amount = command.Amount,
                CurrencyFrom = command.Currency,
                CurrencyTo = command.Currency,
                ExchangeRate = 1.0,
                ExchangeDate = DateTime.UtcNow
            };
            _context.CurrencyExchanges.Add(transfer);
            await _context.SaveChangesAsync(cancellationToken);
            return transfer.Id;
        }   
            
    }
}
