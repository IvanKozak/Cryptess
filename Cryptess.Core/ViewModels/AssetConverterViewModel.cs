using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using Microsoft.Extensions.Configuration;
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
    public class AssetConverterViewModel : MvxViewModel<ObservableCollection<SimpleAsset>>
    {
        private readonly IMvxNavigationService _navService;
        private readonly IMarketRepository _marketRepo;
        private readonly IConfiguration _config;

        public AssetConverterViewModel(IMvxNavigationService navService, IMarketRepository marketRepo, IConfiguration config)
        {
            _navService = navService;
            _marketRepo = marketRepo;
            _config = config;
            CloseCommand = new MvxCommand(async () => await Close());
        }
        public IMvxCommand CloseCommand { get; set; }

        private ObservableCollection<SimpleAsset> _simpleAssets;
        public override void Prepare(ObservableCollection<SimpleAsset> parameter)
        {
            _simpleAssets = parameter;
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

            Markets = await _marketRepo.GetMarketsByExchangeIdAsync(_config["AssetConverter:Exchange"]);
        }

        private SimpleAsset _selectedBaseAsset;

        public SimpleAsset SelectedBaseAsset
        {
            get { return _selectedBaseAsset; }
            set
            {
                SetProperty(ref _selectedBaseAsset, value);
                _baseSearchText = SelectedBaseAsset?.Name;
                RaisePropertyChanged(() => BaseSearchText);
                RaisePropertyChanged(() => ConversionResult);
            }
        }

        private SimpleAsset _selectedQuoteAsset;

        public SimpleAsset SelectedQuoteAsset
        {
            get { return _selectedQuoteAsset; }
            set
            {
                SetProperty(ref _selectedQuoteAsset, value);
                _quoteSearchText = SelectedQuoteAsset?.Name;
                RaisePropertyChanged(() => QuoteSearchText);
                RaisePropertyChanged(() => ConversionResult);
            }
        }


        private string _baseSearchText;

        public string BaseSearchText
        {
            get { return _baseSearchText; }
            set
            {
                SetProperty(ref _baseSearchText, value);
                RaisePropertyChanged(() => BaseSearchResults);
            }
        }

        private string _quoteSearchText;

        public string QuoteSearchText
        {
            get { return _quoteSearchText; }
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
                if (String.IsNullOrWhiteSpace(BaseSearchText))
                {
                    return null;
                }
                return _simpleAssets.Where(a => a.Name.ToUpper().StartsWith(BaseSearchText.ToUpper()) || a.AssetId.ToUpper().StartsWith(BaseSearchText.ToUpper()));
            }
        }

        public IEnumerable<SimpleAsset> QuoteSearchResults
        {
            get
            {
                if (String.IsNullOrWhiteSpace(QuoteSearchText))
                {
                    return null;
                }
                return _simpleAssets.Where(a => a.Name.ToUpper().StartsWith(QuoteSearchText.ToUpper()) || a.AssetId.ToUpper().StartsWith(QuoteSearchText.ToUpper()));
            }
        }

        public string ConversionResult
        {
            get
            {
                if (SelectedBaseAsset != null && SelectedQuoteAsset != null)
                {
                    var market = Markets.Where(m => m.BaseAsset.Equals(SelectedBaseAsset.AssetId) && m.QuoteAsset.Equals(SelectedQuoteAsset.AssetId)).FirstOrDefault();
                    if (market == null)
                    {
                        return $"Found no market for this transaction on {_config["AssetConverter:Exchange"]}";
                    }
                    return $"One {SelectedBaseAsset.Name} is currently worth {market.Price} {SelectedQuoteAsset.Name}.";
                }
                return null;
            }
        }

        private async Task Close()
        {
            await _navService.Close(this);
        }
    }
}
