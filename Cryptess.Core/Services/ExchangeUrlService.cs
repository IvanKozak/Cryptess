using System.Collections.Generic;
using System.Linq;
using Cryptess.Core.Repositories;

namespace Cryptess.Core.Services
{
    public class ExchangeUrlService : IExchangeUrlService
    {
        private readonly Dictionary<string, string> _assetIdAssetName;
        private readonly IAssetRepository _assetRepo;

        public ExchangeUrlService(IAssetRepository assetRepo)
        {
            _assetRepo = assetRepo;
            var assets = _assetRepo.GetAssetsOverview();
            _assetIdAssetName = assets.ToDictionary(x => x.AssetId, x => x.Name);
        }

        public string GetUrl(string exchangeId, string assetId)
        {
            string assetName;
            switch (exchangeId)
            {
                case "BINANCE":
                    assetName = _assetIdAssetName[assetId];
                    return $"https://www.binance.com/en/price/{assetName}";
                case "COINBASE":
                    assetName = _assetIdAssetName[assetId];
                    return $"https://www.coinbase.com/price/{assetName}";
                default:
                    return null;
            }
        }
    }
}