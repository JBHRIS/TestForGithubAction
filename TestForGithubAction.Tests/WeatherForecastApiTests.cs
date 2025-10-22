using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestForGithubAction.Tests;

public class WeatherForecastApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public WeatherForecastApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/weatherforecast");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsJsonContent()
    {
        // Act
        var response = await _client.GetAsync("/weatherforecast");

        // Assert
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsFiveForecasts()
    {
        // Act
        var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Length);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsValidData()
    {
        // Act
        var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        // Assert
        Assert.NotNull(forecasts);
        foreach (var forecast in forecasts)
        {
            Assert.NotEqual(default, forecast.Date);
            Assert.InRange(forecast.TemperatureC, -20, 55);
            Assert.NotNull(forecast.Summary);
            Assert.NotEmpty(forecast.Summary);
        }
    }

    [Fact]
    public async Task GetWeatherForecast_DatesAreInFuture()
    {
        // Arrange
        var today = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        // Assert
        Assert.NotNull(forecasts);
        foreach (var forecast in forecasts)
        {
            Assert.True(forecast.Date > today, $"Forecast date {forecast.Date} should be after today {today}");
        }
    }

    [Fact]
    public async Task GetWeatherForecast_SummariesAreValid()
    {
        // Arrange
        var validSummaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        // Act
        var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        // Assert
        Assert.NotNull(forecasts);
        foreach (var forecast in forecasts)
        {
            Assert.Contains(forecast.Summary, validSummaries);
        }
    }

    [Fact]
    public async Task GetWeatherForecast_TemperatureFIsCalculatedCorrectly()
    {
        // Act
        var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        // Assert
        Assert.NotNull(forecasts);
        foreach (var forecast in forecasts)
        {
            var expectedF = 32 + (int)(forecast.TemperatureC / 0.5556);
            Assert.Equal(expectedF, forecast.TemperatureF);
        }
    }

    [Fact]
    public async Task GetWeatherForecast_MultipleCalls_ReturnsDifferentData()
    {
        // Act
        var forecasts1 = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");
        var forecasts2 = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        // Assert
        Assert.NotNull(forecasts1);
        Assert.NotNull(forecasts2);

        // Since the data is randomly generated, at least one forecast should be different
        var allSame = true;
        for (int i = 0; i < forecasts1.Length; i++)
        {
            if (forecasts1[i].TemperatureC != forecasts2[i].TemperatureC ||
                forecasts1[i].Summary != forecasts2[i].Summary)
            {
                allSame = false;
                break;
            }
        }

        Assert.False(allSame, "Multiple calls should return different random data");
    }
}
