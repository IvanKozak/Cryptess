using Cryptess.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Cryptess.WPF.Views
{
    /// <summary>
    /// Interaction logic for AssetDetailsView.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(AssetDetailsViewModel))]
    public partial class AssetDetailsView : MvxWpfView
    {
        public AssetDetailsView()
        {
            InitializeComponent();
        }
    }
}
