using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using Cryptess.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cryptess.Core.ViewModels
{
    public class AssetDetailsViewModel : MvxViewModel<SimpleAsset>
    {
        private readonly IMvxNavigationService _navService;
        private readonly IAssetRepository _assetRepo;
        private readonly IMarketRepository _marketRepo;
        private readonly IExchangeUrlService _urlService;
        private string _exchangeUrl;

        public AssetDetailsViewModel(IMvxNavigationService mvxNavigationService,
            IAssetRepository assetRepo, IMarketRepository marketRepo, IExchangeUrlService urlService)
        {
            CloseCommand = new MvxCommand(async () => await Close());
            OpenExchangeLinkCommand = new MvxCommand(() => OpenExchangeLink());
            _navService = mvxNavigationService;
            _assetRepo = assetRepo;
            _marketRepo = marketRepo;
            _urlService = urlService;
        }
        public IMvxCommand CloseCommand { get; set; }

        public IMvxCommand OpenExchangeLinkCommand { get; set; }

        private SimpleAsset _simpleAsset;
        public override void Prepare(SimpleAsset parameter)
        {
            _simpleAsset = parameter;
        }
        private Asset _asset;
        public Asset Asset
        {
            get { return _asset; }
            set
            {
                SetProperty(ref _asset, value);
            }
        }
        private ObservableCollection<Market> _markets;

        public ObservableCollection<Market> Markets
        {
            get { return _markets; }
            set
            {
                SetProperty(ref _markets, value);
            }
        }

        private Market _selectedMarket;

        public Market SelectedMarket
        {
            get { return _selectedMarket; }
            set
            {
                SetProperty(ref _selectedMarket, value);
                _exchangeUrl = _urlService.GetUrl(SelectedMarket.ExchangeId, SelectedMarket.BaseAsset);
                RaisePropertyChanged(() => CanVisitExchangeLink);
            }
        }

        public bool CanVisitExchangeLink
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_exchangeUrl))
                {
                    return false;
                }
                return true;
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Asset = await _assetRepo.GetAssetByIdAsync(_simpleAsset.AssetId);
            Markets = await _marketRepo.GetMarketsByAssetIdAsync(_simpleAsset.AssetId);
        }

        private async Task Close()
        {
            await _navService.Close(this);
        }

        private void OpenExchangeLink()
        {
            var sInfo = new System.Diagnostics.ProcessStartInfo(_exchangeUrl)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }
    }
}
