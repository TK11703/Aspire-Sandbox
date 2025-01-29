using AspireAppExample.Core.Entities;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;

namespace AspireAppExample.Web.Clients;

public class LoggingApiClient(HttpClient httpClient, ILogger<LoggingApiClient> logger)
{
    public async Task<string> CheckApiHealth(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("/_health");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return "Unhealthy";
    }

    public async Task TriggerRemoteExceptionUnhandled(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"remote/exception/unhandled");
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Throw Exception (Remote/Unhandled) Request DID NOT SUCCEED.{Newline}Http Message: {Content}", Environment.NewLine, await response.Content.ReadAsStringAsync());            
        }
        logger.LogInformation("Throw Exception (Remote/Unhandled) Request COMPLETED successfully.");
    }

    public async Task TriggerRemoteExceptionHandled(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"remote/exception/handled");
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Throw Exception (Remote/Handled) Request DID NOT SUCCEED.{Newline}Http Message: {Content}", Environment.NewLine, await response.Content.ReadAsStringAsync());
        }
        logger.LogInformation("Throw Exception (Remote/Handled) Request COMPLETED successfully.");
    }

    public async Task TriggerRemoteError(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"remote/error");
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Log Error (Remote) Request DID NOT SUCCEED.{Newline}Http Message: {Content}", Environment.NewLine, await response.Content.ReadAsStringAsync());
        }
        logger.LogInformation("Log Error (Remote) Request COMPLETED successfully.");
    }

    public async Task TriggerRemoteWarning(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"remote/warning");
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Log Warning (Remote) Request DID NOT SUCCEED.{Newline}Http Message: {Content}", Environment.NewLine, await response.Content.ReadAsStringAsync());
        }
        logger.LogInformation("Log Warning (Remote) Request COMPLETED successfully.");
    }

    public async Task TriggerRemoteInformation(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"remote/info");
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Log Information (Remote) Request DID NOT SUCCEED.{Newline}Http Message: {Content}", Environment.NewLine, await response.Content.ReadAsStringAsync());
        }
        logger.LogInformation("Log Information (Remote) Request COMPLETED successfully.");
    }
}
