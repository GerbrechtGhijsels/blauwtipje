using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Update
{
    [TestClass]
    public class NoInternetTest
    {
        private FakeNetworkHelper networkHelper;

        [TestInitialize]
        public void TestInitialize()
        {
            networkHelper = new FakeNetworkHelper { InternetIsAvailable = false };
        }

        /// <summary>
        /// Should show a dialog when there is no internet
        /// </summary>
        [TestMethod]
        public async Task ShowDialog()
        {
            // Arrange
            var view = new FakeUpdateView();
            var updateService = new FakeUpdateService { EssentialsInPlace = false };
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.LastDialogConfigShown);
        }

        /// <summary>
        /// End the view when the essentials are in place
        /// </summary>
        [TestMethod]
        public async Task EndView()
        {
            // Arrange
            var view = new FakeUpdateView();
            var updateService = new FakeUpdateService { EssentialsInPlace = true };
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsTrue(view.ViewEnded);
            Assert.IsFalse(view.ApplicationEnded);
        }

        /// <summary>
        /// End the application when the essentials are not in place
        /// </summary>
        [TestMethod]
        public async Task EndApplication()
        {
            // Arrange
            var view = new FakeUpdateView();
            var updateService = new FakeUpdateService { EssentialsInPlace = false };
            var presenter = new UpdatePresenter<Result>(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsFalse(view.ViewEnded);
            Assert.IsTrue(view.ApplicationEnded);
        }
    }
}
