using WebMVC.Constants;

namespace WebMVC.Middleware;

public class JwtCookieMiddleware
{
    private readonly RequestDelegate next;

    public JwtCookieMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        if (request.Cookies.TryGetValue(Jwt.TokenCookieKey, out var token))
        {
            request.Headers["Authorization"] = "Bearer " + token;
        }

        return next(context);
    }
}
