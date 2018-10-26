using System.Collections.Generic;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.FileManagement;

namespace BlauwtipjeApp.Core.Test.Fakes.Daos
{
    public class FakeAzureResourceDAO : IAzureResourceDAO
    {
        private List<Resource> resourceList;

        public FakeAzureResourceDAO(List<Resource> resourceList)
        {
            this.resourceList = resourceList;
        }

        public Resource Get(string name)
        {
            return resourceList.Find(r => r.Name.Equals(name));
        }

        public string GetEtag(string resourceName)
        {
            return Get(resourceName)?.Etag;
        }

        public Resource GetIfUpdated(string resourceName, string etag)
        {
            var resource = Get(resourceName);
            if (!resource.Etag.Equals(etag))
                return resource;
            return null;
        }
    }
}
