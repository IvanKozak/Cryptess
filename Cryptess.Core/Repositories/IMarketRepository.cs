using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cryptess.Core.Models;

namespace Cryptess.Core.Repositories
{
    public interface IMarketRepository
    {
        Task<ObservableCollection<Market>> GetMarketsByAssetIdAsync(string assetId);
        Task<ObservableCollection<Market>> GetMarketsByExchangeIdAsync(string exchangeId);
    }
}