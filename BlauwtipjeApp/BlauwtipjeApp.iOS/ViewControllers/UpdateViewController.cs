using System;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Services.Update.Impl;
using BlauwtipjeApp.iOS.Dialogs;
using UIKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class UpdateViewController : BaseActivity<UpdatePresenter<Slug>>, IProgress<ProgressInfo>, IUpdateView
    {
        private ProgressDialogBuilder progress;

        public UpdateViewController() : base("UpdateViewController", null)
        {
        }

        public override void ViewDidLoad()
        {   
            progress = new ProgressDialogBuilder(this);
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            UpdateYes.TouchUpInside += YesButtonClick;
            UpdateNo.TouchUpInside += NoButtonClick;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


        private void YesButtonClick(object sender, EventArgs e)
        {
            Presenter.OnYesButtonClicked();
        }

        private void NoButtonClick(object sender, EventArgs e)
        {
            Presenter.OnNoButtonClicked();
        }

        public void Report(ProgressInfo value)
        {
            this.InvokeOnMainThread(() =>
            {
                if (!progress.Progress.Equals(value.Progress))
                    progress.Progress = value.Progress;

                if (value.Max < 1)
                {
                    //Indeterminate = true;
                }
                else
                {
                    //dialog.Indeterminate = false;
                    progress.Max = value.Max;
                }


                if(value.Progress == value.Max){
                    progress.Hide();
                }

                progress.dialog.Message = value.Text;
            });
        }



        public void SetChangeLogText(string changelog)
        {
            LogText.Text = changelog;
        }

        public void ShowChangeLog()
        {
            //progressBar.Visibility = ViewStates.Gone;
            DownloadText.Hidden = true;
            LogText.Hidden = false;
            //titleLog.Visibility = ViewStates.Visible;
            UpdateYes.Hidden = false;
            UpdateNo.Hidden = false;
            UpdateText.Hidden = false;
        }

        public void ShowProgressBar()
        {
            //progressBar.Visibility = ViewStates.Visible;
            progress.CreateProgressDialog();
            DownloadText.Hidden = false;
            LogText.Hidden = true;
            //titleLog.Visibility = ViewStates.Visible;
            UpdateYes.Hidden = true;
            UpdateNo.Hidden = true;
            UpdateText.Hidden = true;
        }

        public ProgressReporter GetProgressReporter()
        {
            return new ProgressReporter(this);
        }
    }
}

