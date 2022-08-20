using System;
using System.Text.Json.Serialization;

namespace Cryptess.Core.Models
{
    public class Asset
    {
        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("volume_24h")]
        public decimal Volume24h { get; set; }
        [JsonPropertyName("change_1h")]
        public decimal Change1h { get; set; }
        [JsonPropertyName("change_24h")]
        public decimal Change24h { get; set; }
        [JsonPropertyName("change_7d")]
        public decimal Change7d { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

    }
}
