using Microsoft.AspNetCore.Http.HttpResults;

namespace AspireAppExample.ApiService.Endpoints.Logging.RemoteLoggingWarning;

public class TriggerWarningLog(ILogger<TriggerWarningLog> logger) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"/logging/remote/warning", Handle)
            .WithName("TriggerRemoteWarning");
    }
    protected virtual Results<Ok, ProblemHttpResult, NotFound> Handle(CancellationToken cancellationToken)
    {
        logger.LogWarning("This is a warning log message.");
        return TypedResults.Ok();
    }
}
