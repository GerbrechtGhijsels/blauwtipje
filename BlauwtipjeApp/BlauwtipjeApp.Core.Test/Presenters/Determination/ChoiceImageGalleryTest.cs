using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes.Daos;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Determination
{
    [TestClass]
    public class ChoiceImageGalleryTest
    {
        /// <summary>
        /// Should open a Image Gallery when the user clicks on a picture of a choice
        /// </summary>
        [TestMethod]
        public async Task OpenImageGallery()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            tree.Questions.Add(new Question
            {
                Choices = new List<Choice>
                {
                    new Choice
                    {
                        ImageIdList = new List<int> { 1, 2 }
                    }
                }
            });
            var view = new FakeDeterminationView();
            var determinationInProgressDao = new FakeDeterminationInProgressDAO();
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);

            // Act
            await presenter.OnViewCreate();
            presenter.OnChoiceImageClicked(0);

            // Assert
            Assert.IsTrue(view.ImageGalleryShown);
            Assert.AreEqual(1, view.LastImageGalleryIdsShown[0]);
            Assert.AreEqual(2, view.LastImageGalleryIdsShown[1]);
        }
    }
}
