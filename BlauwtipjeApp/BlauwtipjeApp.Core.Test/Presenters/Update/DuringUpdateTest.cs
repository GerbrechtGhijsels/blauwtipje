using System;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Services.Tree.Decorators.Exceptions;
using BlauwtipjeApp.Core.Services.Update.Impl;
using BlauwtipjeApp.Core.Test.Fakes;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Update
{
    [TestClass]
    public class DuringUpdateTest
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
        /// Should report progress of the update
        /// </summary>
        [TestMethod]
        public void ReportProgress()
        {
            // Arrange
            var view = new FakeUpdateView();
            updateService.UpdateReportsProgressInfo = new ProgressInfo {Text = "Test", Max = 100, Progress = 50};
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            presenter.StartUpdate();

            // Assert
            Assert.AreEqual("Test", view.LastProgressInfoValueReported.Text);
            Assert.AreEqual(100, view.LastProgressInfoValueReported.Max);
            Assert.AreEqual(50, view.LastProgressInfoValueReported.Progress);
        }

        /// <summary>
        /// Should show a dialog when a TreeDecorateException is thrown during the update process
        /// with the exception message in the message of the dialog
        /// </summary>
        [TestMethod]
        public void ShowTreeDecorationErrorDialog()
        {
            // Arrange
            var view = new FakeUpdateView();
            updateService.UpdateThrowsExeption = new TreeDecorateException("test");
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            presenter.StartUpdate();

            // Assert
            Assert.IsTrue(updateService.UpdateWasStarted);
            Assert.IsNotNull(view.LastDialogConfigShown);
            Assert.AreEqual("test", view.LastDialogConfigShown.Message);
        }

        /// <summary>
        /// Should show a dialog when a Exception is thrown during the update process
        /// with the exception message contained in the message of the dialog
        /// </summary>
        [TestMethod]
        public void ShowExceptionErrorDialog()
        {
            // Arrange
            var view = new FakeUpdateView();
            updateService.UpdateThrowsExeption = new Exception("test");
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            presenter.StartUpdate();

            // Assert
            Assert.IsTrue(updateService.UpdateWasStarted);
            Assert.IsNotNull(view.LastDialogConfigShown);
            Assert.IsTrue(view.LastDialogConfigShown.Message.Contains("test"));
        }
    }
}
