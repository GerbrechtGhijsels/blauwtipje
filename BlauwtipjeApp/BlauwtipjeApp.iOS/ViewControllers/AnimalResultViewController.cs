using System;
using System.IO;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.iOS.Helpers;
using Foundation;
using UIKit;
using WebKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class AnimalResultViewController : BaseActivity<ResultPresenter<Slug>>, IResultView<Slug> , IWKUIDelegate
    {
        private int slugId;
        private bool noPictureSelected;
        private string previousView;
        private CustomWebviewHelper webviewHelper;
        private WKWebView tempWebview;

        public AnimalResultViewController() : base("AnimalResultViewController", null)
        {
        }

        public AnimalResultViewController(int slugId, bool noPictureSelected, string previousView)
        {
            this.slugId = slugId;
            this.noPictureSelected = noPictureSelected;
            this.previousView = previousView;

        }

        public AnimalResultViewController(int slugId)
        {
            this.slugId = slugId;
        }

        public override void ViewDidLoad()
        {   
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);

            tempWebview = new WKWebView(this.View.Frame, new WKWebViewConfiguration());
            tempWebview.UIDelegate = this;
            tempWebview.ScrollView.ScrollEnabled = false;
            WebviewContainer.AddSubview(tempWebview);

            webviewHelper = new CustomWebviewHelper(tempWebview);

            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.



            //webview.LoadData(webviewContent, "utf-8", null, null);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public int GetResultId()
        {
            return slugId;
        }

        public void SetResult(Slug slug)
        {
            //ActivityTitle.Text = slug.DisplayName;
            LblDisplayName.Text = slug.DisplayName;
            LblScientificName.Text = slug.ScientificName;

            var imageList = slug.ImageList;
            if (imageList.Count > 0)
            {
                var decoder = new WebP.Touch.WebPCodec();
                var image = decoder.Decode(slug.ImageList[0].Content);
                //NSData data = NSData.FromArray(slug.ImageList[0].Content);
                //UIImage image = UIImage.LoadFromData(data);
                SpeciesImage.Image = image;



            }
            else
            {
                //if there is no image set it back to the default one.
                //SpeciesImage;
            }

            webviewHelper.SetCustomWebviewText(slug.Text, null);
        }

        [Foundation.Export("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
        public void RunJavaScriptAlertPanel(WebKit.WKWebView webView, string message, WebKit.WKFrameInfo frame, System.Action completionHandler)
        {
            var alertController = UIAlertController.Create("Info", message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alertController, true, null);

            completionHandler();
        }
    }
}

