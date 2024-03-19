using ApiKeyDemo.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ISecurityService, SecurityService>();

var app = builder.Build();


app.UseMiddleware<SecurityApiMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
