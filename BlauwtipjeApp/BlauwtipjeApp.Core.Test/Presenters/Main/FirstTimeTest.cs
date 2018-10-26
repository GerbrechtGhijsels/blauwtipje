using System.Threading.Tasks;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Services.Update;
using BlauwtipjeApp.Core.Test.Fakes;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Main
{
    [TestClass]
    public class FirstTimeTest
    {
        private IUpdateService updateService;

        [TestInitialize]
        public void TestInitialize()
        {
            updateService = new FakeUpdateService { EssentialsInPlace = false };
        }

        /// <summary>
        /// Should show a not cancelable dialog to the user and redirect them to the update screen
        /// </summary>
        [TestMethod]
        public async Task FirstTimeWithInternet()
        {
            // Arrange
            var view = new FakeMainView();
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = true };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.LastDialogConfigShown);
            Assert.IsTrue(view.LastDialogConfigShown.NotCancelable);
            Assert.IsTrue(view.UpdateScreenOpened);
        }

        /// <summary>
        /// Should show a not cancelable dialog to the user and redirect them to the update screen
        /// </summary>
        [TestMethod]
        public async Task FirstTimeWithoutInternet()
        {
            // Arrange
            var view = new FakeMainView();
            var networkHelper = new FakeNetworkHelper { InternetIsAvailable = false };
            var presenter = new MainPresenter(view, updateService, networkHelper);

            // Act
            await presenter.OnViewCreate();

            // Assert
            Assert.IsNotNull(view.LastDialogConfigShown);
            Assert.IsTrue(view.LastDialogConfigShown.NotCancelable);
            Assert.IsTrue(view.UpdateScreenOpened);
        }
    }
}
