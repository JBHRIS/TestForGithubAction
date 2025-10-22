namespace TestForGithubAction.Tests;

public class WeatherForecastTests
{
    [Fact]
    public void TemperatureF_ConvertsFromCelsius_Correctly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 1);
        var temperatureC = 0;
        var summary = "Freezing";

        // Act
        var forecast = new WeatherForecast(date, temperatureC, summary);

        // Assert
        Assert.Equal(32, forecast.TemperatureF);
    }

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(-40, -40)]
    [InlineData(20, 68)]
    [InlineData(30, 86)]
    public void TemperatureF_ConvertsVariousTemperatures_Correctly(int celsius, int expectedFahrenheit)
    {
        // Arrange
        var date = new DateOnly(2024, 1, 1);
        var summary = "Test";

        // Act
        var forecast = new WeatherForecast(date, celsius, summary);

        // Assert
        Assert.Equal(expectedFahrenheit, forecast.TemperatureF);
    }

    [Fact]
    public void WeatherForecast_InitializesWithCorrectValues()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 1);
        var temperatureC = 25;
        var summary = "Warm";

        // Act
        var forecast = new WeatherForecast(date, temperatureC, summary);

        // Assert
        Assert.Equal(date, forecast.Date);
        Assert.Equal(temperatureC, forecast.TemperatureC);
        Assert.Equal(summary, forecast.Summary);
    }

    [Fact]
    public void WeatherForecast_AllowsNullSummary()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 1);
        var temperatureC = 15;

        // Act
        var forecast = new WeatherForecast(date, temperatureC, null);

        // Assert
        Assert.Null(forecast.Summary);
    }

    [Fact]
    public void WeatherForecast_NegativeTemperature_ConvertsCorrectly()
    {
        // Arrange
        var date = new DateOnly(2024, 1, 1);
        var temperatureC = -20;
        var summary = "Freezing";

        // Act
        var forecast = new WeatherForecast(date, temperatureC, summary);

        // Assert
        Assert.Equal(-4, forecast.TemperatureF);
    }
}
