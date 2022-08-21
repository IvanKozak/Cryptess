using Cryptess.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Wpf.Core;
using Serilog;
using Serilog.Extensions.Logging;
using System.IO;

namespace Cryptess.WPF
{
    public class Setup : MvxWpfSetup<Core.App>
    {
        protected override ILoggerFactory? CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
        protected override ILoggerProvider? CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }
        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeFirstChance(iocProvider);
            Mvx.IoCProvider.RegisterSingleton<IConfiguration>(AddConfiguration());
            Mvx.IoCProvider.RegisterType<IAssetRepository, DummyAssetRepository>();
        }
        private IConfiguration AddConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
#if DEBUG
            configurationBuilder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
#else
            configurationBuilder.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
#endif
            return configurationBuilder.Build();
        }
    }
}
