using Cryptess.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Cryptess.Core.Repositories
{
    public class DummyAssetRepository : IAssetRepository
    {
        private readonly IConfiguration _config;

        public DummyAssetRepository(IConfiguration config)
        {
            _config = config;
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
    }
}
