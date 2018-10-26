using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V4.View;
using BlauwtipjeApp.Core;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Droid.Classes;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label = "")]
    public class ImageGalleryActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var intent = new ImageGalleryIntent(Intent);
            var imageIdList = intent.ImageIds;
            var title = intent.Title;
            var tree = TreeManager<Slug>.GetTree();

            var imageList = new List<Image>();
            foreach (var id in imageIdList)
            {
                var image = tree.Images.Find(i => i.XmlId == id);
                imageList.Add(image);
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_imagegallery);

            var mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            mToolbar.SetNavigationIcon(Resource.Drawable.ic_arrow_back_white_24dp);
            SupportActionBar.SetHomeButtonEnabled(true);

            SupportActionBar.Title = title;
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            ImageAdapter adapter = new ImageAdapter(this, imageList);
            viewPager.Adapter = adapter;
        }

        public override void OnBackPressed()
        {
            Finish();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
            }
            return base.OnOptionsItemSelected(item);
        }

        public class ImageGalleryIntent : Intent
        {
            private readonly string titleKey = "Title";
            private readonly string imageIdsKey = "Image_Ids";

            public ImageGalleryIntent(Context context) : base(context, typeof(ImageGalleryActivity))
            {

            }

            public ImageGalleryIntent(Intent intent) : base(intent)
            {

            }

            public string Title
            {
                get => GetStringExtra(titleKey);
                set => PutExtra(titleKey, value);
            }

            public List<int> ImageIds
            {
                get => new List<int>(GetIntArrayExtra(imageIdsKey));
                set => PutExtra(imageIdsKey, value.ToArray());
            }
        }
    }
}