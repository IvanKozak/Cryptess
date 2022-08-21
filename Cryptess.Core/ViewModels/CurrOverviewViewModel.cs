using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cryptess.Core.ViewModels
{
    public class CurrOverviewViewModel : MvxViewModel
    {
        private readonly IAssetRepository _assetRepo;

        public CurrOverviewViewModel(IAssetRepository assetRepo)
        {
            _assetRepo = assetRepo;
        }

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
            _assets = _assetRepo.GetAssetsOverview();
        }
    }
}
