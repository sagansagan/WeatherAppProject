using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
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
            var expectedContent = new Weather("stockholm", 18, 70, 10);
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

        [Fact]
        public void CallCounter_WhenCalled_IncreasesByOne()
        {
            var callCounter = new Counter();
            callCounter.Increment();
            Assert.Equal(1, callCounter.Callcount);
        }

        [Theory]
        [InlineData("add/city/stockholm")]
        [InlineData("add/city/kiruna")]
        [InlineData("add/city/lund")]
        [InlineData("add/city/copenhagen")]
        [InlineData("add/city/karlstad")]
        public async Task Add_City_Endpoint_With_Different_Params_Returns_OK(string endpoint)
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var stopwatch = Stopwatch.StartNew();

            HttpResponseMessage response = await client.GetAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("kiruna")]
        [InlineData("copenHagen")]

        public async Task Add_City_Endpoint_Returns_Correct_Message_When_Requested(string favCity)
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var stopwatch = Stopwatch.StartNew();

           var expected = JsonConvert.SerializeObject(new { message = $"You added: {favCity} as your favorite city" });

            // Act
            HttpResponseMessage response = await client.GetAsync($"/add/city/{favCity}");
            var actual = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expected, actual); 
        }
    }
}