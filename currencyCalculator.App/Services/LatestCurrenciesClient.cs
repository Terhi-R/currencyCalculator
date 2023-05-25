using System.Net.Http.Headers;
using System.Text.Json;
using currencyCalculator.App.Models;

namespace currencyCalculator.App.Services;

public class LatestCurrenciesClient : ILatestCurrenciesClient
{
    public async Task<CurrencyResponse> CurrencyRatesByDate(string fromCurrency, string toCurrency, string date)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("apikey", "De44yfrdnSKryD5yzeSoOBiHemeQvmbi");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var url = $"https://api.apilayer.com/fixer/{date}?symbols={toCurrency}&base={fromCurrency}";
        var currencyTask = client.GetStreamAsync(url);

        return await JsonSerializer.DeserializeAsync<CurrencyResponse>(await currencyTask) ??
               throw new ArgumentNullException();
    }

    public async Task<LatestRateResponse> LatestCurrencyRates(string baseCurrency, string toCurrencies)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("apikey", "De44yfrdnSKryD5yzeSoOBiHemeQvmbi");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var url = $"https://api.apilayer.com/fixer/latest?symbols={toCurrencies}&base={baseCurrency}";
        var currencyTask = client.GetStreamAsync(url);

        return await JsonSerializer.DeserializeAsync<LatestRateResponse>(await currencyTask) ??
               throw new ArgumentNullException();
    }
}