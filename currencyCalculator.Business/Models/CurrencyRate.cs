using System.Text.Json.Serialization;

namespace currencyCalculator.Business.Models;

public class CurrencyRate
{
    public int Id { get; set; }
    public required string FromCurrency { get; set; }
    public required string ToCurrency { get; set; }
    public required double Rate { get; set; }
    public required string Date { get; set; }
}
