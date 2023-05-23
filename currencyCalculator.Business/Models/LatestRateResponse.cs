using System.Text.Json.Serialization;

namespace currencyCalculator.Business.Models;

public class LatestRateResponse
{
    public int Id { get; set; }
    [JsonPropertyName("base")]
    public required string BaseCurrency { get; set; }
    [JsonPropertyName("rates")]
    public Dictionary<string,double>? Rates { get; set; }
    public List<CurrencyRate> CurrencyRates
    {
        get
        {
            return Rates
                    .Select(rate => new CurrencyRate
                                        {
                                            FromCurrency = BaseCurrency,
                                            ToCurrency = rate.Key,
                                            Rate = rate.Value,
                                            Date = Date
                                        }) 
                    .ToList();
        }
    }

    [JsonPropertyName("date")]
    public required string Date { get; set; }

}