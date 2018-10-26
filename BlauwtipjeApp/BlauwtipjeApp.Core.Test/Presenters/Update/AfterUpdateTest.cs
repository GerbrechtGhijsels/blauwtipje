using System;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Update
{
    [TestClass]
    public class AfterUpdateTest
    {
        private FakeUpdateService updateService;
        private FakeNetworkHelper networkHelper;

        [TestInitialize]
        public void TestInitialize()
        {
            updateService = new FakeUpdateService { EssentialsInPlace = true };
            networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
        }

        /// <summary>
        /// Should hide the progress bar and show the changelog after the update failed
        /// </summary>
        [TestMethod]
        public void UpdateFailed()
        {
            // Arrange
            var view = new FakeUpdateView();
            updateService.UpdateThrowsExeption = new Exception();
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            presenter.StartUpdate();

            // Assert
            Assert.IsTrue(view.ProgressBarIsVisible);
        }

        /// <summary>
        /// Should show a cancelable dialog and end the view after the update completed successfully
        /// </summary>
        [TestMethod]
        public void UpdateCompleted()
        {
            // Arrange
            var view = new FakeUpdateView();
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            presenter.StartUpdate();

            // Assert
            Assert.IsNull(view.LastDialogConfigShown);
            Assert.IsTrue(view.ViewEnded);
            Assert.IsFalse(view.ApplicationEnded);
        }
    }
}
