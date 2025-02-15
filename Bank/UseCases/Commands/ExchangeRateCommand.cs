using Bank.Models;
using MediatR;

namespace Bank.UseCases.Commands
{
    public record CreateExchangeRateCommand(
    WalletStatus FromCurrency,
    WalletStatus ToCurrency,
    double Rate,
    DateTime EffectiveDate) : IRequest<Guid>;
}
