namespace Cryptess.Core.Services
{
    public interface IExchangeUrlService
    {
        string GetUrl(string exchangeId, string assetId);
    }
}