using System.Text.Json.Serialization;

namespace desafio.Infrastructure.Adapters.Outbound.DTOs;

public class ExchangeRateResponse
{
    [JsonPropertyName("bid")]
    public decimal Bid { get; set; }

    [JsonPropertyName("ask")]
    public decimal Ask { get; set; }

    [JsonPropertyName("code")]
    public string FromCurrency { get; set; } = string.Empty;

    [JsonPropertyName("codein")]
    public string ToCurrency { get; set; } = string.Empty;

    [JsonPropertyName("create_date")]
    public string CreateDate { get; set; } = string.Empty;
}