using ExchangeHub.Components;
using ExchangeHub.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<ExchangeRateService>(client =>
{
    var baseUrl = builder.Configuration["ExchangeRateApi:BaseUrl"];
    var apiKey = builder.Configuration["exchangerateapi-api-key"];

    client.BaseAddress = new Uri($"{baseUrl}/{apiKey}/");
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

app.Run();
