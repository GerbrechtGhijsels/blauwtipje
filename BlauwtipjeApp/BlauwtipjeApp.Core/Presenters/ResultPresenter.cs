using System.Threading.Tasks;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Presenters
{
    public class ResultPresenter<TResult> : BasePresenter<IResultView<TResult>> where TResult : Result
    {
        private readonly DeterminationTree<TResult> tree;
        private TResult result;

        public ResultPresenter(IResultView<TResult> view, DeterminationTree<TResult> tree) : base(view)
        {
            this.tree = tree;
        }

        public override Task OnViewCreate()
        {
            result = tree.Results.Find(r => r.Id == View.GetResultId());
            View.SetResult(result);
            return Task.CompletedTask;
        }

        public void OnResultPictureClicked()
        {
            View.ShowImageGallery(result?.ImageIdList);
        }
    }

    public interface IResultView<TResult> : IView where TResult : Result
    {
        int GetResultId();
        void SetResult(TResult result);
    }
}
