using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Models.Tree.Traversing.Down;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes.Daos;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Determination
{
    [TestClass]
    public class DeterminationTest
    {
        /// <summary>
        /// Should be able to display the next question when a choice is selected
        /// </summary>
        [TestMethod]
        public async Task GoToNextQuestion()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            var nextQuestion = new Question { Id = 2 };
            tree.Questions.Add(new Question
            {
                Choices = new List<Choice>
                {
                    new Choice {ForwardsTraversableBehavior = new TraverseDown(nextQuestion)}
                }
            });
            tree.Questions.Add(nextQuestion);

            var view = new FakeDeterminationView();
            var determinationInProgressDao = new FakeDeterminationInProgressDAO();
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);

            // Act
            await presenter.OnViewCreate();
            presenter.OnChoiceClicked(0);

            // Assert
            Assert.AreEqual(2, view.CurrentQuestion.Id);
        }

        /// <summary>
        /// Should be able to display a result when a choice is selected
        /// </summary>
        [TestMethod]
        public async Task GoToNextResult()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            var nextResult = new Result() { Id = 5 };
            tree.Questions.Add(new Question
            {
                Choices = new List<Choice>
                {
                    new Choice {ForwardsTraversableBehavior = new TraverseDown(nextResult)}
                }
            });
            tree.Results.Add(nextResult);

            var view = new FakeDeterminationView();
            var determinationInProgressDao = new FakeDeterminationInProgressDAO();
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);

            // Act
            await presenter.OnViewCreate();
            presenter.OnChoiceClicked(0);

            // Assert
            Assert.AreEqual(5, view.ResultShown.Id);
        }
    }
}
