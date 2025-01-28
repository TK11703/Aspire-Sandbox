using AspireAppExample.Web.Clients;

namespace AspireAppExample.Web.Extensions;

internal static class ClientConfigs
{
    /// <summary>
    /// Helps configure these TYPED clients (which are transient) to be used in potential singleton services and not be affected by DNS refresh issues and port exhaustion. Otherwise 
    /// NAMED clients could be used instead with the HttpClientFactory usage.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    public static void ConfigureClients(this IServiceCollection services, ConfigurationManager config)
    {
        //Aspire will handle the translation of the APISERVICE to the correct URL for the API registered for it.
        Uri baseUri = new("https+http://apiservice");

        services.AddHttpClient<WeatherApiClient>(client =>
        {
            // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
            // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
            client.BaseAddress = new(baseUri, "/weather/");
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };
        })
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);


    }
}
