using Cryptess.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cryptess.Core.Repositories
{
    public class DummyAssetRepository : IAssetRepository
    {
        public async Task<List<Asset>> GetAssets()
        {
            string fileName = "AssetSamples.json";
            using (FileStream stream = File.OpenRead(fileName))
            {
                return await JsonSerializer.DeserializeAsync<List<Asset>>(stream);
            }

        }
    }
}
