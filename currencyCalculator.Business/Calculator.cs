using currencyCalculator.Business.Models;

namespace currencyCalculator.Business;

public class Calculator
{
    private RatesHandler _rates;
    public Calculator() => _rates = new RatesHandler();
    public double calculateCurrency(string fromCurrency, string toCurrency, double amount)
    {
        var rates = _rates.ReadCurrencies();
        var foundRate = rates.Where(rate => rate.ToCurrency.Equals(toCurrency)).Select(rate => rate.Rate).FirstOrDefault();
        var finalRate = foundRate * amount;
        return finalRate;
    }

}
