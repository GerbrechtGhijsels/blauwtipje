using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.FileManagement;
using SQLite;

namespace BlauwtipjeApp.Core.DataAccess.Impl
{
    public class LocalResourceDAO : SqliteDatabase, ILocalResourceDAO
    {
        public LocalResourceDAO(string dbPath) : base(dbPath)
        {
        }

        public Resource Get(string name)
        {
            Resource resource;
            
            using (var db = new SQLiteConnection(dbPath))
            {
                resource = db.FindWithQuery<Resource>("SELECT * FROM resources WHERE name=?", name);
            }

            return resource;
        }

        public Resource GetIfUpdated(string resourceName, string sEtag)
        {
            var resource = Get(resourceName);
            if (resource.Etag.Equals(sEtag))
                return resource;

            return null;
        }

        public string GetEtag(string resourceName)
        {
            Resource returnResource;
            using (var db = new SQLiteConnection(dbPath))
            {
                returnResource = (from resource in db.Table<Resource>()
                    where resource.Name == resourceName
                    select resource).FirstOrDefault();

            }
            return returnResource?.Etag;
        }

        public List<Resource> GetAllResourcesInDirectory(string directory)
        {
            List<Resource> resources;
            using (var db = new SQLiteConnection(dbPath))
            {
                resources = db.Query<Resource>("select * from resources where name like ?", directory + "%");
            }
            return resources;
        }

        public void Save(Resource resource)
        {   
            Resource findResource;
            using (var db = new SQLiteConnection(dbPath))
            {
                findResource = (from getResource in db.Table<Resource>()
                                where getResource.Name == resource.Name
                                select getResource).FirstOrDefault();
            }

            if (findResource != null)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    findResource.Etag = resource.Etag;
                    findResource.Content = resource.Content;
                    db.Update(findResource);
                }
            }
            else
            {   
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.Insert(resource);

                }
            }
        }

        public void Delete(string name)
        {
            Resource resource;
            using (var db = new SQLiteConnection(dbPath))
            {
                resource = (from getResource in db.Table<Resource>()
                    where getResource.Name == name
                    select getResource).FirstOrDefault();
                if (resource != null)
                {
                    db.Delete(resource);
                }
            }
            
        }

        public void Delete(Resource resource)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Delete(resource);
            }
        }
    }
}
