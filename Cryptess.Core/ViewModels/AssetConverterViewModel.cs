using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using Microsoft.Extensions.Configuration;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Cryptess.Core.ViewModels
{
    public class AssetConverterViewModel : MvxViewModel<ObservableCollection<SimpleAsset>>
    {
        private readonly IConfiguration _config;
        private readonly IMarketRepository _marketRepo;
        private readonly IMvxNavigationService _navService;


        private string _baseSearchText;

        private ObservableCollection<Market> _markets;

        private string _quoteSearchText;

        private SimpleAsset _selectedBaseAsset;

        private SimpleAsset _selectedQuoteAsset;

        private ObservableCollection<SimpleAsset> _simpleAssets;

        public AssetConverterViewModel(IMvxNavigationService navService, IMarketRepository marketRepo,
            IConfiguration config)
        {
            _navService = navService;
            _marketRepo = marketRepo;
            _config = config;
            CloseCommand = new MvxCommand(async () => await Close());
        }

        public IMvxCommand CloseCommand { get; set; }

        public ObservableCollection<Market> Markets
        {
            get => _markets;
            set => SetProperty(ref _markets, value);
        }

        public SimpleAsset SelectedBaseAsset
        {
            get => _selectedBaseAsset;
            set
            {
                SetProperty(ref _selectedBaseAsset, value);
                _baseSearchText = SelectedBaseAsset?.Name;
                RaisePropertyChanged(() => BaseSearchText);
                RaisePropertyChanged(() => ConversionResult);
            }
        }

        public SimpleAsset SelectedQuoteAsset
        {
            get => _selectedQuoteAsset;
            set
            {
                SetProperty(ref _selectedQuoteAsset, value);
                _quoteSearchText = SelectedQuoteAsset?.Name;
                RaisePropertyChanged(() => QuoteSearchText);
                RaisePropertyChanged(() => ConversionResult);
            }
        }

        public string BaseSearchText
        {
            get => _baseSearchText;
            set
            {
                SetProperty(ref _baseSearchText, value);
                RaisePropertyChanged(() => BaseSearchResults);
            }
        }

        public string QuoteSearchText
        {
            get => _quoteSearchText;
            set
            {
                SetProperty(ref _quoteSearchText, value);
                RaisePropertyChanged(() => QuoteSearchResults);
            }
        }

        public IEnumerable<SimpleAsset> BaseSearchResults
        {
            get
            {
                if (string.IsNullOrWhiteSpace(BaseSearchText)) return null;
                return _simpleAssets.Where(a =>
                    a.Name.ToUpper().StartsWith(BaseSearchText.ToUpper()) ||
                    a.AssetId.ToUpper().StartsWith(BaseSearchText.ToUpper()));
            }
        }

        public IEnumerable<SimpleAsset> QuoteSearchResults
        {
            get
            {
                if (string.IsNullOrWhiteSpace(QuoteSearchText)) return null;
                return _simpleAssets.Where(a =>
                    a.Name.ToUpper().StartsWith(QuoteSearchText.ToUpper()) ||
                    a.AssetId.ToUpper().StartsWith(QuoteSearchText.ToUpper()));
            }
        }

        public string ConversionResult
        {
            get
            {
                if (SelectedBaseAsset != null && SelectedQuoteAsset != null)
                {
                    var market = Markets.Where(m =>
                        m.BaseAsset.Equals(SelectedBaseAsset.AssetId) &&
                        m.QuoteAsset.Equals(SelectedQuoteAsset.AssetId)).FirstOrDefault();
                    if (market == null)
                        return $"Found no market for this transaction on {_config["AssetConverter:Exchange"]}";
                    return $"One {SelectedBaseAsset.Name} is currently worth {market.Price} {SelectedQuoteAsset.Name}.";
                }

                return null;
            }
        }

        public override void Prepare(ObservableCollection<SimpleAsset> parameter)
        {
            _simpleAssets = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            Markets = await _marketRepo.GetMarketsByExchangeIdAsync(_config["AssetConverter:Exchange"]);
        }

        private async Task Close()
        {
            await _navService.Close(this);
        }
    }
}