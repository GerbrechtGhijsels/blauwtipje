using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BlauwtipjeApp.Core;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Droid.Components.Dialog;
using BlauwtipjeApp.Droid.Components.NavigationDrawer;
using Math = System.Math;

namespace BlauwtipjeApp.Droid.Activities
{
    public abstract class BaseActivity<TPresenter> : AppCompatActivity, IView where TPresenter : IPresenter
    {
        // Base
        protected int CustomLayoutId;
        protected TextView ActivityTitle;
        protected TPresenter Presenter;

        // Every screen greater than "TabletTreshold" inches is considered a tablet
        private const int TabletTreshold = 6;

        // Navigation
        private List<NavigationItem> myDrawerList = new List<NavigationItem>();
        private ListView myLeftDrawer;
        private ActionBarDrawerToggle myDrawerToggle;
        private DrawerLayout myDrawerLayout;

        protected NavigableScreen CurrentActivity { get; set; }
        protected List<NavigableScreen> CanNavigateTo = new List<NavigableScreen>();
        protected bool IsNotOnMainScreen;
        protected bool DisableNavigation { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Presenter == null) throw new InvalidOperationException("Presenter is not initialized");
            if (CustomLayoutId == default(int)) throw new InvalidOperationException("No CustomLayoutId provided");
            Initialize();
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (!DisableNavigation)
            {
                myDrawerLayout.AddDrawerListener(myDrawerToggle);
                myDrawerToggle.SyncState();
            }
            Presenter.OnViewCreate();
        }

        protected override void OnStart()
        {
            base.OnStart();
            Presenter.OnViewStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            Presenter.OnViewGainsFocus();
        }

        protected override void OnPause()
        {
            Presenter.OnViewLosesFocus();
            base.OnPause();
        }

        protected override void OnStop()
        {
            Presenter.OnViewStop();
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Presenter.OnViewDestroy();
            base.OnDestroy();
        }

        private void Initialize()
        {
            IsNotOnMainScreen = CurrentActivity != NavigableScreen.Main;
            if (CanNavigateTo == null) DisableNavigation = true;

            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            var fullLayout = (LinearLayout)LayoutInflater.Inflate(Resource.Layout.activity_base, null);
            var activityMainContent = fullLayout.FindViewById<LinearLayout>(Resource.Id.MainContentContainer);
            LayoutInflater.Inflate(CustomLayoutId, activityMainContent, true);
            SetContentView(fullLayout);
            ActivityTitle = FindViewById<TextView>(Resource.Id.activityTitle);
            InitializeToolbar();
            if (!DisableNavigation) InitializeNavigationDrawer();
        }

        private void InitializeToolbar()
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myDrawerToolbar);
            SetSupportActionBar(toolbar);
        }

        private void InitializeNavigationDrawer()
        {
            myDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.myLeftDrawer);
            myLeftDrawer = FindViewById<ListView>(Resource.Id.LeftDrawer);
            myDrawerToggle = new ActionBarDrawerToggle(
                this,                           //host activity
                myDrawerLayout,                 //Layout
                Resource.String.openDrawer,     //Message when drawer is open
                Resource.String.closeDrawer);   //Message when drawer is closed


            foreach (var navigableActivity in CanNavigateTo)
                SetupMenuItem(navigableActivity);
            
            myDrawerToggle.SyncState();

            var myDrawerAdapter = new NavigationAdapter(this, myDrawerList);
            myLeftDrawer.Adapter = myDrawerAdapter;
            myLeftDrawer.ItemClick += (sender, e) => OnNavigationItemClick(e.Position);
            myLeftDrawer.BringToFront();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Pass the event to ActionBarDrawerToggle, if it returns
            // true, then it has handled the app icon touch event
            if (myDrawerToggle != null && myDrawerToggle.OnOptionsItemSelected(item))
                return true;

            if (item.ItemId == Android.Resource.Id.Home)
                OnBackPressed();
            return base.OnOptionsItemSelected(item);
        }

        private void SetupMenuItem(NavigableScreen activity)
        {
            NavigationItem itemToAdd;
            switch (activity)
            {
                case NavigableScreen.Main:
                    itemToAdd = new NavigationItem() { MenuName = "Hoofdmenu", Icon = Resource.Drawable.ic_home_white_48dp };
                    break;
                case NavigableScreen.Determination:
                    itemToAdd = new NavigationItem() { MenuName = "Determinatie", Icon = Resource.Drawable.ic_search_white_48dp };
                    break;
                case NavigableScreen.SpeciesList:
                    itemToAdd = new NavigationItem() { MenuName = "Soortenlijst", Icon = Resource.Drawable.ic_list_white_48dp };
                    break;
                case NavigableScreen.Info:
                    itemToAdd = new NavigationItem() { MenuName = "Info", Icon = Resource.Drawable.ic_info_outline_white_48dp };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(activity), activity, null);
            }
            myDrawerList.Add(itemToAdd);
        }

        private void OnNavigationItemClick(int position)
        {
            NavigateTo(CanNavigateTo[position]);
        }

        public void NavigateTo(NavigableScreen screen)
        {
            switch (screen)
            {
                case NavigableScreen.Main:
                    StartActivity(typeof(MainActivity));
                    break;
                case NavigableScreen.Determination:
                    StartActivity(typeof(DeterminationActivity));
                    break;
                case NavigableScreen.SpeciesList:
                    StartActivity(typeof(SpeciesListActivity));
                    break;
                case NavigableScreen.Info:
                    StartActivity(typeof(InfoActivity));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screen), screen, null);
            }
        }

        public bool IsTablet()
        {
            // Compute screen size
            var dm = BaseContext.Resources.DisplayMetrics;
            var screenWidth = dm.WidthPixels / dm.Xdpi;
            var screenHeight = dm.HeightPixels / dm.Ydpi;
            var size = Math.Sqrt(Math.Pow(screenWidth, 2) +
                                 Math.Pow(screenHeight, 2));
            // Tablet devices should have a screen size greater than "TabletTreshold" inches
            return size >= TabletTreshold;
        }

        public async Task<DialogResult> ShowAlertDialog(AlertDialogConfig config)
        {
            var builder = new AlertDialogBuilder(this);
            return await builder.ShowAlertDialog(config);
        }

        public void ShowNotification(string text = "", int durationInMilliSeconds = 2000)
        {
            var mainHandler = new Handler(Looper.MainLooper);
            var runnableToast = new Java.Lang.Runnable(() =>
            {
                var toast = Toast.MakeText(this, text, ToastLength.Long);
                toast.Show();
                var handler = new Handler();
                handler.PostDelayed(() => toast.Cancel(), durationInMilliSeconds);
            });
            
            mainHandler.Post(runnableToast);
        }

        public void ShowImageGallery(List<int> imageIds)
        {
            if (imageIds == null || imageIds.Count < 1)
                return;
            var intent = new ImageGalleryActivity.ImageGalleryIntent(this) { ImageIds = imageIds };
            StartActivity(intent);
        }

        public void EndView()
        {
            Finish();
        }

        public void EndApplication()
        {
            if (IsNotOnMainScreen)
                App.Shutdown();
            else
                EndView();
        }

        public override async void OnBackPressed()
        {
            var shouldCancelBackPress = await Presenter.OnBackButtonClicked();
            if (shouldCancelBackPress) return;
            base.OnBackPressed();
        }
    }
}