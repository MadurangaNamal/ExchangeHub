using ExchangeHub.Models;

namespace ExchangeHub.Services;

public interface IExchangeRateService
{
    Task<ExchangeRateStandardResponse?> GetLatestRatesAsync(string baseCurrency);
}
