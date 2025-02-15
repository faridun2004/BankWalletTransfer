using Bank.Contexts;
using Bank.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Queries.Accounts
{
    public class GetAccountById : IRequest<AccountDto>
    {
        public Guid AccountId { get; set; }
    }
    public class GetAccountByIdHandler : IRequestHandler<GetAccountById, AccountDto>
    {
        private readonly BankDbContext _context;
        public GetAccountByIdHandler(BankDbContext context)
        {
            _context = context;
        }
        public async Task<AccountDto> Handle(GetAccountById request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken);
            if (account == null)            
                throw new Exception("Account not found");
            return new AccountDto
            {
                Id = account.Id,
                UserId = account.UserId,
                PhoneNumber = account.PhoneNumber,
            };
        }
    }
}
