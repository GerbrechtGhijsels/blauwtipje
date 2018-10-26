using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.FileManagement;

namespace BlauwtipjeApp.Core.DataAccess
{
    public interface ILocalResourceDAO
    {
        Resource Get(string name);
        List<Resource> GetAllResourcesInDirectory(string directory);
        void Save(Resource resource);
        void Delete(string name);
        void Delete(Resource resource);
    }
}
