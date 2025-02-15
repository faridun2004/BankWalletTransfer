using Bank.UseCases.Commands;
using Bank.UseCases.Queries.Wallets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalletById(Guid id)
        {
            var wallet = await _mediator.Send(new GetWalletById { WalletId = id });
            return Ok(wallet);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletCommand command)
        {
            var walletId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetWalletById), new { id = walletId }, walletId);
        }
    }
}
