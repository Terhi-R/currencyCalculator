namespace currencyCalculator.Tests;

public class CurrencyUnitTests
{
    [Fact]
    public void calculator_does_calculation()
    {
        //arrange
        var calculatorClass = new Calculator();

        //act
        var calculator = calculatorClass.calculateCurrency("EUR", "NOK", 1.0);

        //assert
        calculator.Should().Be(11.7010);
    }  

    [Fact]
    public void calculator_will_throw_if_no_values_are_given()
    {
        //arrange
        var calculatorClass = new Calculator();

        //act
        var calculator = () => calculatorClass.calculateCurrency("", "", 1.0);

        //assert
        calculator.Should().Throw<InvalidDataException>();
    }  

    [Fact]
    public void calculator_will_throw_if_currency_code_is_not_found()
    {
        //arrange
        var calculatorClass = new Calculator();

        //act
        var calculator = () => calculatorClass.calculateCurrency("Fiction", "Fictionary", 1.0);

        //assert
        calculator.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void calculator_calculates_for_differrent_currencies()
    {
        //arrange
        var calculatorClass = new Calculator();

        //act
        var calculator = calculatorClass.calculateCurrency("EUR", "USD", 50);

        //assert
        calculator.Should().Be(54.04);
    }

    [Fact]
    public void calculator_calculates_for_double_input_values()
    {
        //arrange
        var calculatorClass = new Calculator();

        //act
        var calculator = calculatorClass.calculateCurrency("EUR", "CHF", 84.9);

        //assert
        calculator.Should().Be(82.67562000000001);
    }

    [Fact]
    public void calculator_calculates_from_other_currencies_than_EUR()
    {
        //arrange
        var calculatorClass = new Calculator();

        //act
        var calculator = calculatorClass.calculateCurrency("USD", "CHF", 20);
        var secondCalculator = calculatorClass.calculateCurrency("PLN", "ISK", 20);

        //assert
        calculator.Should().Be(22.14);
        secondCalculator.Should().Be(2915.59); 
    }

    [Fact]
    public void calculator_gets_currency_rates_by_date_from_api()
    {
        //arrange
        var calculatorClass = new Calculator();

        //act
        var calculator = calculatorClass.calculateCurrency("GBP", "EUR", 20, "2013-12-24");

        //assert
        calculator.Should().Be(23.929520000000004);
    }
}