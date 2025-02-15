using Bank.Contexts;
using Bank.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Queries.CurrencyExchanges
{
    public class GetCurrencyExchangeQueryHandler: IRequestHandler<GetCurrencyExchangeQuery, CurrencyExchangeDto>
    {
        private readonly BankDbContext _context;
        public GetCurrencyExchangeQueryHandler(BankDbContext context)
        {
            _context = context;
        }
        public async Task<CurrencyExchangeDto> Handle(GetCurrencyExchangeQuery request, CancellationToken cancellationToken)
        {
            var exchange = await _context.CurrencyExchanges.FirstOrDefaultAsync(e=>e.Id==request.ExchangeId, cancellationToken);
            if (exchange == null) throw new Exception("Currency exchange not found");
            return new CurrencyExchangeDto
            {
                Id = exchange.Id,
                WalletId = exchange.WalletId,
                WalletSentId = exchange.WalletSentId,
                Amount = exchange.Amount,
                CurrencyFrom = exchange.CurrencyFrom,
                CurrencyTo = exchange.CurrencyTo,
                ExchangeDate = exchange.ExchangeDate,
                ExchangeRate = exchange.ExchangeRate
            };
        }
    }
}
