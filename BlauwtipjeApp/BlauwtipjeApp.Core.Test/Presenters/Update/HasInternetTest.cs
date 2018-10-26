using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Update
{
    [TestClass]
    public class HasInternetTest
    {
        private FakeNetworkHelper networkHelper;

        [TestInitialize]
        public void TestInitialize()
        {
            networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
        }

        /// <summary>
        /// Should immediatly start updating when there is internet but the essentials are not in place 
        /// </summary>
        [TestMethod]
        public async Task StartUpdateImmediatly()
        {
            // Arrange
            var view = new FakeUpdateView();
            var updateService = new FakeUpdateService { EssentialsInPlace = false };
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsTrue(updateService.UpdateWasStarted);
        }

        /// <summary>
        /// Should show the changelog of the update when there is internet and the essentials are in place
        /// </summary>
        [TestMethod]
        public async Task ShowChangelog()
        {
            // Arrange
            var view = new FakeUpdateView();
            var updateService = new FakeUpdateService { EssentialsInPlace = true, ChangeLog = "test" };
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsFalse(updateService.UpdateWasStarted);
            Assert.IsFalse(view.ProgressBarIsVisible);
            Assert.IsTrue(view.ChangeLogIsVisible);
            Assert.AreEqual("test", view.ChangeLogText);
        }

        /// <summary>
        /// Should start updating when the user inputs a positive result
        /// </summary>
        [TestMethod]
        public void StartUpdateOnPostiveResult()
        {
            // Arrange
            var view = new FakeUpdateView();
            var updateService = new FakeUpdateService { EssentialsInPlace = true };
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            presenter.OnYesButtonClicked();

            // Assert
            Assert.IsTrue(updateService.UpdateWasStarted);
            Assert.IsTrue(view.ProgressBarIsVisible);
            Assert.IsFalse(view.ChangeLogIsVisible);
        }

        /// <summary>
        /// Should end the view when the user inputs a negative result
        /// </summary>
        [TestMethod]
        public void EndViewOnNegativeResult()
        {
            // Arrange
            var view = new FakeUpdateView();
            var updateService = new FakeUpdateService { EssentialsInPlace = true };
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            presenter.OnNoButtonClicked();

            // Assert
            Assert.IsFalse(updateService.UpdateWasStarted);
            Assert.IsTrue(view.ViewEnded);
        }
    }
}
