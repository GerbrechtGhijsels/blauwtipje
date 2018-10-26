using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;
using Android.Util;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Droid.Components.Image;
using BlauwtipjeApp.Droid.Components.WebView;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label = "",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SlugResultActivity : BaseActivity<ResultPresenter<Slug>>, IResultView<Slug>
    {
        private TextView SlugName;
        private TextView SlugScientificName;
        private ResourceImageView SlugPicture;
        private CustomWebView SlugDescription;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);
            CustomLayoutId = IsTablet() ? Resource.Layout.activity_slugresult_tablet : Resource.Layout.activity_slugresult_phone;
            CurrentActivity = NavigableScreen.Other;
           
            CanNavigateTo = new List<NavigableScreen>
            {
                NavigableScreen.Main,
                NavigableScreen.Determination,
                NavigableScreen.SpeciesList,
                NavigableScreen.Info
            };

            base.OnCreate(savedInstanceState);

            SlugName = FindViewById<TextView>(Resource.Id.AnimalName);
            SlugScientificName = FindViewById<TextView>(Resource.Id.AnimalScientificName);
            SlugPicture = FindViewById<ResourceImageView>(Resource.Id.AnimalImage);
            SlugPicture.Click += (sender, e) => Presenter.OnResultPictureClicked();
            SlugDescription = FindViewById<CustomWebView>(Resource.Id.AnimalPage);
        }

        public int GetResultId()
        {
            var resultId = new SlugResultIntent(Intent).ResultId;
            Log.Info("BlauwtipjeApp", "RESULTID = " + resultId);
            return resultId;
        }

        public void SetResult(Slug slug)
        {
            ActivityTitle.Text = slug.DisplayName;
            SlugName.Text = slug.DisplayName;
            SlugScientificName.Text = slug.ScientificName;

            var imageList = slug.ImageList;
            if (imageList.Count > 0)
                SlugPicture.SetImageFromBytes(imageList[0].Content);
            else
                //if there is no image set it back to the default one.
                SlugPicture.SetImageToDefaultImage();

            SlugDescription.InjectHtml(slug.Text);
        }

        public class SlugResultIntent : Intent
        {
            private readonly string resultIdKey = "Result_Id";

            public SlugResultIntent(Context context) : base(context, typeof(SlugResultActivity))
            {

            }

            public SlugResultIntent(Intent intent) : base(intent)
            {

            }

            public int ResultId
            {
                get => GetIntExtra(resultIdKey, 0);
                set => PutExtra(resultIdKey, value);
            }
        }
    }
}