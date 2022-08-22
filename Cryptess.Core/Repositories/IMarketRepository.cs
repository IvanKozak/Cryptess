using Cryptess.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cryptess.Core.Repositories
{
    public interface IMarketRepository
    {
        Task<ObservableCollection<Market>> GetMarketsByAssetIdAsync(string assetId);
    }
}