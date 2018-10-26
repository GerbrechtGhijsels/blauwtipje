using System.Collections.Generic;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;

namespace BlauwtipjeApp.Core.Test.Fakes.Views
{
    public class FakeResultListView : FakeBaseView, IResultListView<Result>
    {
        public FakeResultListView()
        {
        }

        public List<Result> ResultList { get; private set; }
        public void SetResultList(List<Result> results)
        {
            ResultList = results;
        }

        public bool ResultDetailViewShown { get; private set; }
        public Result ResultShown { get; private set; }
        public void ShowResultDetailView(Result result)
        {
            ResultDetailViewShown = true;
            ResultShown = result;
        }
    }
}