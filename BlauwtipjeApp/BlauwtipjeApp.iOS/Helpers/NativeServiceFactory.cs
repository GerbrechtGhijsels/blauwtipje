using System;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Interfaces;

namespace BlauwtipjeApp.iOS.Helpers
{
    public class NativeServiceFactory : INativeServiceFactory
    {
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
                networkHelper = new NetworkHelper();
            }
            return networkHelper;
        }

        private IAzureResourceDAO azureResourceDatabase;
        public IAzureResourceDAO GetAzureResourceDatabase()
        {
            if (azureResourceDatabase == null)
            {
                azureResourceDatabase = new WebResourceDatabase();
            }
            return azureResourceDatabase;
        }
    }
}
