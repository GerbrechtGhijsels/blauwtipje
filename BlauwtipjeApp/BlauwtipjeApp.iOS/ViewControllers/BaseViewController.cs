using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.iOS.Dialogs;
using Foundation;
using ToastIOS;
using UIKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public abstract class BaseActivity<TPresenter> :  UIViewController,IView where TPresenter : IPresenter
    {
        // Base
        protected TPresenter Presenter;


        private UIImagePickerController imagePicker;
        private UIImage myPicture;

        // Every screen greater than "TabletTreshold" inches is considered a tablet
        private const int TabletTreshold = 6;

       
        protected NavigableScreen CurrentActivity { get; set; }
        protected bool IsNotOnMainScreen;
        private string v;
        private object p;

        protected internal BaseActivity(IntPtr handle) : base(handle)
        {
        }

        protected internal BaseActivity() : base()
        {
        }

        public BaseActivity(string v, object p)
        {
            this.v = v;
            this.p = p;
        }

        protected bool DisableNavigation { get; set; }


        public override void ViewDidLoad()
        {   
            base.ViewDidLoad();
            if (Presenter == null) throw new InvalidOperationException("Presenter is not initialized");

            Initialize();
            Presenter.OnViewCreate();
        }

        public override void ViewDidAppear(bool animated)
        { 
            base.ViewDidAppear(animated);

            Presenter.OnViewCreate();
        }

        public override void ViewWillAppear(Boolean animated){
            base.ViewDidAppear(animated);
            Presenter.OnViewStart();
            Presenter.OnViewGainsFocus();
        }

        public override void ViewWillDisappear(bool animated){
            Presenter.OnViewLosesFocus();
            Presenter.OnViewStop();
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated){
            Presenter.OnViewDestroy();
            base.ViewDidDisappear(animated);
        }

        public void Initialize(){
            IsNotOnMainScreen = CurrentActivity != NavigableScreen.Main;
        }

        public void NavigateTo(NavigableScreen screen)
        {
            if (IsNotOnMainScreen)
                this.DismissViewController(true, null);
            switch (screen)
            {
                case NavigableScreen.Main:
                    break;
                case NavigableScreen.Determination:
                    NavigationController.PushViewController(new DeterminationViewController(), true);
                    break;
                case NavigableScreen.SpeciesList:
                    NavigationController.PushViewController(new SpeciesListViewController(), true);
                    break;
                case NavigableScreen.Info:
                    NavigationController.PushViewController(new InfoViewController(), true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screen), screen, null);
            }
        }

        public Task<DialogResult> ShowAlertDialog(AlertDialogConfig config)
        {
            var builder = new AlertDialogBuilder(this);
            return builder.ShowAlertDialog(config);
        }

        public void ShowImageGallery(List<int> imageIds)
        {
            StartPhotoSelector();
        }

        public void ShowNotification(string text = "", int durationInMilliSeconds = 2000)
        {
            var toast = Toast.MakeText(text);
            toast.SetDuration(durationInMilliSeconds);
            toast.Show();
                

        }

        public void EndView()
        {
            NavigationController.PopViewController(true);
        }

        public void EndApplication()
        {
            if (IsNotOnMainScreen)
            {
                System.Environment.Exit(0);
            }
            else{
                EndView();
            }
        }

        private void StartPhotoSelector()
        {
            imagePicker = new UIImagePickerController();

            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

            imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            imagePicker.Canceled += Handle_Canceled;


            NavigationController.PresentModalViewController(imagePicker, true);
        }



        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            // get common info (shared between images and video)
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine("Url:" + referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {
                // get the original image
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");
                    myPicture = originalImage; // display
                    NavigationController.PushViewController(new DeterminationViewController(true, myPicture), true);
                }
            }
            else
            { // if it's a video
              // get video url
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            // dismiss the picker
            imagePicker.DismissViewController(true, null);

        }

        void Handle_Canceled(object sender, EventArgs e)
        {
            imagePicker.DismissViewController(true, null);
        }

        [Foundation.Export("imagePickerController:didFinishPickingImage:editingInfo:")]
        public void FinishedPickingImage(UIKit.UIImagePickerController picker, UIKit.UIImage image, Foundation.NSDictionary editingInfo)
        {
            // determine what was selected, video or image
            bool isImage = false;

            switch (editingInfo[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }


            // get common info (shared between images and video)
            NSUrl referenceURL = editingInfo[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine("Url:" + referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {
                // get the original image
                UIImage originalImage = editingInfo[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");
                    myPicture = originalImage; // display
                }
            }
            else
            { // if it's a video
              // get video url
                NSUrl mediaURL = editingInfo[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            // dismiss the picker
            imagePicker.DismissViewController(true, null);

        }

        [Foundation.Export("imagePickerControllerDidCancel:")]
        public void Canceled(UIKit.UIImagePickerController picker)
        {
            imagePicker.DismissViewController(true, null);
        }
    }
}