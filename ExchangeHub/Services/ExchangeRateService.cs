using ExchangeHub.Models;
using System.Text.Json;

namespace ExchangeHub.Services;

public class ExchangeRateService : IExchangeRateService
{
    private readonly HttpClient _httpClient;

    public ExchangeRateService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ExchangeRateStandardResponse?> GetLatestRatesAsync(string baseCurrency)
    {
        var response = await _httpClient.GetAsync($"latest/{baseCurrency}");

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ExchangeRateStandardResponse>(content);
    }

    public async Task<decimal?> ConvertAsync(string fromCurrency, string toCurrency, decimal amount)
    {
        var rates = await GetLatestRatesAsync(fromCurrency);

        if (rates?.ConversionRates.TryGetValue(toCurrency, out var rate) == true)
            return amount * rate;

        return null;
    }
}
