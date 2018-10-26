using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree;
using BlauwtipjeApp.Core.Services.Update;

namespace BlauwtipjeApp.Core.Interfaces
{
    public interface ICoreServiceFactory<TResult> where TResult : Result
    {
        ILocalResourceDAO GetLocalResourceDatabase();
        ITreeFactory<TResult> GetTreeFactory();
        IUpdateService GetUpdateService();
    }
}
