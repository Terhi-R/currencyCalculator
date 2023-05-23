using currencyCalculator.Business;
using currencyCalculator.Business.Models;

public static class SeedData
{
    private static RatesHandler _rates;
    static SeedData() => _rates = new RatesHandler();
    public static void Initialize()
    {
        using (var context = new ApplicationDbContext())
        {
            if (context.CurrencyRate.Any()) { return; } 

            var allRates = _rates.ReadCurrencies()
                                              .Select(ratesFromFile => 
                                                          new CurrencyRate
                                                              {
                                                                  FromCurrency = ratesFromFile.FromCurrency,
                                                                  ToCurrency = ratesFromFile.ToCurrency,
                                                                  Rate = ratesFromFile.Rate,
                                                                  Date = ratesFromFile.Date
                                                              })
                                              .ToList();

            allRates.ForEach(rate => context.CurrencyRate.Add(rate));
            
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