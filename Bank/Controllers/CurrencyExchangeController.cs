using Bank.Exceptions;
using Bank.Models;
using Bank.UseCases.Commands;
using Bank.UseCases.Queries.CurrencyExchanges;
using Bank.UseCases.Queries.ExchangeRates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController:ControllerBase
    {
        private readonly IMediator _mediator;
        public CurrencyExchangeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Exchangerate")]
        public async Task<IActionResult> Create([FromForm] CreateExchangeRateCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { ExchangeRateId = id, Message = "Exchange rate created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var exchange = await _mediator.Send(new GetExchangeRateQuery(id));

            if (exchange == null)
                return NotFound();

            return Ok(exchange);
        }


        [HttpGet("{byExchangeid}")]
        public async Task<IActionResult> GetCurrencyExchange(Guid id)
        {
            var query = new GetCurrencyExchangeQuery { ExchangeId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCurrencyExchange([FromForm] ExchangeCurrencyCommand command)
        {
            try
            {
                var exchangeId = await _mediator.Send(command);
                return Ok(new { ExchangeId = exchangeId, Message = "Exchange created successfully" });

            }
            catch (CustomException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex) 
            {
                return StatusCode(500);
            }

        }
        [HttpPost("transfer-by-phone")]
        public async Task<IActionResult> TransferByPhone([FromBody] TransferByPhoneCommand command)
        {
            try
            {
                var transferId = await _mediator.Send(command);
                return Ok(new { TransferId = transferId, Message = "Перевод успешно выполнен" });
            }
            catch (CustomException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Произошла ошибка", Details = ex.Message });
            }
        }

    }
}
