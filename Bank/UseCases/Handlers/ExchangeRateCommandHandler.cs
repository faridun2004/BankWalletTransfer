using Bank.Contexts;
using Bank.Models;
using Bank.UseCases.Commands;
using MediatR;

namespace Bank.UseCases.Handlers
{
    public class CreateExchangeRateCommandHandler : IRequestHandler<CreateExchangeRateCommand, Guid>
    {
        private readonly BankDbContext _context;

        public CreateExchangeRateCommandHandler(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var exchangeRate = new ExchangeRate
            {
                Id = Guid.NewGuid(),
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                Rate = request.Rate,
                EffectiveDate = request.EffectiveDate
            };

            _context.ExchangeRates.Add(exchangeRate);

            await _context.SaveChangesAsync(cancellationToken);

            return exchangeRate.Id;
        }
    }
}
