using System.Text.Json.Serialization;

namespace ExchangeHub.Models;

public class ExchangeRateStandardResponse
{
    [JsonPropertyName("result")]
    public string Result { get; set; } = string.Empty;
    
    [JsonPropertyName("base_code")]
    public string BaseCode { get; set; } = string.Empty;
    
    [JsonPropertyName("conversion_rates")]
    public Dictionary<string, decimal> ConversionRates { get; set; } = [];
    
    [JsonPropertyName("time_last_update_unix")]
    public long TimeLastUpdateUnix { get; set; }
    
    [JsonPropertyName("time_next_update_unix")]
    public long TimeNextUpdateUnix { get; set; }
}
