using System;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.iOS.Classes;
using BlauwtipjeApp.iOS.Helpers;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class DeterminationViewCell : UITableViewCell , IWKUIDelegate
    {
        public static readonly NSString Key = new NSString("DeterminationViewCell");
        public static readonly UINib Nib;
        private Foundation.NSIndexPath indexPath;
        private ChoiceTable context;
        private Choice choice;
        public event EventHandler<int> OnChoiceSelected;
        private CustomWebviewHelper webviewHelper;
        private WKWebView tempWebview;

        static DeterminationViewCell()

        {
            Nib = UINib.FromName("DeterminationViewCell", NSBundle.MainBundle);
        }

        protected DeterminationViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public DeterminationViewCell(ChoiceTable context)
        {
            this.context = context;


            //webviewHelper = new CustomWebviewHelper(tempWebview);


        }







        public void PopulateCell(Choice choice, Foundation.NSIndexPath indexPath,ChoiceTable context)
        {

            //tempWebview = new WKWebView(WebviewContainer.Frame, new WKWebViewConfiguration());
            tempWebview = new WKWebView(this.Frame, new WKWebViewConfiguration());
            tempWebview.UIDelegate = this;
            tempWebview.ScrollView.ScrollEnabled = false;
            tempWebview.BackgroundColor = UIColor.Blue;
            WebviewContainer.AddSubview(tempWebview);
            this.AddSubview(tempWebview);

            this.context = context;
            this.indexPath = indexPath;
            this.choice = choice;
            webviewHelper = new CustomWebviewHelper(tempWebview);
            webviewHelper.SetCustomWebviewText(choice.Text, null);
            var decoder = new WebP.Touch.WebPCodec();
            var image = decoder.Decode(choice.ImageList[0].Content);

            //NSData data = NSData.FromArray(choice.ImageList[0].Content);
            //UIImage image = UIImage.LoadFromData(data);
            QuestionImage.Image = image;

            BtnSelect.TouchUpInside += (sender, e) =>
            {
                OnChoiceSelected?.Invoke(sender, indexPath.Row);
            };
        }

        [Foundation.Export("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
        public void RunJavaScriptAlertPanel(WebKit.WKWebView webView, string message, WebKit.WKFrameInfo frame, System.Action completionHandler)
        {
            var alertController = UIAlertController.Create("Info", message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alertController, true, null);

            completionHandler();
        }
    }
}
