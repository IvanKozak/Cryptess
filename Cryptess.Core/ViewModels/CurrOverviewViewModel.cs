using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cryptess.Core.ViewModels
{
    public class CurrOverviewViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navService;
        private readonly IAssetRepository _assetRepo;

        public CurrOverviewViewModel(IAssetRepository assetRepo, IMvxNavigationService navService)
        {
            _assetRepo = assetRepo;
            _navService = navService;
            ViewAssetDetailsCommand = new MvxCommand(async () => await ShowAssetDetails());
        }
        public IMvxCommand ViewAssetDetailsCommand { get; set; }

        private ObservableCollection<SimpleAsset> _assets = new ObservableCollection<SimpleAsset>();

        public ObservableCollection<SimpleAsset> Assets
        {
            get { return _assets; }
            set
            {
                SetProperty(ref _assets, value);
            }
        }
        public override async Task Initialize()
        {
            await base.Initialize();
            Assets = _assetRepo.GetAssetsOverview();
        }

        private SimpleAsset _selectedAsset;

        public SimpleAsset SelectedAsset
        {
            get { return _selectedAsset; }
            set
            {
                SetProperty(ref _selectedAsset, value);
                RaisePropertyChanged(() => CanViewAssetDetails);
            }
        }
        public bool CanViewAssetDetails => SelectedAsset != null;

        private async Task ShowAssetDetails()
        {
            await _navService.Navigate<AssetDetailsViewModel, SimpleAsset>(SelectedAsset);
        }
    }
}
