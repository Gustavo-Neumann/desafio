using desafio.Domain.Entities;

namespace desafio.Application.Ports.Outbound;
public interface IExchangeHistoryRepository
{
    Task SaveExchangeHistoryAsync(ExchangeHistory exchangeHistory, CancellationToken cancellationToken = default);
    Task<List<ExchangeHistory>> GetExchangeHistoryAsync(CancellationToken cancellationToken = default);
}