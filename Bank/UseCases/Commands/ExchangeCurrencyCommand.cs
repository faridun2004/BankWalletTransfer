using Bank.Models;
using MediatR;

namespace Bank.UseCases.Commands
{
    public class ExchangeCurrencyCommand : IRequest<Guid>
    {
        public Guid WalletId { get; set; }
        public double Amount { get; set; }
        public WalletStatus CurrencyFrom { get; set; }
        public WalletStatus CurrencyTo { get; set; }
        public Guid WalletSentId { get; set; }
    }
}
