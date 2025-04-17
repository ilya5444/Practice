namespace WebAPI.Middleware;

public class ApiTokenMiddleware
{
    private readonly RequestDelegate next;

    public ApiTokenMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;

        if (request.Headers.TryGetValue("Apitoken", out var token))
        {
            request.Headers["Authorization"] = "Bearer " + token.First();
        }

        await next(context);
    }
}
