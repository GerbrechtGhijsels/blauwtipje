using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Info
{
    [TestClass]
    public class ShowInfoTest
    {
        /// <summary>
        /// Should show the Info field of the tree
        /// </summary>
        [TestMethod]
        public async Task ShowInfo()
        {
            // Arrange
            var view = new FakeInfoView();
            var tree = new DeterminationTree<Result> { Info = "test" };
            var presenter = new InfoPresenter<Result>(view, tree);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.AreEqual("test", view.InfoText);
        }

        /// <summary>
        /// Should show something else when the Info field of the tree is not filled
        /// </summary>
        [TestMethod]
        public async Task ShowSomethingElse()
        {
            // Arrange
            var view = new FakeInfoView();
            var tree = new DeterminationTree<Result>();
            var presenter = new InfoPresenter<Result>(view, tree);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.InfoText);
        }
    }
}
