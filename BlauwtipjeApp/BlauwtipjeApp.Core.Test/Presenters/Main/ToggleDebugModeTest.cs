using System.Threading.Tasks;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Test.Fakes.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlauwtipjeApp.Core.Test.Presenters.Main
{
    [TestClass]
    public class ToggleDebugModeTest
    {
        [TestCleanup]
        public void TestCleanup()
        {
            Settings.ResetSettings();
        }

        /// <summary>
        /// Should by default use ReleaseMode
        /// </summary>
        [TestMethod]
        public void DefaultsToReleaseMode()
        {
            Assert.IsFalse(Settings.DebugMode);
        }

        /// <summary>
        /// Should be able to turn on DebugMode
        /// </summary>
        [TestMethod]
        public async Task TurnOnDebugMode()
        {
            // Arrange
            var view = new FakeMainView();
            var presenter = new MainPresenter(view, null, null);

            // Act
            await presenter.OnTurnOnDebugModeRequest();

            // Assert
            Assert.IsTrue(Settings.DebugMode);
        }

        /// <summary>
        /// Should be able to turn off DebugMode
        /// </summary>
        [TestMethod]
        public async Task TurnOffDebugMode()
        {
            // Arrange
            var view = new FakeMainView();
            var presenter = new MainPresenter(view, null, null);

            // Act
            await presenter.OnTurnOffDebugModeRequest();

            // Assert
            Assert.IsFalse(Settings.DebugMode);
        }
    }
}
