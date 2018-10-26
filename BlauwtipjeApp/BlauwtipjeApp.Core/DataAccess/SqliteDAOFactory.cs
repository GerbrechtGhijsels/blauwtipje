using BlauwtipjeApp.Core.DataAccess.Impl;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;


namespace BlauwtipjeApp.Core.DataAccess
{
    public class SqliteDaoFactory
    {
        private IFileHelper fileHelper;

        public SqliteDaoFactory(IFileHelper fileHelper)
        {
            this.fileHelper = fileHelper;
        }

        public ILocalResourceDAO CreateLocalResourceDao()
        {
            var dbPath = fileHelper.GetFilePath(Settings.LocalDatabaseName);
            return new LocalResourceDAO(dbPath);
        }
    }
}
