using desafio.Application.Ports.Outbound;
using desafio.Infrastructure.Adapters.Outbound.DTOs;

namespace desafio.Infrastructure.Adapters.Outbound;
public class EconomiaExchangeRateClient : IExchangeRateClient
{
    private readonly HttpClient _httpClient;
    
    public EconomiaExchangeRateClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetExchangeRateAsync(
        string fromCurrency,
        string toCurrency,
        CancellationToken cancellationToken = default)
    {
        string url = $"/json/last/{fromCurrency}-{toCurrency}";
        var response = await _httpClient.GetFromJsonAsync<Dictionary<string, ExchangeRateResponse>>(url, cancellationToken);
        
        string key = $"{fromCurrency}{toCurrency}";

        if (response != null && response.TryGetValue(key, out var exchangeRateResponse))
        {
            return exchangeRateResponse.Bid;
        }

        throw new Exception("Exchange rate not found.");
    }


}
