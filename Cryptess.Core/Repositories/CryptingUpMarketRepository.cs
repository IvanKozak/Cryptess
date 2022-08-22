using Cryptess.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cryptess.Core.Repositories
{
    public class CryptingUpMarketRepository : IMarketRepository
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly IConfiguration _config;

        public CryptingUpMarketRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ObservableCollection<Market>> GetMarketsByAssetIdAsync(string assetId)
        {
            var sizeString = _config["CryptingUp:MarketsSize"];
            if (int.TryParse(sizeString, out var size) == false)
            {
                size = 5;
            }
            var requestUri = $"https://cryptingup.com/api/assets/{assetId}/markets?size={size}";
            var jsonString = await _client.GetStringAsync(requestUri);
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                JsonElement marketsElement = root.GetProperty("markets");
                return marketsElement.Deserialize<ObservableCollection<Market>>();
            }
        }
    }
}
