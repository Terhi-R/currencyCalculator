using System.Globalization;
using currencyCalculator.Business.Models;

namespace currencyCalculator.Business;

public class RatesHandler
{
    public List<CurrencyRate> ReadCurrencies()
    {
        var path = "/Users/terhiraudaskoski/Documents/GitHubProjects/Riv/currencyCalculator/data/rates.csv";
        var currencyRatesData = System.IO.File.ReadLines(path).ToList();
        var provider = new NumberFormatInfo();
        provider.NumberDecimalSeparator = ".";
        provider.NumberGroupSeparator = ",";
        var rates = currencyRatesData
                        .Select(currencyRate => currencyRate.Split(","))
                        .Where(line => !line.Contains("Date"))
                        .Select(rate => new CurrencyRate()
                        {
                            FromCurrency = rate[1],
                            ToCurrency = rate[0],
                            CurrencyName = rate[2],
                            Rate = Convert.ToDouble(rate[3], provider),
                            Date = rate[4]
                        })
                        .ToList();
        return rates;
    }
}