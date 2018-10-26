using System.Collections.Generic;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.FileManagement;

namespace BlauwtipjeApp.Core.Test.Fakes.Daos
{
    public class FakeLocalResourceDAO : ILocalResourceDAO
    {
        private List<Resource> resourceList;

        public FakeLocalResourceDAO(List<Resource> resourceList)
        {
            this.resourceList = resourceList;
        }

        public Resource Get(string name)
        {
            return resourceList.Find(r => r.Name.Equals(name));
        }

        public List<Resource> GetAllResourcesInDirectory(string directory)
        {
            return resourceList.FindAll(r => r.Name.Contains(directory));
        }

        public void Save(Resource resource)
        {
            var index = resourceList.FindIndex(r => r.Name.Equals(resource.Name));
            if (index != -1)
            {
                resourceList[index] = resource;
            }
            else
                resourceList.Add(resource);
        }

        public void Delete(string name)
        {
            resourceList.Remove(Get(name));
        }

        public void Delete(Resource resource)
        {
            resourceList.Remove(resource);
        }
    }
}
