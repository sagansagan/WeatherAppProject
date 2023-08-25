using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Net;
using WeatherAppProject;

namespace WeatherAppTests
{
    internal record Weather(string City, int Temperature, int Humidity, int Wind);
    public class WeatherAppTests
    {
        //private readonly HttpClient _httpClient = new()
        //{
        //    BaseAddress = new Uri("https://localhost:7238")
        //};

        [Fact]
        public async Task GetWeather_ReturnsWeatherData()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedContent = new Weather("Stockholm", 18, 70, 10);
            var stopwatch = Stopwatch.StartNew();

            var response = await client.GetAsync("/weather/stockholm");

            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var stopwatch = Stopwatch.StartNew();

            // Act
            HttpResponseMessage response = await client.GetAsync("/health");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}