using currencyCalculator.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace currencyCalculator.Tests;

public class CurrencyUnitTests
{
    Calculator calculatorClass;
    private IRatesHandler _ratesHandler;
    private ICurrencyConverterClient _currencyConverter;
    public CurrencyUnitTests()
    {
        var serviceProvider = new ServiceCollection()
                                .AddScoped<ICurrencyConverterClient, CurrencyConverterClient>()
                                .AddScoped<IRatesHandler, RatesHandler>()
                                .BuildServiceProvider();

        _ratesHandler = serviceProvider.GetService<IRatesHandler>()!;
        _currencyConverter = serviceProvider.GetService<ICurrencyConverterClient>()!;

        this.calculatorClass = new Calculator(_ratesHandler, _currencyConverter);
    }
    
    [Fact]
    public void calculator_does_calculation()
    {
        //arrange

        //act
        var calculator = calculatorClass.CalculateCurrency("EUR", "NOK", 1.0);

        //assert
        calculator.Should().Be(11.7010);
    }  

    [Fact]
    public void calculator_will_throw_if_no_values_are_given()
    {
        //arrange

        //act
        var calculator = () => calculatorClass.CalculateCurrency("", "", 1.0);

        //assert
        calculator.Should().Throw<InvalidDataException>();
    }  

    [Fact]
    public void calculator_will_throw_if_currency_code_is_not_found()
    {
        //arrange

        //act
        var calculator = () => calculatorClass.CalculateCurrency("Fiction", "Fictionary", 1.0);

        //assert
        calculator.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void calculator_calculates_for_differrent_currencies()
    {
        //arrange

        //act
        var calculator = calculatorClass.CalculateCurrency("EUR", "USD", 50);

        //assert
        calculator.Should().Be(54.04);
    }

    [Fact]
    public void calculator_calculates_for_double_input_values()
    {
        //arrange

        //act
        var calculator = calculatorClass.CalculateCurrency("EUR", "CHF", 84.9);

        //assert
        calculator.Should().Be(82.67562000000001);
    }

    [Theory]
    [InlineData("USD", "CHF", 20, 18.019985196150998)]
    [InlineData("PLN", "ISK", 20, 669.3949784315895)]
    [InlineData("NOK", "CHF", 400, 33.28946243910777)]
    [InlineData("NOK", "EUR", 400, 34.18511238355696)]
    public void calculator_calculates_for_many_cases(string from, string to, double amount, double expected)
    {
        //act
        var calculator = calculatorClass.CalculateCurrency(from, to, amount);

        //assert
        calculator.Should().Be(expected);
    }

     [Fact]
    public void calculator_gets_currency_rates_by_date_from_external_api()
    {
        //arrange

        //act
        var calculator = calculatorClass.CalculateCurrency("GBP", "EUR", 20, "2013-12-24");

        //assert
        calculator.Should().Be(23.929520000000004);
    } 

    [Fact]
    public void latestRates_are_found_successfylly_from_external_api()
    {
        //arrange
        ICurrencyConverterClient currencyClient = new CurrencyConverterClient();
        var listOfCurrencies = _ratesHandler
                                    .ReadCurrencies()
                                        .Take(3)
                                        .Select(currency => currency.ToCurrency)
                                        .ToList();
        var str = string.Join(",", listOfCurrencies);

        //act
        var latestCurrencies = currencyClient.LatestCurrencyRates("EUR", str);
        var foundCurrencyRates = latestCurrencies.Result.CurrencyRates.ToList();

        //assert
        latestCurrencies.Result.BaseCurrency.Should().Be("EUR");
        latestCurrencies.Result.CurrencyRates.Count.Should().Be(3);
        foundCurrencyRates[0].ToCurrency.Should().Be("USD");
    }   
}