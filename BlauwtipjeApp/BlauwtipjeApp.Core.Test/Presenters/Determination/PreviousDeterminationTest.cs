using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Models;
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
    public class PreviousDeterminationTest
    {
        /// <summary>
        /// Should start at first question when no previous determination is present
        /// </summary>
        [TestMethod]
        public async Task StartsAtFirstQuestion()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            tree.Questions.Add(new Question { Id = 1 });
            var view = new FakeDeterminationView();
            var determinationInProgressDao = new FakeDeterminationInProgressDAO();
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.AreEqual(1, view.CurrentQuestion.Id);
        }

        /// <summary>
        /// 1. Asks if user wants to continue with the previous determination.
        /// 2a. Should continue when the user gives a positive dialog result.
        /// 2b. Should start at the first question when the user gives a negative dialog result, 
        ///     but should not remove the previous determination.
        /// </summary>
        [TestMethod]
        public async Task ContinuePreviousDetermination()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            tree.Questions.Add(new Question { Id = 1 });
            tree.Questions.Add(new Question { Id = 2 });
            var view = new FakeDeterminationView();
            var determinationInProgressDao = new FakeDeterminationInProgressDAO { StoredDetermination = new DeterminationInProgress { QuestionID = 2 } };
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);

            // Act
            view.DefaultDialogResult = DialogResult.Positive;
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.LastDialogConfigShown);
            Assert.AreEqual(2, view.CurrentQuestion.Id);

            // Act
            view.DefaultDialogResult = DialogResult.Negative;
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.LastDialogConfigShown);
            Assert.AreEqual(1, view.CurrentQuestion.Id);
            Assert.IsNotNull(determinationInProgressDao.StoredDetermination);
        }

        /// <summary>
        /// Should save the question id and the determination picture when going to the next question
        /// </summary>
        [TestMethod]
        public async Task SaveDeterminationProgress()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            var nextQuestion = new Question { Id = 2 };
            tree.Questions.Add(new Question
            {
                Id = 1,
                Choices = new List<Choice>
                {
                    new Choice {ForwardsTraversableBehavior = new TraverseDown(nextQuestion)}
                }
            });
            tree.Questions.Add(nextQuestion);
            var view = new FakeDeterminationView();
            var determinationInProgressDao = new FakeDeterminationInProgressDAO();
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);
            var pictureChosenByUser = new byte[] { 1, 2, 3 };

            // Act
            await presenter.OnViewCreate();
            presenter.OnUserSelectPictureResponse(pictureChosenByUser);
            presenter.OnChoiceClicked(0);

            // Assert
            Assert.AreEqual(pictureChosenByUser, determinationInProgressDao.StoredDetermination.DeterminationPicture);
            Assert.AreEqual(2, determinationInProgressDao.StoredDetermination.QuestionID);
        }

        /// <summary>
        /// Should discard the determination progress when going to a result
        /// </summary>
        [TestMethod]
        public async Task ResetDeterminationProgress()
        {
            // Arrange
            var tree = new DeterminationTree<Result>();
            var nextResult = new Result() { Id = 5 };
            tree.Questions.Add(new Question
            {
                Id = 1,
                Choices = new List<Choice>
                {
                    new Choice {ForwardsTraversableBehavior = new TraverseDown(nextResult)}
                }
            });
            tree.Results.Add(nextResult);
            var view = new FakeDeterminationView();
            var determinationInProgressDao = new FakeDeterminationInProgressDAO()
            {
                StoredDetermination = new DeterminationInProgress
                {
                    QuestionID = 1,
                    DeterminationPicture = new byte[] {1, 2, 3}
                }
            };
            var presenter = new DeterminationPresenter<Result>(view, tree, determinationInProgressDao);

            // Act
            await presenter.OnViewCreate();
            presenter.OnChoiceClicked(0);

            // Assert
            Assert.IsNull(determinationInProgressDao.StoredDetermination);
        }
    }
}
