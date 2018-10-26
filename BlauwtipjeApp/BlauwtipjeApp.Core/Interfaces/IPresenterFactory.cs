using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;

namespace BlauwtipjeApp.Core.Interfaces
{
    public interface IPresenterFactory<TResult> where TResult : Result
    {
        MainPresenter GetPresenterFor(IMainView view);
        DeterminationPresenter<TResult> GetPresenterFor(IDeterminationView<TResult> view);
        UpdatePresenter<TResult> GetPresenterFor(IUpdateView view);
        InfoPresenter<TResult> GetPresenterFor(IInfoView view);
        ResultListPresenter<TResult> GetPresenterFor(IResultListView<TResult> view);
        ResultPresenter<TResult> GetPresenterFor(IResultView<TResult> view);
    }
}
