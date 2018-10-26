using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.ResultList
{
    [TestClass]
    public class ShowResultListTest
    {
        /// <summary>
        /// Should show a list of results from the tree
        /// </summary>
        [TestMethod]
        public async Task ShowAllResultsFromTree()
        {
            // Arrange
            var view = new FakeResultListView();
            var tree = new DeterminationTree<Result>();
            tree.Results.Add(new Result { Id = 1 });
            tree.Results.Add(new Result { Id = 2 });
            tree.Results.Add(new Result { Id = 3 });
            var presenter = new ResultListPresenter<Result>(view, tree);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.ResultList);
            Assert.AreEqual(3, view.ResultList.Count);
        }

        /// <summary>
        /// Should open the results detail view when it is clicked
        /// </summary>
        [TestMethod]
        public void ShowResultDetailView()
        {
            // Arrange
            var view = new FakeResultListView();
            var tree = new DeterminationTree<Result>();
            tree.Results.Add(new Result { Id = 1 });
            tree.Results.Add(new Result { Id = 2 });
            tree.Results.Add(new Result { Id = 3 });
            var presenter = new ResultListPresenter<Result>(view, tree);

            // Act
            presenter.OnResultClicked(2);

            // Assert
            Assert.IsTrue(view.ResultDetailViewShown);
            Assert.AreEqual(2, view.ResultShown.Id);
        }

        /// <summary>
        /// Should show a error notification when the result could not be found in the tree
        /// </summary>
        [TestMethod]
        public void ShowErrorNotification()
        {
            // Arrange
            var view = new FakeResultListView();
            var tree = new DeterminationTree<Result>();
            tree.Results.Add(new Result { Id = 1 });
            tree.Results.Add(new Result { Id = 2 });
            tree.Results.Add(new Result { Id = 3 });
            var presenter = new ResultListPresenter<Result>(view, tree);

            // Act
            presenter.OnResultClicked(5);

            // Assert
            Assert.IsFalse(view.ResultDetailViewShown);
            Assert.IsNotNull(view.LastNotificationShown);
        }
    }
}
