using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Services.Update;

namespace BlauwtipjeApp.Core.Helpers
{
    public class PresenterFactory<TResult> : IPresenterFactory<TResult> where TResult : Result
    {
        public PresenterFactory()
        {

        }

        public MainPresenter GetPresenterFor(IMainView view)
        {
            return new MainPresenter(view, ServiceLocator.GetService<IUpdateService>(), ServiceLocator.GetService<INetworkHelper>());
        }

        public DeterminationPresenter<TResult> GetPresenterFor(IDeterminationView<TResult> view)
        {
            return new DeterminationPresenter<TResult>(view, TreeManager<TResult>.GetTree(), ServiceLocator.GetService<IDeterminationInProgressDAO>());
        }

        public UpdatePresenter<TResult> GetPresenterFor(IUpdateView view)
        {
            return new UpdatePresenter<TResult>(view, ServiceLocator.GetService<IUpdateService>(), ServiceLocator.GetService<INetworkHelper>());
        }

        public InfoPresenter<TResult> GetPresenterFor(IInfoView view)
        {
            return new InfoPresenter<TResult>(view, TreeManager<TResult>.GetTree());
        }

        public ResultListPresenter<TResult> GetPresenterFor(IResultListView<TResult> view)
        {
            return new ResultListPresenter<TResult>(view, TreeManager<TResult>.GetTree());
        }

        public ResultPresenter<TResult> GetPresenterFor(IResultView<TResult> view)
        {
            return new ResultPresenter<TResult>(view, TreeManager<TResult>.GetTree());
        }
    }
}
