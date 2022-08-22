using Cryptess.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cryptess.Core.Repositories
{
    public interface IAssetRepository
    {
        ObservableCollection<Asset> GetAssets();
        Task<ObservableCollection<Asset>> GetAssetsAsync();
        ObservableCollection<SimpleAsset> GetAssetsOverview();
        Task<ObservableCollection<SimpleAsset>> GetAssetsOverviewAsync();
        Asset GetAssetById(string assetId);
        Task<Asset> GetAssetByIdAsync(string assetId);

    }
}