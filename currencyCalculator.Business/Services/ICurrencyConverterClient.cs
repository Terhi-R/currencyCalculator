using currencyCalculator.Business.Models;

namespace currencyCalculator.Business.Services;

public interface ICurrencyConverterClient
{
    Task<CurrencyResponse> currencyRatesByDate(string date, string toCurrency, string fromCurrency);
}