using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label="", ScreenOrientation = ScreenOrientation.Portrait)]
    public class UpdateActivity : BaseActivity<UpdatePresenter<Slug>>, IProgress<ProgressInfo>, IUpdateView
    {
        private ProgressBar progressBar;
        private TextView textLog;
        private TextView titleLog;
        private TextView text;
        private TextView updateText;
        private Button YesButton;
        private Button NoButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            CustomLayoutId = Resource.Layout.activity_update;
            DisableNavigation = true;
            CurrentActivity = NavigableScreen.Other;
            base.OnCreate(savedInstanceState);
            ActivityTitle.Text = "Updates";

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            textLog = FindViewById<TextView>(Resource.Id.textLog);
            titleLog = FindViewById<TextView>(Resource.Id.titleLog);
            text = FindViewById<TextView>(Resource.Id.text);
            updateText = FindViewById<TextView>(Resource.Id.updateText);

            YesButton = FindViewById<Button>(Resource.Id.updateYes);
            NoButton = FindViewById<Button>(Resource.Id.updateNo);
            YesButton.Click += YesButtonClick;
            NoButton.Click += NoButtonClick;
        }

        public ProgressReporter GetProgressReporter()
        {
            return new ProgressReporter(this);
        }

        public void SetChangeLogText(string changelog)
        {
            textLog.Text = changelog;
        }

        public void ShowChangeLog()
        {
            progressBar.Visibility = ViewStates.Gone;
            text.Visibility = ViewStates.Gone;
            textLog.Visibility = ViewStates.Visible;
            titleLog.Visibility = ViewStates.Visible;
            YesButton.Visibility = ViewStates.Visible;
            NoButton.Visibility = ViewStates.Visible;
            updateText.Visibility = ViewStates.Visible;
        }

        public void ShowProgressBar()
        {
            progressBar.Visibility = ViewStates.Visible;
            text.Visibility = ViewStates.Visible;
            textLog.Visibility = ViewStates.Gone;
            titleLog.Visibility = ViewStates.Gone;
            YesButton.Visibility = ViewStates.Gone;
            NoButton.Visibility = ViewStates.Gone;
            updateText.Visibility = ViewStates.Gone;
        }

        private void YesButtonClick(object sender, EventArgs e)
        {
            Presenter.OnYesButtonClicked();
        }

        private void NoButtonClick(object sender, EventArgs e)
        {
            Presenter.OnNoButtonClicked();
        }

        public void Report(ProgressInfo progressInfo)
        {
            RunOnUiThread(() =>
            {
                if (!progressBar.Progress.Equals(progressInfo.Progress))
                    progressBar.Progress = progressInfo.Progress;

                if (progressInfo.Max < 1)
                    progressBar.Indeterminate = true;
                else
                {
                    progressBar.Indeterminate = false;
                    progressBar.Max = progressInfo.Max;
                }

                text.Text = progressInfo.Text + " " + progressInfo.Progress + "/" + progressInfo.Max;
            });
        }
    }
}