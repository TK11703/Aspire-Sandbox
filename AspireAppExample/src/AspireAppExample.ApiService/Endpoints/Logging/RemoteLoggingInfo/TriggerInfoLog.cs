using AspireAppExample.ApiService.Endpoints.Logging.RemoteLoggingWarning;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AspireAppExample.ApiService.Endpoints.Logging.RemoteLoggingInfo;

public class TriggerInfoLog(ILogger<TriggerInfoLog> logger) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"/logging/remote/info", Handle)
            .WithName("TriggerRemoteInfo");
    }
    protected virtual Results<Ok, ProblemHttpResult, NotFound> Handle(CancellationToken cancellationToken)
    {
        logger.LogInformation("This is an info log message.");
        return TypedResults.Ok();
    }
}
