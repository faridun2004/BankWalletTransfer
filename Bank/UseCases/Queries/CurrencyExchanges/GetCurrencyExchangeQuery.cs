using Bank.DTOs;
using MediatR;

namespace Bank.UseCases.Queries.CurrencyExchanges
{
    public class GetCurrencyExchangeQuery : IRequest<CurrencyExchangeDto>
    {
        public Guid ExchangeId { get; set; }
    }
}
