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
}