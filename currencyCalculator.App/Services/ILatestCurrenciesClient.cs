using currencyCalculator.App.Models;

namespace currencyCalculator.App.Services;

public interface ILatestCurrenciesClient
{
    Task<CurrencyResponse> CurrencyRatesByDate(string date, string toCurrency, string fromCurrency);
    Task<LatestRateResponse> LatestCurrencyRates(string baseCurrency, string toCurrencies);
}