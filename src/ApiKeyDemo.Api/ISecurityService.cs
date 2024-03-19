namespace ApiKeyDemo.Api;

public interface ISecurityService
{
    bool IsAuthorized(string? secretKey, string? apiKey);
}
