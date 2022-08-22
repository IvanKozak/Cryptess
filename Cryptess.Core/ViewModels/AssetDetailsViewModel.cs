using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
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

        public AssetDetailsViewModel(IMvxNavigationService mvxNavigationService,
            IAssetRepository assetRepo, IMarketRepository marketRepo)
        {
            CloseCommand = new MvxCommand(async () => await Close());
            _navService = mvxNavigationService;
            _assetRepo = assetRepo;
            _marketRepo = marketRepo;
        }
        public IMvxCommand CloseCommand { get; set; }

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
    }
}
