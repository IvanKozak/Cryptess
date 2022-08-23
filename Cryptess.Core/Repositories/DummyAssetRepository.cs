using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Cryptess.Core.Models;
using Microsoft.Extensions.Configuration;

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
            var path = _config["DummyAssetsPath"];
            using (var stream = File.OpenRead(path))
            {
                using (var document = JsonDocument.Parse(stream))
                {
                    var root = document.RootElement;
                    var assetsElement = root.GetProperty("assets");
                    foreach (var asset in assetsElement.EnumerateArray())
                        if (asset.GetProperty("asset_id").ToString() == assetId)
                            return asset.Deserialize<Asset>();
                    return null;
                }
            }
        }

        public async Task<Asset> GetAssetByIdAsync(string assetId)
        {
            var path = _config["DummyAssetsPath"];
            using (var stream = File.OpenRead(path))
            {
                using (var document = await JsonDocument.ParseAsync(stream))
                {
                    var root = document.RootElement;
                    var assetsElement = root.GetProperty("assets");
                    foreach (var asset in assetsElement.EnumerateArray())
                        if (asset.GetProperty("asset_id").ToString() == assetId)
                            return asset.Deserialize<Asset>();
                    return null;
                }
            }
        }

        public ObservableCollection<Asset> GetAssets()
        {
            var path = _config["DummyAssetsPath"];
            using (var stream = File.OpenRead(path))
            {
                using (var document = JsonDocument.Parse(stream))
                {
                    var root = document.RootElement;
                    var assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<Asset>>();
                }
            }
        }

        public async Task<ObservableCollection<Asset>> GetAssetsAsync()
        {
            var path = _config["DummyAssetsPath"];
            using (var stream = File.OpenRead(path))
            {
                using (var document = await JsonDocument.ParseAsync(stream))
                {
                    var root = document.RootElement;
                    var assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<Asset>>();
                }
            }
        }

        public ObservableCollection<SimpleAsset> GetAssetsOverview()
        {
            var filePath = _config["DummyAssetsPath"];
            using (var sourceStream = File.OpenRead(filePath))
            {
                using (var document = JsonDocument.Parse(sourceStream))
                {
                    var root = document.RootElement;
                    var assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<SimpleAsset>>();
                }
            }
        }

        public async Task<ObservableCollection<SimpleAsset>> GetAssetsOverviewAsync()
        {
            var filePath = _config["DummyAssetsPath"];
            using (var sourceStream = File.OpenRead(filePath))
            {
                using (var document = await JsonDocument.ParseAsync(sourceStream))
                {
                    var root = document.RootElement;
                    var assetsElement = root.GetProperty("assets");
                    return assetsElement.Deserialize<ObservableCollection<SimpleAsset>>();
                }
            }
        }
    }
}