namespace desafio.Application.Ports.Outbound;
public interface IExchangeRateClient
{
    Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, CancellationToken cancellationToken = default);
}