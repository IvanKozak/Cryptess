using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Cryptess.Core.ViewModels
{
    public class CurrOverviewViewModel : MvxViewModel
    {
        private readonly IAssetRepository _assetRepo;
        private readonly IMvxNavigationService _navService;

        private ObservableCollection<SimpleAsset> _assets = new ObservableCollection<SimpleAsset>();

        private string _searchText;

        private SimpleAsset _selectedAsset;

        public CurrOverviewViewModel(IAssetRepository assetRepo, IMvxNavigationService navService)
        {
            _assetRepo = assetRepo;
            _navService = navService;
            ViewAssetDetailsCommand = new MvxCommand(async () => await ShowAssetDetails());
            ShowAssetConverterCommand = new MvxCommand(async () => await ShowAssetConverter());
        }

        public IMvxCommand ViewAssetDetailsCommand   { get; set; }
        public IMvxCommand ShowAssetConverterCommand { get; set; }

        public ObservableCollection<SimpleAsset> Assets
        {
            get => _assets;
            set => SetProperty(ref _assets, value);
        }

        public IEnumerable<SimpleAsset> SearchResults
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchText)) return null;
                return Assets.Where(a =>
                    a.Name.ToUpper().StartsWith(SearchText.ToUpper()) ||
                    a.AssetId.ToUpper().StartsWith(SearchText.ToUpper()));
            }
        }

        public SimpleAsset SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                SetProperty(ref _selectedAsset, value);
                RaisePropertyChanged(() => CanViewAssetDetails);
            }
        }

        public bool CanViewAssetDetails => SelectedAsset != null;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                RaisePropertyChanged(() => SearchResults);
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Assets = _assetRepo.GetAssetsOverview();
        }


        private async Task ShowAssetDetails()
        {
            await _navService.Navigate<AssetDetailsViewModel, SimpleAsset>(SelectedAsset);
        }

        private async Task ShowAssetConverter()
        {
            await _navService.Navigate<AssetConverterViewModel, ObservableCollection<SimpleAsset>>(Assets);
        }
    }
}