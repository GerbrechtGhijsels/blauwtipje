using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using BlauwtipjeApp.Core;
using BlauwtipjeApp.Core.Models.Results;

namespace BlauwtipjeApp.Droid.Activities
{
    [Activity(Label = "@string/app_name", Icon ="@drawable/blauwtipjelogo300wit", Theme = "@style/SplashTheme", MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            mainActivityIntent.AddFlags(ActivityFlags.ClearTop);
            mainActivityIntent.AddFlags(ActivityFlags.SingleTop);

            StartActivity(mainActivityIntent);
            TreeManager<Slug>.Initialize();
            Finish();
        }
    }
}