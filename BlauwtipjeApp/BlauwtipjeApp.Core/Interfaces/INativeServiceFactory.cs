using BlauwtipjeApp.Core.DataAccess;

namespace BlauwtipjeApp.Core.Interfaces
{
    public interface INativeServiceFactory
    {
        IFileHelper GetFileHelper();
        INetworkHelper GetNetworkHelper();
        IAzureResourceDAO GetAzureResourceDatabase();
    }
}
