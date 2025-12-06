using ExchangeHub.Components;
using ExchangeHub.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>(client =>
{
    var baseUrl = builder.Configuration["ExchangeRateApi:BaseUrl"] ?? throw new InvalidOperationException("Missing ExchangeRateApi:BaseUrl");
    var apiKey = builder.Configuration["exchangerateapi-api-key"] ?? throw new InvalidOperationException("Missing exchangerateapi-api-key");
    client.BaseAddress = new Uri($"{baseUrl.TrimEnd('/')}/{apiKey}/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
