using BlauwtipjeApp.Core;
using Foundation;
using UIKit;
using BlauwtipjeApp.iOS.Helpers;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.iOS.ViewControllers;
using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate, App.INativeApp
	{
		// class-level declarations

        //private TreeStore<Slug> treeStore;
        //private UpdateService updateService;
        private NetworkHelper networkHelper;
        private bool hasCheckedForUpdate;
        private MainViewController mainViewController;

        public static AppDelegate Self { get; private set; }

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{   
            AppDelegate.Self = this;
			
            //ServiceLocator.Instance.Register<IMessageDialog, MessageDialog>();

            //Window.RootViewController = UIStoryboard.FromName("Main", null)
            //.InstantiateViewController("tabViewController");


            var test = new NativeServiceFactory();
            ServiceLocator.InitializeServices<Slug>(test);

            TreeManager<Slug>.Initialize();
           
            //dbHelper = new DBHelper(new FileHelper());
            //treeStore = new TreeStore<Slug>(dbHelper.GetResourceDatabase(), new XmlDataBaseProvider(dbHelper.GetResourceDatabase()));
            //updateService = new UpdateService(new WebResourceDatabase(), dbHelper.GetResourceDatabase());
            //networkHelper = new NetworkHelper();

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            mainViewController = new MainViewController();
            Window.RootViewController = new UINavigationController(mainViewController);
            Window.MakeKeyAndVisible();
            App.Initialize(this);
            return true;
		}

        public bool GetHasCheckedForUpdate()
        {
            return hasCheckedForUpdate;
        }

        public void SetHasCheckedForUpdate(bool hasCheckedForUpdate)
        {
            this.hasCheckedForUpdate = hasCheckedForUpdate;
        }

        public INetworkHelper GetNetworkHelper()
        {
            return networkHelper;
        }



		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

        public void Shutdown()
        {
            System.Environment.Exit(0);
        }
	}
}