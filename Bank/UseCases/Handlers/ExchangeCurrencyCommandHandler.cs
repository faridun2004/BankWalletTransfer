using Bank.Contexts;
using Bank.DTOs;
using Bank.Exceptions;
using Bank.Models;
using Bank.UseCases.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Handlers
{
    public class ExchangeCurrencyCommandHandler : IRequestHandler<ExchangeCurrencyCommand, Guid>
    {
        private readonly BankDbContext _context;
        public ExchangeCurrencyCommandHandler(BankDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(ExchangeCurrencyCommand request, CancellationToken cancellationToken)
        {
            var senderWallet = await _context.Wallets.FirstOrDefaultAsync(w=>w.Id==request.WalletId);
            if (senderWallet == null) throw new Exception("Sender wallet not found");
            var receiverWallet = await _context.Wallets.FirstOrDefaultAsync(w=>w.Id==request.WalletSentId);
            if (receiverWallet == null) throw new Exception("Receiver wallet not found");
            double exchangeRate = 1;
            double convertedAmount = request.Amount ;

            if (request.CurrencyFrom != request.CurrencyTo) 
            {
                var exchangeRateRecord=await _context.ExchangeRates.FirstOrDefaultAsync(er=>
                er.FromCurrency==request.CurrencyFrom && er.ToCurrency==request.CurrencyTo, 
                cancellationToken);
                if (exchangeRateRecord == null)
                    throw new CustomException("Exchange rate not defined for the selected currencie");
                exchangeRate=exchangeRateRecord.Rate;
                convertedAmount=request.Amount*exchangeRate;
            }
            if (request.CurrencyFrom == WalletStatus.USD && senderWallet.UsdBalance < request.Amount)
                throw new Exception("");
            if (request.CurrencyFrom == WalletStatus.TJS && senderWallet.TjsBalance < request.Amount)
                throw new Exception("");
            if (request.CurrencyFrom == request.CurrencyTo)
            {
                if (request.CurrencyFrom == WalletStatus.USD)
                {
                    senderWallet.UsdBalance -= request.Amount;
                    receiverWallet.UsdBalance += request.Amount;
                }
                else
                {
                    senderWallet.TjsBalance -= request.Amount;
                    receiverWallet.TjsBalance += request.Amount;
                }              
            }
            else 
            {
                if (request.CurrencyFrom == WalletStatus.USD)
                {
                    senderWallet.UsdBalance -= request.Amount;
                    receiverWallet.TjsBalance += convertedAmount;
                }
                else
                {
                    senderWallet.TjsBalance -= request.Amount;
                    receiverWallet.UsdBalance += convertedAmount;
                }               
            }
            var currencyExchange = new CurrencyExchange
            {
                Id = Guid.NewGuid(),
                WalletId = senderWallet.Id,
                Amount = request.Amount,
                CurrencyFrom = request.CurrencyFrom,
                CurrencyTo = request.CurrencyTo,
                WalletSentId = receiverWallet.Id,
                ExchangeRate = exchangeRate, 
                ExchangeDate = DateTime.UtcNow,
            };

            _context.CurrencyExchanges.Add(currencyExchange);
            await _context.SaveChangesAsync(cancellationToken);
            
            return currencyExchange.Id;

        }
    }
}
