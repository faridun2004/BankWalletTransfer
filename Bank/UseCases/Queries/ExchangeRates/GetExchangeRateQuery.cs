using Bank.DTOs;
using MediatR;

namespace Bank.UseCases.Queries.ExchangeRates
{
    public record GetExchangeRateQuery(Guid Id) : IRequest<ExchangeRateDto>;
}
