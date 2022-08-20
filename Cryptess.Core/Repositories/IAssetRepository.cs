using Cryptess.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cryptess.Core.Repositories
{
    public interface IAssetRepository
    {
        Task<List<Asset>> GetAssets();
    }
}