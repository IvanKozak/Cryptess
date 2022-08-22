using Cryptess.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cryptess.Core.Repositories
{
    public class DummyAssetRepository : IAssetRepository
    {
        private readonly IConfiguration _config;

        public DummyAssetRepository(IConfiguration config)
        {
            _config = config;
        }

        public Asset GetAssetById(string assetId)
        {
            string path = _config["DummyAssetsPath"];
            using (FileStream stream = File.OpenRead(path))
            {
                using (JsonDocument document = JsonDocument.Parse(stream))
                {
                    JsonElement root = document.RootElement;
                    JsonElement assetsElement = root.GetProperty("assets");
                    foreach (var asset in assetsElement.EnumerateArray())
                    {
                        if (asset.GetProperty("asset_id").ToString() == assetId)
                        {
                            return asset.Deserialize<Asset>();
                        }
                    }
                    return null;
                }
            }
        }

        public async Task<Asset> GetAssetByIdAsync(string assetId)
        {
            string path = _config["DummyAssetsPath"];
            using (FileStream stream = File.OpenRead(path))
            {
                using (JsonDocument document = await JsonDocument.ParseAsync(stream))
                {
                    JsonElement root = document.RootElement;
                    JsonElement assetsElement = root.GetProperty("assets");
                    foreach (var asset in assetsElement.EnumerateArray())
                    {
                        if (asset.GetProperty("asset_id").ToString() == assetId)
                        {
                            return asset.Deserialize<Asset>();
                        }
                    }
                    return null;
                }
            }
        }

        public ObservableCollection<Asset> GetAssets()
        {
            string path = _config["DummyAssetsPath"];
            using (FileStream stream = File.OpenRead(path))
            {
                using (JsonDocument document = JsonDocument.Parse(stream))
                {
                    JsonElement root = document.RootElement;
                    JsonElement assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<Asset>>();
                }
            }
        }

        public async Task<ObservableCollection<Asset>> GetAssetsAsync()
        {
            string path = _config["DummyAssetsPath"];
            using (FileStream stream = File.OpenRead(path))
            {
                using (JsonDocument document = await JsonDocument.ParseAsync(stream))
                {
                    JsonElement root = document.RootElement;
                    JsonElement assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<Asset>>();
                }
            }
        }

        public ObservableCollection<SimpleAsset> GetAssetsOverview()
        {
            string filePath = _config["DummyAssetsPath"];
            using (var sourceStream = File.OpenRead(filePath))
            {
                using (JsonDocument document = JsonDocument.Parse(sourceStream))
                {
                    JsonElement root = document.RootElement;
                    JsonElement assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<SimpleAsset>>();
                }
            }
        }

        public async Task<ObservableCollection<SimpleAsset>> GetAssetsOverviewAsync()
        {
            string filePath = _config["DummyAssetsPath"];
            using (var sourceStream = File.OpenRead(filePath))
            {
                using (JsonDocument document = await JsonDocument.ParseAsync(sourceStream))
                {
                    JsonElement root = document.RootElement;
                    JsonElement assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<SimpleAsset>>();
                }
            }
        }
    }
}
