using System.Globalization;
using currencyCalculator.App.Models;

namespace currencyCalculator.App.Services;

public class RatesHandler : IRatesHandler
{
    public List<CurrencyRate> ReadCurrencies()
    {
        var path = Directory.GetParent(Environment.CurrentDirectory).FullName + "/data/rates.csv";
        if (path.Contains(".Tests/bin")) 
        {
            path = path.Split("/currencyCalculator.Tests")[0] + "/data/rates.csv";
        }
        var currencyRatesData = System.IO.File.ReadLines(path).ToList();
        var provider = new NumberFormatInfo();
        provider.NumberDecimalSeparator = ".";
        provider.NumberGroupSeparator = ",";

        var rates = currencyRatesData
                        .Select(line => line.Split(","))
                        .Where(line => !line.Contains("Date"))
                        .Select(line => new CurrencyRate()
                        {
                            FromCurrency = line[1],
                            ToCurrency = line[0],
                            Rate = Convert.ToDouble(line[3], provider),
                            Date = line[4]
                        })
                        .ToList();
        return rates;
    }
}