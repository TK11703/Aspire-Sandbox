using AspireAppExample.Core.Dtos;
using AspireAppExample.Core.Entities;
namespace AspireAppExample.Web.Clients;

public class WeatherApiClient(HttpClient httpClient, ILogger<WeatherApiClient> logger)
{
    public async Task<WeatherForecast[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {        
        List<WeatherForecast>? forecasts = null;

        using HttpResponseMessage response = await httpClient.GetAsync("forecast", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogInformation("Weather forecast request failed with status code {StatusCode}.", response.StatusCode);
            return Array.Empty<WeatherForecast>();
        }
        else
        {
            WeatherForecastResponse? forecastResponse = await response.Content.ReadFromJsonAsync<WeatherForecastResponse>(cancellationToken: cancellationToken);

            if (forecastResponse == null)
            {
                logger.LogInformation("Weather forecast response was empty.");
            }
            else if (forecastResponse.forecasts == null || forecastResponse.forecasts.Length.Equals(0))
            {
                logger.LogInformation("The forecast request did not return any forecasted items.");
            }
            else
            {
                foreach (WeatherForecast forecast in forecastResponse.forecasts)
                {
                    if (forecasts?.Count >= maxItems)
                    {
                        break;
                    }
                    if (forecast is not null)
                    {
                        forecasts ??= [];
                        forecasts.Add(forecast);
                    }
                }
            }
        }

        return forecasts?.ToArray() ?? [];
    }
}
