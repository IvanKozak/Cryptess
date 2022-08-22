using Cryptess.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cryptess.Core.Repositories
{
    public class CryptingUpAssetRepository : IAssetRepository
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly IConfiguration _config;

        public CryptingUpAssetRepository(IConfiguration config)
        {
            _config = config;
        }

        public Asset GetAssetById(string assetId)
        {
            string requestUri = $"https://cryptingup.com/api/assets/{assetId}";
            var task = Task.Run(() =>
            {
                return _client.GetStringAsync(requestUri);
            });
            task.Wait();
            var jsonString = task.Result;
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                return document.RootElement.GetProperty("asset").Deserialize<Asset>();
            }
        }

        public async Task<Asset> GetAssetByIdAsync(string assetId)
        {
            string requestUri = $"https://cryptingup.com/api/assets/{assetId}";
            var jsonString = await _client.GetStringAsync(requestUri);
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                return document.RootElement.GetProperty("asset").Deserialize<Asset>();
            }
        }

        public ObservableCollection<Asset> GetAssets()
        {
            int size;
            var sizeString = _config["CryptingUp:AssetsSize"];
            if (int.TryParse(sizeString, out size) == false)
            {
                size = 10;
            }
            string requestUri = $"https://cryptingup.com/api/assets?size={size}";
            var task = Task.Run(() =>
            {
                return _client.GetStringAsync(requestUri);
            });
            task.Wait();
            var jsonString = task.Result;
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                JsonElement assetsElement = root.GetProperty("assets");
                return assetsElement.Deserialize<ObservableCollection<Asset>>();
            }
        }

        public async Task<ObservableCollection<Asset>> GetAssetsAsync()
        {
            int size;
            var sizeString = _config["CryptingUp:AssetsSize"];
            if (int.TryParse(sizeString, out size) == false)
            {
                size = 10;
            }

            string requestUri = $"https://cryptingup.com/api/assets?size={size}";
            var jsonString = await _client.GetStringAsync(requestUri);
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                JsonElement assetsElement = root.GetProperty("assets");
                return assetsElement.Deserialize<ObservableCollection<Asset>>();
            }
        }

        public ObservableCollection<SimpleAsset> GetAssetsOverview()
        {
            int size;
            var sizeString = _config["CryptingUp:AssetsSize"];
            if (int.TryParse(sizeString, out size) == false)
            {
                size = 10;
            }
            var requestUri = $"https://cryptingup.com/api/assets?size={size}";
            var task = Task.Run(() => _client.GetStringAsync(requestUri));
            task.Wait();
            var jsonString = task.Result;
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                JsonElement assetsElement = root.GetProperty("assets");
                return assetsElement.Deserialize<ObservableCollection<SimpleAsset>>();
            }

        }

        public async Task<ObservableCollection<SimpleAsset>> GetAssetsOverviewAsync()
        {
            int size;
            var sizeString = _config["CryptingUp:AssetsSize"];
            if (int.TryParse(sizeString, out size) == false)
            {
                size = 10;
            }
            var url = $"https://cryptingup.com/api/assets?size={size}";
            var jsonString = await _client.GetStringAsync(url);
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                JsonElement assetsElement = root.GetProperty("assets");
                return assetsElement.Deserialize<ObservableCollection<SimpleAsset>>();
            }
        }
    }
}
