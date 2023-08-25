using Microsoft.Extensions.Diagnostics.HealthChecks;
using static System.Net.WebRequestMethods;

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

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseCors("corsapp");

            app.UseAuthorization();

            var weather = new WeatherForecast
            {

                City = "Stockholm",
                Temperature = 18,
                Humidity = 70,
                Wind = 10,
            };

            app.MapGet("/health", async http =>
            {
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

            app.MapGet("/weather/stockholm", () => Results.Ok(weather));

 
            app.Run();
        }
    }
}
public partial class Program { }