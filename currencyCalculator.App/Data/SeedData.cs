using currencyCalculator.App;
using currencyCalculator.Business;
using currencyCalculator.Business.Models;
using currencyCalculator.Business.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    private static RatesHandler _rates;
    static SeedData() => _rates = new RatesHandler();
    public static void Initialize()
    {
        using (var context = new ApplicationDbContext())
        {
            /*  if (context.CurrencyRate.Any()) { return; } */
            /*            var allRates = _rates.ReadCurrencies()
                                              .Select(ratesFromFile => 
                                                          new CurrencyRate
                                                              {
                                                                  FromCurrency = ratesFromFile.FromCurrency,
                                                                  ToCurrency = ratesFromFile.ToCurrency,
                                                                  Rate = ratesFromFile.Rate,
                                                                  Date = ratesFromFile.Date
                                                              })
                                              .ToList();

                      allRates.ForEach(rate => context.CurrencyRate.Add(rate));   */
            var rate = new CurrencyRate
            {
                FromCurrency = "EUR",
                ToCurrency = "USD",
                Rate = 1.0764397,
                Date = "2023-05-23"
            };
            
            context.CurrencyRate.Add(rate);
            context.SaveChanges();
        }
    }
}