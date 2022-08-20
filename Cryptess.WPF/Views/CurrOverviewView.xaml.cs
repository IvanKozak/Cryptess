using Cryptess.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Cryptess.WPF.Views
{
    /// <summary>
    /// Interaction logic for CurrOverviewView.xaml
    /// </summary>
    [MvxContentPresentation]
    [MvxViewFor(typeof(CurrOverviewViewModel))]
    public partial class CurrOverviewView : MvxWpfView
    {
        public CurrOverviewView()
        {
            InitializeComponent();
        }
    }
}
