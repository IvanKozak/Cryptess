using Cryptess.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Cryptess.WPF.Views
{
    /// <summary>
    /// Interaction logic for AssetConverterView.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(AssetConverterViewModel))]
    public partial class AssetConverterView : MvxWpfView
    {
        public AssetConverterView()
        {
            InitializeComponent();
        }
    }
}
