using Coravel.Invocable;
using currencyCalculator.Business.Models;
using currencyCalculator.Business.Services;

public class InvokeDb : IInvocable
{
    private static IRatesHandler? _rates;
    private static ICurrencyConverterClient? _currencyConverterClient;
    public InvokeDb(IRatesHandler ratesHandler, ICurrencyConverterClient currencyConverter)
    {
        _rates = ratesHandler;
        _currencyConverterClient = currencyConverter;
    }
    public Task Invoke()
    {
        Console.WriteLine("Program has ran");
        using (var context = new ApplicationDbContext())
        {
            if (_rates is null) return Task.CompletedTask;
            var listOfCurrencies = _rates.ReadCurrencies().Select(currency => currency.ToCurrency).ToList();
            var currencyStr = string.Join(",", listOfCurrencies);


            if (_currencyConverterClient is null) return Task.CompletedTask;
            var fetchNewRates = _currencyConverterClient.LatestCurrencyRates("EUR", currencyStr);

            var validRates = new List<CurrencyRate>();

            foreach (var newRate in fetchNewRates.Result.CurrencyRates)
            {
                if (newRate is null) continue;
                validRates.Add(newRate);
            }

            validRates.ForEach(rate => context.CurrencyRate.Add(rate));
            context.SaveChanges();
        }
        return Task.CompletedTask;
    }
}