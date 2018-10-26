using BlauwtipjeApp.Core.Models.FileManagement;

namespace BlauwtipjeApp.Core.DataAccess
{
    public interface IAzureResourceDAO
    {
        Resource Get(string resourceName);
        string GetEtag(string resourceName);
        Resource GetIfUpdated(string resourceName, string etag);
    }
}
