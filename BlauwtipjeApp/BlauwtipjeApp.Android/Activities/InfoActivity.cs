using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Content.PM;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Droid.Components.WebView;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label = "", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class InfoActivity : BaseActivity<InfoPresenter<Slug>>, IInfoView
    {
        private CustomWebView webview;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            CustomLayoutId = Resource.Layout.activity_info;
            CurrentActivity = NavigableScreen.Info;
            CanNavigateTo = new List<NavigableScreen>
            {
                NavigableScreen.Main,
                NavigableScreen.Determination,
                NavigableScreen.SpeciesList
            };
            base.OnCreate(savedInstanceState);
            ActivityTitle.Text = "Informatie";
            webview = FindViewById<CustomWebView>(Resource.Id.infoWebview);
        }

        public void SetInfoText(string text)
        {
            webview.InjectHtml(text);
        }
    }
}