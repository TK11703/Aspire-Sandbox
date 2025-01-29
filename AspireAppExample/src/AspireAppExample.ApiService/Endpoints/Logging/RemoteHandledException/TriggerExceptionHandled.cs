using Microsoft.AspNetCore.Http.HttpResults;

namespace AspireAppExample.ApiService.Endpoints.Logging.RemoteUnhandledException;

public class TriggerExceptionHandled : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"/logging/remote/exception/handled", Handle)
            .WithName("TriggerRemoteExceptionHandled");
    }

    protected virtual Results<Ok, ProblemHttpResult, BadRequest> Handle(CancellationToken cancellationToken)
    {
        try
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
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }

}
