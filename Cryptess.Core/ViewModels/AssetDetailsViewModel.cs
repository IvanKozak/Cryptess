using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using Cryptess.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Cryptess.Core.ViewModels
{
    public class AssetDetailsViewModel : MvxViewModel<SimpleAsset>
    {
        private readonly IAssetRepository _assetRepo;
        private readonly IMarketRepository _marketRepo;
        private readonly IMvxNavigationService _navService;
        private readonly IExchangeUrlService _urlService;
        private Asset _asset;
        private string _exchangeUrl;
        private ObservableCollection<Market> _markets;

        private Market _selectedMarket;

        private SimpleAsset _simpleAsset;

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

        public Asset Asset
        {
            get => _asset;
            set => SetProperty(ref _asset, value);
        }

        public ObservableCollection<Market> Markets
        {
            get => _markets;
            set => SetProperty(ref _markets, value);
        }

        public Market SelectedMarket
        {
            get => _selectedMarket;
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
                if (string.IsNullOrWhiteSpace(_exchangeUrl)) return false;
                return true;
            }
        }

        public override void Prepare(SimpleAsset parameter)
        {
            _simpleAsset = parameter;
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
            var sInfo = new ProcessStartInfo(_exchangeUrl)
            {
                UseShellExecute = true
            };
            Process.Start(sInfo);
        }
    }
}