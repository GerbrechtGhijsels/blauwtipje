using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.DataAccess.Impl;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree;
using BlauwtipjeApp.Core.Services.Update;
using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Core.Helpers
{
    public class CoreServiceFactory<TResult> : ICoreServiceFactory<TResult> where TResult : Result
    {
        private IFileHelper fileHelper;
        private IAzureResourceDAO remoteDatabase;

        public CoreServiceFactory(IFileHelper fileHelper, IAzureResourceDAO remoteDatabase)
        {
            this.fileHelper = fileHelper;
            this.remoteDatabase = remoteDatabase;
        }

        private ILocalResourceDAO _localResourceDao;
        public ILocalResourceDAO GetLocalResourceDatabase()
        {
            if (_localResourceDao == null)
            {
                var dbPath = fileHelper.GetFilePath(Settings.LocalDatabaseName);
                _localResourceDao = new LocalResourceDAO(dbPath);
            }
            return _localResourceDao;
        }

        private IDeterminationInProgressDAO _determinationInProgressDao;
        public IDeterminationInProgressDAO GetDeterminationInProgressDAO()
        {
            if (_determinationInProgressDao == null)
            {
                var dbPath = fileHelper.GetFilePath(Settings.LocalDatabaseName);
                _determinationInProgressDao = new DeterminationInProgressDAO(dbPath);
            }
            return _determinationInProgressDao;
        }

        private ITreeFactory<TResult> treeFactory;
        public ITreeFactory<TResult> GetTreeFactory()
        {
            if (treeFactory == null)
            {
                treeFactory = new TreeFactory<TResult>(GetLocalResourceDatabase());
            }
            return treeFactory;
        }

        private IUpdateService updateService;
        public IUpdateService GetUpdateService()
        {
            if (updateService == null)
            {
                updateService = new UpdateService(remoteDatabase, GetLocalResourceDatabase(), Settings.XmlFileName, Settings.ChangelogFileName);
            }
            return updateService;
        }

        private IPresenterFactory<TResult> _presenterFactory;
        public IPresenterFactory<TResult> GetPresenterFactory()
        {
            if (_presenterFactory == null)
            {
                _presenterFactory = new PresenterFactory<TResult>();
            }
            return _presenterFactory;
        }
    }
}
