using desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace desafio.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ExchangeHistory> ExchangeHistories => Set<ExchangeHistory>();
}