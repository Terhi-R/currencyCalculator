using currencyCalculator.Business.Models;

namespace currencyCalculator.Business.Services;

public interface IRatesHandler
{
    List<CurrencyRate> ReadCurrencies();
}