using MediatR;

namespace Bank.UseCases.Commands
{
    public class CreateWalletCommand: IRequest<Guid>
    {
        public Guid AccountId { get; set; }
        public double UsdBalance { get; set; }
        public double TjsBalance { get; set; }

    }
}
