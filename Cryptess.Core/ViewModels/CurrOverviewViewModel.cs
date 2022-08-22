using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public IEnumerable<SimpleAsset> SearchResults
        {
            get
            {
                if (String.IsNullOrWhiteSpace(SearchText))
                {
                    return null;
                }
                return Assets.Where(a => a.Name.ToUpper().StartsWith(SearchText.ToUpper()) || a.AssetId.ToUpper().StartsWith(SearchText.ToUpper()));
            }
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

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                RaisePropertyChanged(() => SearchResults);
            }
        }


        private async Task ShowAssetDetails()
        {
            await _navService.Navigate<AssetDetailsViewModel, SimpleAsset>(SelectedAsset);
        }
    }
}
