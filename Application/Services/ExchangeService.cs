using desafio.Application.Ports.Outbound;
using Microsoft.Extensions.Caching.Memory;
using desafio.Domain.Entities;
using desafio.Application.Ports.Inbound;

namespace desafio.Application.Services;

public class ExchangeService : IExchangeService
{
    private readonly IExchangeRateClient _exchangeClient;
    private readonly IMemoryCache _memoryCache;
    private readonly IExchangeHistoryRepository _exchangeHistoryRepository;

    public ExchangeService(IExchangeRateClient exchangeClient, IMemoryCache memoryCache, IExchangeHistoryRepository exchangeHistoryRepository)
    {
        _exchangeClient = exchangeClient;
        _memoryCache = memoryCache;
        _exchangeHistoryRepository = exchangeHistoryRepository;
    }

    public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, CancellationToken cancellationToken = default)
    {
        fromCurrency = fromCurrency.ToUpperInvariant();
        toCurrency = toCurrency.ToUpperInvariant();

        var key = $"exchange:{fromCurrency}-{toCurrency}";

        if (_memoryCache.TryGetValue(key, out decimal cachedRate))
        {
            return cachedRate;
        }

        var rate = await _exchangeClient.GetExchangeRateAsync(fromCurrency, toCurrency, cancellationToken);
        _memoryCache.Set(key, rate);
        return rate;
        
        throw new InvalidOperationException("Failed to parse exchange rate");
    }

    public async Task<ExchangeHistory> ConvertAsync(
        string from, string to, decimal amount, CancellationToken ct = default)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

        var rate = await GetExchangeRateAsync(from, to, ct);

        var history = new ExchangeHistory
        {
            FromCurrency = from,
            ToCurrency = to,
            Rate = rate,
            Amount = amount,
            ConvertedAmount = Math.Round(amount * rate, 2),
            Timestamp = DateTime.UtcNow
        };

        await _exchangeHistoryRepository.SaveExchangeHistoryAsync(history, ct);

        return history;
    }
}