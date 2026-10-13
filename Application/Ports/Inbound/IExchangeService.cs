namespace desafio.Application.Ports.Inbound;
using desafio.Domain.Entities;
public interface IExchangeService
{
    Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, CancellationToken
    cancellationToken = default);
    Task<ExchangeHistory> ConvertAsync(string from, string to, decimal amount, CancellationToken ct = default);
}