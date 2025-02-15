using Bank.Contexts;
using Bank.Models;
using Bank.UseCases.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly BankDbContext _context;
        public CreateUserCommandHandler(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
                //Account = new Account
                //{
                //    Id = Guid.NewGuid(),
                //    Wallet = new Wallet
                //    {
                //        Id = Guid.NewGuid(),
                //        UsdBalance = 0,
                //        TjsBalance = 0
                //    }
                //}
            };
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == user.Id);
            if (existingAccount != null)
            {
                throw new InvalidOperationException("У пользователя уже есть аккаунт.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
