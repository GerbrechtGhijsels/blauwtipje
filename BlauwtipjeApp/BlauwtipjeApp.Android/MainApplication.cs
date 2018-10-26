using System;
using Android.App;
using Android.Net;
using Android.OS;
using Android.Runtime;
using BlauwtipjeApp.Droid.Helpers;
using Plugin.CurrentActivity;
using Android.Content;
using BlauwtipjeApp.Core;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using FFImageLoading;

namespace BlauwtipjeApp.Droid
{
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks, App.INativeApp
    {
        private bool hasCheckedForUpdate;

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);

            ImageService.Instance.Initialize();

            var test = new NativeServiceFactory((ConnectivityManager)GetSystemService(ConnectivityService));
            ServiceLocator.InitializeServices<Slug>(test);
            App.Initialize(this);
        }

        public void Shutdown()
        {
            System.Environment.Exit(0);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        /// <summary>
        /// Called when the operating system has determined that it is a good
        /// time for a process to trim unneeded memory from its process.
        /// Clears cached images. More information can be found here: 
        /// https://github.com/luberda-molinet/FFImageLoading/wiki/Advanced-Usage#clear-cache-and-memory-considerations
        /// </summary>
        //public override void OnTrimMemory(TrimMemory level)
        //{
        //    ImageService.Instance.InvalidateMemoryCache();
        //    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //    base.OnTrimMemory(level);
        //}
    }
}