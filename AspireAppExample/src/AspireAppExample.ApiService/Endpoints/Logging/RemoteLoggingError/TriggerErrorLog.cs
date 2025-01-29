using AspireAppExample.ApiService.Endpoints.Logging.RemoteLoggingWarning;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AspireAppExample.ApiService.Endpoints.Logging.RemoteLoggingError;

public class TriggerErrorLog(ILogger<TriggerErrorLog> logger) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"/logging/remote/error", Handle)
            .WithName("TriggerRemoteError");
    }
    protected virtual Results<Ok, ProblemHttpResult, NotFound> Handle(CancellationToken cancellationToken)
    {
        logger.LogError("This is an error log message.");
        return TypedResults.Ok();
    }
}
