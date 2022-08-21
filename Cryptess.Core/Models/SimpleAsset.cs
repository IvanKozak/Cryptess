using System.Text.Json.Serialization;

namespace Cryptess.Core.Models
{
    public class SimpleAsset
    {
        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
