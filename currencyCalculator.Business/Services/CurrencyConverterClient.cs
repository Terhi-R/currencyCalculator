using System.Net.Http.Headers;
using System.Text.Json;
using currencyCalculator.Business.Models;

namespace currencyCalculator.Business.Services;

public class CurrencyConverterClient : ICurrencyConverterClient
{
    public async Task<CurrencyResponse> CurrencyRatesByDate(string fromCurrency, string toCurrency, string date)
    {
        var client = new HttpClient(); 
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("apikey", "qL9q3kgftZbWfewm3fZXgbiykPig9Qni");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var url = $"https://api.apilayer.com/fixer/{date}?symbols={toCurrency}&base={fromCurrency}";
        var currencyTask = client.GetStreamAsync(url);

        return await JsonSerializer.DeserializeAsync<CurrencyResponse>(await currencyTask);
    }
}