using Coravel.Invocable;
using currencyCalculator.App.Models;
using currencyCalculator.App.Services;

public class InvokeDb : IInvocable
{
    private static IRatesHandler? _rates;
    private static ILatestCurrenciesClient? _latestCurrenciesClient;
    public InvokeDb(IRatesHandler ratesHandler, ILatestCurrenciesClient currencyConverter)
    {
        _rates = ratesHandler;
        _latestCurrenciesClient = currencyConverter;
    }
    public Task Invoke()
    {
        using (var context = new ApplicationDbContext())
        {
            if (_rates is null || _latestCurrenciesClient is null) return Task.CompletedTask;

            var listOfCurrencies = _rates
                                    .ReadCurrencies()
                                        .Select(currency => currency.ToCurrency)
                                        .ToList();

            var currencyStr = string.Join(",", listOfCurrencies);

            var fetchNewRates = _latestCurrenciesClient.LatestCurrencyRates("EUR", currencyStr);

            var validRates = new List<CurrencyRate>();
            foreach (var newRate in fetchNewRates.Result.CurrencyRates)
            {
                if (newRate is null) continue;
                validRates.Add(newRate);
            }

            context.CurrencyRate?.AddRange(validRates);
            context.SaveChanges();
        }
        return Task.CompletedTask;
    }
}