using AspireAppExample.Web.Clients;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspireAppExample.Web.Health;

public class ApiHealthCheck (LoggingApiClient client) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await client.CheckApiHealth(cancellationToken);
            if (response == "Healthy")
            {
                return HealthCheckResult.Healthy("The API is healthy.");
            }

            return HealthCheckResult.Unhealthy("The API is not healthy.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("The API is not reachable.", ex);
        }
    }
}
