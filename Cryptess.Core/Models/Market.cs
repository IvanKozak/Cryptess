
using System;
using System.Text.Json.Serialization;

namespace Cryptess.Core.Models
{
    public class Market
    {
        [JsonPropertyName("exchange_id")]
        public string ExchangeId { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("base_asset")]
        public string BaseAsset { get; set; }
        [JsonPropertyName("quote_asset")]
        public string QuoteAsset { get; set; }
        [JsonPropertyName("price_unconverted")]
        public float PriceUnconverted { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("change_24h")]
        public float Change24h { get; set; }
        [JsonPropertyName("spread")]
        public float Spread { get; set; }
        [JsonPropertyName("volume_24h")]
        public float Volume24h { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}

