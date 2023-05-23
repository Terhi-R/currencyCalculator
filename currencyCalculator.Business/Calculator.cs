using currencyCalculator.Business.Models;
using currencyCalculator.Business.Services;

namespace currencyCalculator.Business;

public class Calculator
{
    private readonly IRatesHandler _rates;
    private readonly ICurrencyConverterClient _currencyConverterClient;
    public Calculator(IRatesHandler ratesHandler, ICurrencyConverterClient currencyConverter)
    {
        _rates = new RatesHandler();
        _currencyConverterClient = new CurrencyConverterClient();
    }

    public double CalculateCurrency(string fromCurrency, string toCurrency, double amount, string? date = null)
    {
        if (fromCurrency is "" || toCurrency is "") throw new InvalidDataException();

        if (date is not null)
        {
            var rateFromGivenDate = _currencyConverterClient
                                        .CurrencyRatesByDate(fromCurrency, toCurrency, date);
            return rateFromGivenDate.Result.Rate * amount;
        }

        var allRates = _rates.ReadCurrencies();

        var rateFromEUR = allRates
                            .Where(rate => rate.ToCurrency.Equals(toCurrency)
                                            && rate.FromCurrency.Equals("EUR"))
                            .Select(rate => rate.Rate)
                            .FirstOrDefault();

        if (rateFromEUR is 0.0) throw new ArgumentNullException();

        var calculateRate = allRates
                            .Where(currency => !fromCurrency.Equals("EUR")
                                                    && currency.ToCurrency.Equals(fromCurrency))
                            .Select(currency => Math.Abs((currency.Rate - rateFromEUR + 1) * amount))
                            .FirstOrDefault();

        if (calculateRate is 0.0) return rateFromEUR * amount;
        return calculateRate;
    }

}
