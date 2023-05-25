using currencyCalculator.App.Models;
using currencyCalculator.App.Services;

namespace currencyCalculator.App;

public class Calculator
{
    private readonly IRatesHandler _rates;
    private readonly ILatestCurrenciesClient _latestCurrenciesClient;
    public Calculator(IRatesHandler ratesHandler, ILatestCurrenciesClient currencyConverter)
    {
        _rates = ratesHandler;
        _latestCurrenciesClient = currencyConverter;
    }

    public double CalculateCurrency(string fromCurrency, string toCurrency, double amount, string? date = null)
    {
        if (fromCurrency is "" || toCurrency is "") throw new InvalidDataException();

        if (date is not null)
        {
            var rateFromGivenDate = _latestCurrenciesClient
                                        .CurrencyRatesByDate(fromCurrency, toCurrency, date);
            return rateFromGivenDate.Result.Rate * amount;
        }

        var allRates = _rates.ReadCurrencies();

        var foundEURRate = allRates
                            .Where(rate => rate.ToCurrency.Equals(toCurrency))
                            .Select(rate => rate.Rate * amount)
                            .FirstOrDefault();

        if (foundEURRate is 0.0 && !toCurrency.Equals("EUR")) throw new ArgumentNullException();

        if (foundEURRate is 0.0 && toCurrency.Equals("EUR"))
        {
            foundEURRate = allRates
                            .Where(rate => rate.ToCurrency.Equals(fromCurrency))
                            .Select(rate => amount / rate.Rate)
                            .FirstOrDefault();
        }

        var calculateRate = allRates
                            .Where(currency => !fromCurrency.Equals("EUR")
                                                    && !toCurrency.Equals("EUR")
                                                    && currency.ToCurrency.Equals(fromCurrency))
                            .Select(currency => (amount / currency.Rate) * (foundEURRate / amount))
                            .FirstOrDefault();

        if (calculateRate is 0.0) return foundEURRate;
        return calculateRate;
    }
}
