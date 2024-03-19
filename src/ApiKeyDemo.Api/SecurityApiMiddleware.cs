namespace ApiKeyDemo.Api;

public class SecurityApiMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISecurityService _securityService;
    private const string APIKEY_HEADER = "X-ApiKey";
    private const string SECRET_HEADER = "X-SecretKey";

    public SecurityApiMiddleware(RequestDelegate next, ISecurityService securityService)
    {
        _next = next;
        _securityService = securityService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var apiKeyHeaderExists = context.Request.Headers.TryGetValue(APIKEY_HEADER, out var apiKeyValue);
        var secretHeaderExists = context.Request.Headers.TryGetValue(SECRET_HEADER, out var secretValue);
        if (!apiKeyHeaderExists || !secretHeaderExists)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Header API Key and secret key not found");
            return;
        }

        if (!_securityService.IsAuthorized(secretValue, apiKeyValue))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized client");
            return;
        }

        await _next(context);
    }
}
