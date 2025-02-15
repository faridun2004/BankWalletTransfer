using Bank.Contexts;
using Bank.DTOs;
using Bank.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Queries.ExchangeRates
{
    public class GetExchangeRateQueryHandler : IRequestHandler<GetExchangeRateQuery, ExchangeRateDto>
    {
        private readonly BankDbContext _context;

        public GetExchangeRateQueryHandler(BankDbContext context)
        {
            _context = context;
        }

        public async Task<ExchangeRateDto> Handle(GetExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var exchangeRate = await _context.ExchangeRates
                .FirstOrDefaultAsync(er => er.Id == request.Id, cancellationToken);

            if (exchangeRate == null)
                throw new Exception("Exchange rate not found");

            return new ExchangeRateDto
            {
                Id = exchangeRate.Id,
                FromCurrency = exchangeRate.FromCurrency,
                ToCurrency = exchangeRate.ToCurrency,
                Rate = exchangeRate.Rate,
                EffectiveDate = exchangeRate.EffectiveDate
            };
        }
    }
}
