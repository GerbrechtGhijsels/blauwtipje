using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Presenters
{
    public class ResultListPresenter<TResult> : BasePresenter<IResultListView<TResult>> where TResult : Result
    {
        private DeterminationTree<TResult> tree;

        public ResultListPresenter(IResultListView<TResult> view, DeterminationTree<TResult> tree) : base(view)
        {
            this.tree = tree;
        }

        public override Task OnViewCreate()
        {   
            View.SetResultList(tree.Results);
            return Task.CompletedTask;
        }

        public void OnResultClicked(int idOfResult)
        {
            var result = tree.Results.Find(r => r.Id == idOfResult);
            if (result == null)
                View.ShowNotification("Kan detailpagina niet openen: Data niet gevonden");
            else
                View.ShowResultDetailView(result);
        }
    }

    public interface IResultListView<TResult> : IView where TResult : Result
    {
        void SetResultList(List<TResult> results);
        void ShowResultDetailView(TResult result);
    }
}
