using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Main
{
    [TestClass]
    public class NotFirstTimeTest
    {
        private FakeUpdateService updateService;

        [TestInitialize]
        public void TestInitialize()
        {
            updateService = new FakeUpdateService { EssentialsInPlace = true };
        }

        /// <summary>
        /// Should skip checking for a update when there is no internet
        /// </summary>
        [TestMethod]
        public async Task NoInternet()
        {
            // Arrange
            var view = new FakeMainView();
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = false };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsFalse(updateService.HasCheckedForUpdate);
            Assert.IsFalse(view.UpdateScreenOpened);
        }

        /// <summary>
        /// Should check for a update when there is internet
        /// </summary>
        [TestMethod]
        public async Task CheckForUpdate()
        {
            // Arrange
            var view = new FakeMainView();
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsTrue(updateService.HasCheckedForUpdate);
        }

        /// <summary>
        /// Should show a cancelable dialog when an update is available
        /// </summary>
        [TestMethod]
        public async Task UpdateAvailableDialog()
        {
            // Arrange
            var view = new FakeMainView();
            updateService.UpdateAvailable = true;
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.LastDialogConfigShown);
            Assert.IsFalse(view.LastDialogConfigShown.NotCancelable);
        }

        /// <summary>
        /// Should open the update screen when the user inputs a positive result
        /// </summary>
        [TestMethod]
        public async Task AcceptUpdateDialog()
        {
            // Arrange
            var view = new FakeMainView { DefaultDialogResult = DialogResult.Positive };
            updateService.UpdateAvailable = true;
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsTrue(view.UpdateScreenOpened);
        }

        /// <summary>
        /// Should not open the update screen when the user inputs a negative result
        /// </summary>
        [TestMethod]
        public async Task DeclineUpdateDialog()
        {
            // Arrange
            var view = new FakeMainView { DefaultDialogResult = DialogResult.Negative };
            updateService.UpdateAvailable = true;
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsFalse(view.UpdateScreenOpened);
        }

        /// <summary>
        /// Should not open the update screen when the user cancels the dialog
        /// </summary>
        [TestMethod]
        public async Task CancelUpdateDialog()
        {
            // Arrange
            var view = new FakeMainView { DefaultDialogResult = DialogResult.None };
            updateService.UpdateAvailable = true;
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsFalse(view.UpdateScreenOpened);
        }
    }
}
