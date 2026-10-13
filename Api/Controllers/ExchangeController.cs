using desafio.Api.DTOs;
using desafio.Application.Ports.Inbound;
using desafio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace desafio.Api.Controllers;

[ApiController]
[Route("api/cambio")]
public class ExchangeController
{
   public readonly IExchangeService _exchangeService;

    public ExchangeController(IExchangeService exchangeService)
     {
          _exchangeService = exchangeService;
     }
    
     [HttpGet("cotacao")]
     public async Task<IActionResult> GetExchangeRate([FromQuery] string moeda, CancellationToken ct)
     {  
        string from = "BRL"; // Default from currency
        var rate = await _exchangeService.GetExchangeRateAsync(from, moeda, ct);
        return new OkObjectResult(rate);
     }
    
     [HttpPost("converter")]
     public async Task<IActionResult> ConvertCurrency([FromBody] ConvertRequest request, CancellationToken ct)
     {
        string to = "BRL"; // Default to currency
        var exchangeHistory = await _exchangeService.ConvertAsync(request.MoedaOrigem, to, request.ValorOrigem, ct);
        return new OkObjectResult(exchangeHistory);
     }
}