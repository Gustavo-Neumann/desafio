namespace desafio.Domain.Entities;

public class ExchangeHistory
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string FromCurrency { get; set; }
    public required string ToCurrency { get; set; }
    public required decimal Rate { get; set; }
    public required decimal Amount { get; set; }
    public required decimal ConvertedAmount { get; set; }
    public required DateTime Timestamp { get; set; }
}