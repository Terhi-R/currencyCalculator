using currencyCalculator.Business.Services;
using Moq;

namespace currencyCalculator.Tests;

public class CurrencyUnitTests
{
    Calculator calculatorClass;
    IRatesHandler _ratesHandler;
    ICurrencyConverterClient _currencyConverter;

    public CurrencyUnitTests()
    {
        _ratesHandler = new RatesHandler();
        _currencyConverter = new CurrencyConverterClient();
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

    [Fact]
    public void calculator_calculates_from_other_currencies_than_EUR()
    {
        //arrange

        //act
        var calculator = calculatorClass.CalculateCurrency("USD", "CHF", 20);
        var secondCalculator = calculatorClass.CalculateCurrency("PLN", "ISK", 20);

        //assert
        calculator.Should().Be(22.14);
        secondCalculator.Should().Be(2915.59); 
    }

  /*   [Fact]
    public void calculator_gets_currency_rates_by_date_from_external_api()
    {
        //arrange

        //act
        var calculator = calculatorClass.CalculateCurrency("GBP", "EUR", 20, "2013-12-24");

        //assert
        calculator.Should().Be(23.929520000000004);
    } */

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