using currencyCalculator.App;
using currencyCalculator.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace currencyCalculator.App;
class Program
{
    public static IRatesHandler? _rates { get; private set; }
    public static ICurrencyConverterClient? _currencyConverterClient { get; private set; }

    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<ICurrencyConverterClient, CurrencyConverterClient>()
            .AddScoped<IRatesHandler, RatesHandler>()
            .BuildServiceProvider();
        _rates = serviceProvider.GetService<IRatesHandler>();
        _currencyConverterClient = serviceProvider.GetService<ICurrencyConverterClient>();

        if (_rates is null) throw new ArgumentNullException();

        SeedData.Configure(_rates);
        SeedData.Initialize();
        RunConsole();
    }
    public static void RunConsole()
    {
        if (_rates is null || _currencyConverterClient is null) throw new ArgumentNullException();

        var ratesCalculator = new Calculator(_rates, _currencyConverterClient);
        Console.WriteLine("Currency Rate Calculator");
        Console.WriteLine("Submit in format FromCurrency,ToCurrency,Amount,Date | Date is optional and is given in following format: YEAR-MM-DD.");
        Console.WriteLine("Example: EUR,USD,10 | Please communicate currencies in currency codes");
        var input = Console.ReadLine()?.Split(",");

        if (input is null || input[0] is null || input[1] is null || input[2] is null) return;

        var fromCurrency = input[0].Trim();
        var toCurrency = input[1].Trim();
        var amount = input[2].Trim();

        if (input is not null && input.Length is 3)
        {
            var currentRate = ratesCalculator
                                .CalculateCurrency(fromCurrency, toCurrency, Convert.ToDouble(amount));
            Console.WriteLine($"{amount} {fromCurrency} is {currentRate} {toCurrency}");
            return;
        }  

        var rateByDate = ratesCalculator
                            .CalculateCurrency(fromCurrency, toCurrency, Convert.ToDouble(amount), input?[3].Trim());
        Console.WriteLine($"{amount} {fromCurrency} is {rateByDate} {toCurrency}");
    }
}