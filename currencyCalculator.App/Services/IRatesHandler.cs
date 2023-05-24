using currencyCalculator.App.Models;

namespace currencyCalculator.App.Services;

public interface IRatesHandler
{
    List<CurrencyRate> ReadCurrencies();
}