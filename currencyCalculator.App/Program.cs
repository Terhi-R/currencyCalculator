using currencyCalculator.Business;
using Microsoft.Extensions.Hosting;

namespace currencyCalculator.App;

class Program
{
    static void Main(string[] args)
    {
        SeedData.Initialize();
       /*  RunConsole(); */
    }

    public static void RunConsole()
    {
        var ratesCalculator = new Calculator();
        Console.WriteLine("Currency Rate Calculator");
        Console.WriteLine("Submit in format FromCurrency,ToCurrency,Amount,Date | Date is optional and is given in following format: YEAR-MM-DD.");
        Console.WriteLine("Example: EUR,USD,10 | Please communicate currencies in currency codes");
        var input = Console.ReadLine()?.Split(",");

        if (input is null || input[0] is null || input[1] is null || input[2] is null)
        {
            Console.WriteLine("Invalid input. Please state all three values: From Currency,To Currency,Amount");
            return;
        }

        var fromCurrency = input[0].Trim();
        var toCurrency = input[1].Trim();
        var amount = input[2].Trim();


        if (input is not null && input.Length is 3)
        {
            var currentRate = ratesCalculator.CalculateCurrency(fromCurrency, toCurrency, Convert.ToDouble(amount));
            Console.WriteLine($"{amount} {fromCurrency} is {currentRate} {toCurrency}");
            return;
        }

        var rateByDate = ratesCalculator.CalculateCurrency(fromCurrency, toCurrency, Convert.ToDouble(amount), input![3]);
        Console.WriteLine($"{amount} {fromCurrency} is {rateByDate} {toCurrency}");
        return;
    }
}