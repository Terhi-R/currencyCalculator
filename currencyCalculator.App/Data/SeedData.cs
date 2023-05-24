using currencyCalculator.App.Services;
using currencyCalculator.App.Models;

public static class SeedData
{
    private static IRatesHandler _rates;
    static SeedData() => _rates = new RatesHandler();
    public static void Initialize()
    {
        using (var context = new ApplicationDbContext())
        {
            if (context.CurrencyRate is null || context.CurrencyRate.Any()) { return; } 

            var allRates = _rates.ReadCurrencies();
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