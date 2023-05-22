using currencyCalculator.Business.Models;

namespace currencyCalculator.Business;

public class Calculator
{
    private RatesHandler _rates;
    public Calculator() => _rates = new RatesHandler();
    public double calculateCurrency(string fromCurrency, string toCurrency, double amount)
    {
        if (fromCurrency is "" || toCurrency is "") throw new InvalidDataException();

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
