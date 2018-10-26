using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using Splat;

namespace BlauwtipjeApp.Core.Helpers
{
    public static class ServiceLocator
    {
        public static void InitializeServices<TResult>(INativeServiceFactory nativeServiceFactory) where TResult : Result
        {
            var coreServiceFactory = new CoreServiceFactory<TResult>(
                nativeServiceFactory.GetFileHelper(),
                nativeServiceFactory.GetAzureResourceDatabase());

            // Register native services
            Locator.CurrentMutable.Register(nativeServiceFactory.GetFileHelper);
            Locator.CurrentMutable.Register(nativeServiceFactory.GetNetworkHelper);
            Locator.CurrentMutable.Register(nativeServiceFactory.GetAzureResourceDatabase);

            // Register core services
            Locator.CurrentMutable.Register(coreServiceFactory.GetLocalResourceDatabase);
            Locator.CurrentMutable.Register(coreServiceFactory.GetDeterminationInProgressDAO);
            Locator.CurrentMutable.Register(coreServiceFactory.GetTreeFactory);
            Locator.CurrentMutable.Register(coreServiceFactory.GetUpdateService);
            Locator.CurrentMutable.Register(coreServiceFactory.GetPresenterFactory);
        }

        public static TService GetService<TService>()
        {
            return Locator.Current.GetService<TService>();
        }
    }
}

