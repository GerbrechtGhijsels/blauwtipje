using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Services.Update;
using BlauwtipjeApp.Core.Models.Tree;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BlauwtipjeApp.Core.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        private readonly List<Image> _randomStartScreenImages;
        private readonly IUpdateService _updateService;
        private readonly INetworkHelper _networkHelper;
        

        public MainPresenter(IMainView view, IUpdateService updateService, INetworkHelper networkHelper) : base(view)
        {
            _updateService = updateService;
            _networkHelper = networkHelper;
        }

        public override async Task OnViewCreate()
        {
            if (Settings.UpdateWasNotCompleted)
            {
                await View.ShowAlertDialog(new AlertDialogConfig
                {
                    Title = "Het updaten was niet voltooid",
                    Message = "De update zal nu opnieuw moeten worden uitgevoerd",
                    NotCancelable = true,
                    NeutralButtonText = "Ok"
                });
                View.OpenUpdateScreen();
                return;
            }

            var determinationXmlInstalled = _updateService.AreEssentialsInPlace();
            if (!determinationXmlInstalled)
            {
                await View.ShowAlertDialog(new AlertDialogConfig
                {
                    Title = "Alleen de eerste keer...",
                    Message = "Om de installatie te voltooien moet er nog wat gedownload worden. Hiervoor is een internetconnectie vereist.",
                    NotCancelable = true,
                    NeutralButtonText = "Ok"
                });
                View.OpenUpdateScreen();
                return;
            }

            if (_networkHelper.HasInternet())
                await CheckForUpdate();
            SetRandomPicture();
        }

        public async Task CheckForUpdate()
        {
            var updateAvailable = _updateService.IsUpdateAvailable();
            if (!updateAvailable) return;
            var dialogResult = await View.ShowAlertDialog(new AlertDialogConfig
            {
                Title = "Update",
                Message = "Er is een update beschikbaar. Wilt u die nu downloaden?",
                NotCancelable = false,
                PositiveButtonText = "Ja",
                NegativeButtonText = "Nee"
            });

            if (dialogResult == DialogResult.Positive) View.OpenUpdateScreen();
        }

        public void OnDeterminationButtonClicked()
        {
            View.NavigateTo(NavigableScreen.Determination);
        }

        public void OnSpeciesListButtonClicked()
        {
            View.NavigateTo(NavigableScreen.SpeciesList);
        }

        public void OnInfoButtonClicked()
        {
            View.NavigateTo(NavigableScreen.Info);
        }

        public async Task OnTurnOnDebugModeRequest()
        {
            Settings.DebugMode = true;
            Settings.IsDebugModeToggled = true;
            await View.ShowAlertDialog(new AlertDialogConfig
            {
                Title = "Debug mode is aangezet",
                Message = "Er moet nu een update gedownload worden",
                NotCancelable = true,
                NeutralButtonText = "Ok"
            });
            View.OpenUpdateScreen();
        }

        public async Task OnTurnOffDebugModeRequest()
        {
            Settings.DebugMode = false;
            Settings.IsDebugModeToggled = true;
            await View.ShowAlertDialog(new AlertDialogConfig
            {
                Title = "Debug mode is uitgezet",
                Message = "Er moet nu een update gedownload worden",
                NotCancelable = true,
                NeutralButtonText = "Ok"
            });
            View.OpenUpdateScreen();
        }
        public void SetRandomPicture()
        {
            Random rand1 = new Random();
            var images = View.GetRandomPicturesForOnMainScreen();
            if (images.Count > 3)
                View.SetRandomPicture(images[rand1.Next(0,4)].Content);
        }
    }

    public interface IMainView : IView
    {
        void OpenUpdateScreen();

        void SetRandomPicture(byte[] pictrue);

        List<Image> GetRandomPicturesForOnMainScreen();
    }
}
