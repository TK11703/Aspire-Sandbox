using Microsoft.AspNetCore.Http.HttpResults;

namespace AspireAppExample.ApiService.Endpoints.Logging.RemoteUnhandledException;

public class TriggerExceptionUnhandled : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"/logging/remote/exception/unhandled", Handle)
            .WithName("TriggerRemoteExceptionUnhandled");
    }

    protected virtual Results<Ok, ProblemHttpResult, BadRequest> Handle(CancellationToken cancellationToken)
    {
        int result = 0;
        //Will trigger a divide by zero exception and it is not handled by design for logging purposes.
        result = 1 / result;
        if (result > 0)
        {
            return TypedResults.Ok();
        }
        return TypedResults.BadRequest();
    }
}
