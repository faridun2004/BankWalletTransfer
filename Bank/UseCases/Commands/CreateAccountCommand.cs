using MediatR;

namespace Bank.UseCases.Commands
{
    public class CreateAccountCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
