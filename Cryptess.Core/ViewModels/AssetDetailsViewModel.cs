using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Cryptess.Core.ViewModels
{
    public class AssetDetailsViewModel : MvxViewModel<SimpleAsset>
    {
        private readonly IMvxNavigationService _navService;
        private readonly IAssetRepository _assetRepo;

        public AssetDetailsViewModel(IMvxNavigationService mvxNavigationService, IAssetRepository assetRepo)
        {
            CloseCommand = new MvxCommand(async () => await Close());
            _navService = mvxNavigationService;
            _assetRepo = assetRepo;
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
        public override async Task Initialize()
        {
            await base.Initialize();
            Asset = _assetRepo.GetAssetById(_simpleAsset.AssetId);
        }

        private async Task Close()
        {
            await _navService.Close(this);
        }
    }
}
