using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Net.Http;
using WeatherAppProject;

namespace WeatherAppTests
{
    internal record Weather(string City, int Temperature, int Humidity, int Wind);
    public class WeatherAppTests
    {
        private readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5287")
        };

        [Fact]
        public async Task GetWeather_ReturnsWeatherData()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new Weather("Stockholm", 18, 70, 8);
            var stopwatch = Stopwatch.StartNew();

            var response = await _httpClient.GetAsync("/weather/stockholm");

            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);

        }
    }
}