using currencyCalculator.Business.Models;

namespace currencyCalculator.Business.Services;

public interface ICurrencyConverterClient
{
    Task<CurrencyResponse> CurrencyRatesByDate(string date, string toCurrency, string fromCurrency);
    Task<LatestRateResponse> LatestCurrencyRates(string baseCurrency, string toCurrencies);
}