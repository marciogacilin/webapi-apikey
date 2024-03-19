namespace ApiKeyDemo.Api;

public class SecurityService : ISecurityService
{
    private readonly IConfiguration _config;

    public SecurityService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public bool IsAuthorized(string? secretKey, string? apiKey)
    {
        var secretKeyValue = _config.GetValue<string>("ApiKeySettings:SecretKey");
        var apiKeyValue = _config.GetValue<string>("ApiKeySettings:ApiKey");

        if (secretKeyValue is null || apiKeyValue is null)
            return false;

        return secretKeyValue.Equals(secretKey) && apiKeyValue.Equals(apiKey);
    }
}
