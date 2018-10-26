using Android.Net;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Interfaces;

namespace BlauwtipjeApp.Droid.Helpers
{
    public class NativeServiceFactory : INativeServiceFactory
    {
        private ConnectivityManager connectivityManager;

        public NativeServiceFactory(ConnectivityManager connectivityManager)
        {
            this.connectivityManager = connectivityManager;
        }

        private IFileHelper fileHelper;
        public IFileHelper GetFileHelper()
        {
            if (fileHelper == null)
            {
                fileHelper = new FileHelper();
            }
            return fileHelper;
        }

        private INetworkHelper networkHelper;
        public INetworkHelper GetNetworkHelper()
        {
            if (networkHelper == null)
            {
                networkHelper = new NetworkHelper(connectivityManager);
            }
            return networkHelper;
        }

        private IAzureResourceDAO azureResourceDatabase;
        public IAzureResourceDAO GetAzureResourceDatabase()
        {
            if (azureResourceDatabase == null)
            {
                azureResourceDatabase = new AzureResourceDatabase();
            }
            return azureResourceDatabase;
        }
    }
}