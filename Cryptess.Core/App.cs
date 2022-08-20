using Cryptess.Core.ViewModels;
using MvvmCross.ViewModels;

namespace Cryptess.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<CurrOverviewViewModel>();
        }

    }
}
