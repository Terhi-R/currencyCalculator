using currencyCalculator.Business;

namespace currencyCalculator.App;

class Program
{
    static void Main(string[] args)
    {
        var ratesCalculator = new Calculator();
        Console.WriteLine("Currency Rate Calculator \n Submit in format FromCurrency,ToCurrency,Amount,Date | Date is optional. (Example: EUR,USD,10) \n Please communicate currencies in currency codes");
        var input = Console.ReadLine()?.Split(",");
        
        if (input is null || input[0] is null || input[1] is null || input[2] is null)
        {
            Console.WriteLine("Invalid input. Please state all three values: FromCurrency,ToCurrency,Amount");
        }

        if (input is not null && input.Length is 3)
        {
            var currentRate = ratesCalculator.calculateCurrency(input![0], input[1], Convert.ToDouble(input[2]));
            Console.WriteLine($"{input[2]} {input[0]} is {currentRate} {input[1]}");
        }

        var rateByDate = ratesCalculator.calculateCurrency(input![0],input[1],Convert.ToDouble(input[2]), input[3]);
        Console.WriteLine($"{input[2]} {input[0]} is {rateByDate} {input[1]}");
    }
}