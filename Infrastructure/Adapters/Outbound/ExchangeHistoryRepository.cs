using desafio.Application.Ports.Outbound;
using desafio.Domain.Entities;
using desafio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace desafio.Infrastructure.Adapters.Outbound;

public class ExchangeHistoryRepository : IExchangeHistoryRepository
{
    private readonly AppDbContext _context;

    public ExchangeHistoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveExchangeHistoryAsync(ExchangeHistory exchangeHistory, CancellationToken ct = default)
    {
        _context.ExchangeHistories.Add(exchangeHistory);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<List<ExchangeHistory>> GetExchangeHistoryAsync(CancellationToken ct = default)
    {
        return await _context.ExchangeHistories
            .AsNoTracking()
            .OrderByDescending(h => h.Timestamp)
            .ToListAsync(ct);
    }
}