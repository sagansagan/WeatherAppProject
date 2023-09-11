using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Diagnostics.Metrics;
using static System.Net.WebRequestMethods;
using static System.Reflection.Metadata.BlobBuilder;

namespace WeatherAppProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            //built in healthcheck service
            builder.Services.AddHealthChecks();

            var app = builder.Build();

            Counter callCounter = new Counter();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseCors("corsapp");

            app.UseAuthorization();

            WeatherForecast[] cities = {
            new() { City = "stockholm", Temperature = 18, Humidity = 70, Wind = 10},
            new() { City = "kiruna"},
            new() { City = "lund"},
            new() { City = "copenhagen"},
            new() { City = "karlstad"}
            };
            app.MapGet("/weather/{cityName}", async (string cityName) =>
            {
                await Task.Delay(10);
                callCounter.Increment();
                var city = cities.Where(x => x.City.Equals(cityName.ToLower())).FirstOrDefault();
                if (city is null)
                {
                    return Results.NotFound(new { message = $"Sorry, could not find {cityName}" });
                }
                return Results.Ok(city);
            });

            app.MapGet("/health", async http =>
            {
                callCounter.Increment();
                var healthCheckService = http.RequestServices.GetRequiredService<HealthCheckService>();
                var healthCheckResult = await healthCheckService.CheckHealthAsync();

                if (healthCheckResult.Status == HealthStatus.Healthy)
                {
                    http.Response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    http.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }
            });

            app.MapGet("/weather", () =>
            {
                callCounter.Increment();
                return Results.Ok(cities);
            });

            app.MapGet("/api/calls", async () =>
            {
                callCounter.Increment();
                await Task.Delay(10);
                return callCounter.Callcount;
            });

            app.MapGet("/add/city/{favCity}", (string favCity) =>
            {

                callCounter.Increment();
                var city = cities.Where(x => x.City == favCity.ToLower()).FirstOrDefault();
                if (city is null)
                {
                    return Results.NotFound(new { message = "City not found" });
                }
                return Results.Ok(new { message = $"You added: {favCity} as your favorite city" });
            });


            app.Run();
        }
    }
}
public partial class Program { }