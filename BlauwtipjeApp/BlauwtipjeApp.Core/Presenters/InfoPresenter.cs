using System.Threading.Tasks;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Presenters
{
    public class InfoPresenter<TResult> : BasePresenter<IInfoView> where TResult : Result
    {
        private readonly DeterminationTree<TResult> tree;

        public InfoPresenter(IInfoView view, DeterminationTree<TResult> tree) : base(view)
        {
            this.tree = tree;
        }

        public override Task OnViewCreate()
        {
            View.SetInfoText(tree.Info ?? "Er is geen informatie");
            return Task.CompletedTask;
        }
    }

    public interface IInfoView : IView
    {
        void SetInfoText(string text);
    }
}
