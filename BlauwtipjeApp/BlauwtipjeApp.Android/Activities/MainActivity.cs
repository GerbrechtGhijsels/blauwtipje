using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Droid.Components.Image;
using FFImageLoading.Transformations;
using BlauwtipjeApp.Core;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label = "",
        LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity<MainPresenter>, IMainView
    {
        private const int ClicksNeededForTogglingDebugMode = 10;
        private bool DebugMode;

        private ResourceImageView companyLogo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            CustomLayoutId = Resource.Layout.activity_main;
            CurrentActivity = NavigableScreen.Main;
            CanNavigateTo = new List<NavigableScreen>
            {
                NavigableScreen.Determination,
                NavigableScreen.SpeciesList,
                NavigableScreen.Info
            };
            base.OnCreate(savedInstanceState);
            ActivityTitle.Text = "Welkom bij Blauwtipje!";

            //Searching buttons in layout
            var buttonDetermination = FindViewById<Button>(Resource.Id.myButtonDeterminatie);
            var buttonInfo = FindViewById<Button>(Resource.Id.myButtonInfo);
            var buttonListOfSpecies = FindViewById<Button>(Resource.Id.myButtonSoortenlijst);

            //Making events for the buttons 
            buttonDetermination.Click += (sender, e) =>
            {
                Presenter.OnDeterminationButtonClicked();
            };
            buttonListOfSpecies.Click += (sender, e) =>
            {
                Presenter.OnSpeciesListButtonClicked();
            };
            buttonInfo.Click += (sender, e) =>
            {
                Presenter.OnInfoButtonClicked();
            };
            DebugMode = Settings.DebugMode;
            companyLogo = FindViewById<ResourceImageView>(Resource.Id.companyLogo);
            companyLogo.Click += CompanyLogoOnClick;

            if (DebugMode)
                companyLogo.SetBackgroundColor(Color.Black);
        }

        private int _debugModeToggleCounter;
        private async void CompanyLogoOnClick(object o, EventArgs eventArgs)
        {
            _debugModeToggleCounter++;
            if (_debugModeToggleCounter == ClicksNeededForTogglingDebugMode)
            {
                if (DebugMode)
                {
                    companyLogo.SetBackgroundColor(Color.White);
                    await Presenter.OnTurnOffDebugModeRequest();
                }
                else
                {
                    companyLogo.SetBackgroundColor(Color.Black);
                    await Presenter.OnTurnOnDebugModeRequest();
                }
                DebugMode = Settings.DebugMode;
                _debugModeToggleCounter = 0;
            }
            else
            {
                if (_debugModeToggleCounter > 4)
                    ShowNotification("Nog " + 
                                     (ClicksNeededForTogglingDebugMode - _debugModeToggleCounter) +
                                     " ticks nodig", 800);
            }
        }

        protected override void OnResume()
        {
            _debugModeToggleCounter = 0;
            base.OnResume();
        }

        public void SetRandomPicture(byte[] picture)
        {
            if (picture == null) return;
           
            companyLogo.SetImageFromBytes(picture, new ImageTransformation
            {
                Width = 300,
                Height = 300
            });
        }

        public void OpenUpdateScreen()
        {
            StartActivity(typeof(UpdateActivity));
        }

        public List<Image> GetRandomPicturesForOnMainScreen()
        {
            return TreeManager<Slug>.GetTree()?.Images.GetRange(0, 3);
        }
    }
}
