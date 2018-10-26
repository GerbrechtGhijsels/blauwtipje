using System.Collections.Generic;
using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.FileManagement;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml;
using BlauwtipjeApp.Core.Services.Tree.Converters.Xml.Impl;

namespace BlauwtipjeApp.Core.Services.Update.Impl
{
    public class UpdateHelper
    {
        private IAzureResourceDAO remoteDatabase;
        private ILocalResourceDAO _localDao;

        public UpdateHelper(IAzureResourceDAO remoteDatabase, ILocalResourceDAO localDao)
        {
            this.remoteDatabase = remoteDatabase;
            this._localDao = localDao;
        }

        public bool IsResourceChanged(string resource)
        {
            var remoteEtag = remoteDatabase.GetEtag(resource) ?? "";
            var localEtag = _localDao.Get(resource)?.Etag ?? "";
            return !remoteEtag.Equals(localEtag);
        }

        public Resource GetLocalResource(string resource)
        {
            return _localDao.Get(resource);
        }

        public bool UpdateResourceIfChanged(string resourceName)
        {
            var localResource = _localDao.Get(resourceName);
            if (localResource == null)
            {
                _localDao.Save(remoteDatabase.Get(resourceName));
                return true;
            }

            var resource = remoteDatabase.GetIfUpdated(resourceName, localResource.Etag);
            if (resource == null)
                return false;

            _localDao.Save(resource);
            return true;
        }

        public string GetRemoteResourceAsString(string resource)
        {
            return remoteDatabase.Get(resource).GetContentAsString();
        }

        public string GetLocalResourceAsString(string resource)
        {
            return _localDao.Get(resource).GetContentAsString();
        }

        public List<Resource> GetAllLocalResourcesFromDirectory(string directory)
        {
            return _localDao.GetAllResourcesInDirectory(directory);
        }

        public void RemoveLocalResource(string resourceName)
        {
            _localDao.Delete(resourceName);
        }

        public List<Image> GetImagesFromXml(string xml)
        {
            return new XmlTreeConverter<Slug>(XmlConverterSettings.GetBlauwtipjeXmlConverterSettings())
                .FromXml(xml).Images;
        }

        public List<Resource> GetImagesFromLocalDatabase()
        {
            var imageList = new List<Resource>();
            foreach (var resource in GetAllLocalResourcesFromDirectory("zeenaaktslakken"))
            {
                imageList.Add(resource);
            }
            return imageList;
        }
    }
}
