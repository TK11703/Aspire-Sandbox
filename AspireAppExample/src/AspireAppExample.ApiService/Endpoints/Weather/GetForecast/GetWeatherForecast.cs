using Microsoft.AspNetCore.Http.HttpResults;
using AspireAppExample.Core.Dtos;
using AspireAppExample.Core.Entities;

namespace AspireAppExample.ApiService.Endpoints.Weather.GetForecast;

public class GetWeatherForecast : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/weather/forecast", Handle)
            .WithName("GetWeatherForecast");
    }

    protected virtual Results<Ok<WeatherForecastResponse>, ProblemHttpResult, NotFound> Handle()
    {
        try
        {
            string[] summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
            return TypedResults.Ok(new WeatherForecastResponse(forecast));
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message);
        }
    }
}
