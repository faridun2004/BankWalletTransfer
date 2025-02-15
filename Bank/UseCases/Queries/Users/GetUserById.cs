using Bank.Contexts;
using Bank.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.UseCases.Queries.Users
{
    public class GetUserById: IRequest<UserDto>
    {
        public Guid UserId { get; set; }
    }
    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
    {
        private readonly BankDbContext _bankDbContext;
        public GetUserByIdHandler(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }
        public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _bankDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if(user == null) throw new Exception("User not found");
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
