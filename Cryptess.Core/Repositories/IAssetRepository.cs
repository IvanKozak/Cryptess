using Cryptess.Core.Models;
using System.Collections.ObjectModel;

namespace Cryptess.Core.Repositories
{
    public interface IAssetRepository
    {
        ObservableCollection<Asset> GetAssets();
        ObservableCollection<SimpleAsset> GetAssetsOverview();
    }
}