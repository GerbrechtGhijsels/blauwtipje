using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes.Daos;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Determination
{
    [TestClass]
    public class InfoButtonTest
    {
        /// <summary>
        /// Should display the note of the current question if the info button is clicked
        /// </summary>
        [TestMethod]
        public void ShowInfo()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            var view = new FakeDeterminationView();
            view.SetCurrentQuestion(new Question { Note = "test" });
            var determinationInProgressDao = new FakeDeterminationInProgressDAO();
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);

            // Act
            presenter.OnInfoButtonClicked();

            // Assert
            Assert.AreEqual("test", view.LastDialogConfigShown.Message);
        }
    }
}
