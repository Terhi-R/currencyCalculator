using System.Text.Json.Serialization;

namespace currencyCalculator.Business.Models;

public class CurrencyResponse
{
    public int Id { get; set; }
    [JsonPropertyName("base")]
    public required string BaseCurrency { get; set; }
    [JsonPropertyName("rates")]
    public required Dictionary<string,double> Rates { get; set; }
    public string ToCurrency { 
        get
        {
            return Rates.Keys.First();
        }
    }
    public double Rate {
        get
        {
            return Rates.Values.First();
        }
    }
    [JsonPropertyName("date")]
    public required string Date { get; set; }

}