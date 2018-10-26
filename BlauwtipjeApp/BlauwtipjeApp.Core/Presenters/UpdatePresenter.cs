using System;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Tree.Decorators.Exceptions;
using BlauwtipjeApp.Core.Services.Update;
using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Core.Presenters
{
    public class UpdatePresenter<TResult> : BasePresenter<IUpdateView> where TResult : Result
    {
        private IUpdateService updateService;
        private INetworkHelper networkHelper;
        private bool DisableBackButton;

        public UpdatePresenter(IUpdateView view, IUpdateService updateService, INetworkHelper networkHelper) : base(view)
        {
            this.networkHelper = networkHelper;
            this.updateService = updateService;
            this.updateService.OnUpdateCompleted += OnUpdateCompleted;
        }

        public override async Task OnViewCreate()
        {
            var hasInternet = networkHelper.HasInternet();
            if (!hasInternet)
            {
                await View.ShowAlertDialog(new AlertDialogConfig
                {
                    Title = "Internet vereist",
                    Message = "Zet uw internet aan om de update te voltooien",
                    NotCancelable = true,
                    NeutralButtonText = "Ok"
                });
                if (!IsProgramInAnUnstableState())
                    View.EndView();
                else
                    View.EndApplication();
                return;
            }

            if (IsProgramInAnUnstableState())
                StartUpdate();
            else
            {
                if (!updateService.IsUpdateAvailable())
                    View.EndView();
                var changeLogText = "";
                await Task.Run(() => {
                    changeLogText = updateService.GetChangelog();
                });
                View.SetChangeLogText(changeLogText);
                View.ShowChangeLog();
            }
        }

        public bool IsProgramInAnUnstableState()
        {
            return !updateService.AreEssentialsInPlace() 
                   || Settings.IsDebugModeToggled 
                   || Settings.UpdateWasNotCompleted;
        }

        public void OnYesButtonClicked()
        {
            StartUpdate();
        }

        public void OnNoButtonClicked()
        {
            View.EndView();
        }

        public async void StartUpdate()
        {
            var retryUpdate = false;
            View.ShowProgressBar();
            do
            {
                try
                {
                    Settings.UpdateWasNotCompleted = true;
                    DisableBackButton = true;
                    await updateService.DoUpdate<TResult>(View.GetProgressReporter());
                }
                catch (Exception ex)
                {
                    AlertDialogConfig alertDialog;
                    if (ex is TreeDecorateException)
                    {
                        alertDialog = new AlertDialogConfig
                        {
                            Title = "Fout bij het laden van de determinatie boom",
                            Message = ex.Message,
                        };
                    }
                    else
                    {
                        alertDialog = new AlertDialogConfig
                        {
                            Title = "Updaten mislukt",
                            Message = "Er is iets fout gegaan tijdens het updaten: " + ex.Message
                        };
                    }

                    alertDialog.NotCancelable = true;
                    alertDialog.PositiveButtonText = "Opnieuw proberen";
                    alertDialog.NegativeButtonText = "Sluit applicatie";
                    var dialogResult = await View.ShowAlertDialog(alertDialog);
                    if (dialogResult == DialogResult.Positive)
                    {
                        retryUpdate = true;
                    }
                    else if (dialogResult == DialogResult.Negative)
                    {
                        retryUpdate = false;
                        View.EndApplication();
                    }
                }
            } while (retryUpdate);
        }

        private void OnUpdateCompleted(object sender, EventArgs e)
        {
            Settings.IsDebugModeToggled = false;
            Settings.UpdateWasNotCompleted = false;
            DisableBackButton = false;
            View.EndView();
        }

        public override Task<bool> OnBackButtonClicked()
        {
            return Task.FromResult(DisableBackButton);
        }
    }

    public interface IUpdateView : IView
    {
        void SetChangeLogText(string changelog);
        void ShowChangeLog();
        void ShowProgressBar();
        ProgressReporter GetProgressReporter();
    }
}
