using Bank.Contexts;
using Bank.Models;
using Bank.UseCases.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Handlers
{
    public class CreateAccountCommandHandler: IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly BankDbContext _context;

        public CreateAccountCommandHandler(BankDbContext context)
        {
            _context = context;
        }   
        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                PhoneNumber = request.PhoneNumber,
                //Wallet = new Wallet
                //{
                //    Id = Guid.NewGuid(),
                //    UsdBalance = 0,
                //    TjsBalance = 0
                //}
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync(cancellationToken);
            return account.Id;
        }
    }
}
