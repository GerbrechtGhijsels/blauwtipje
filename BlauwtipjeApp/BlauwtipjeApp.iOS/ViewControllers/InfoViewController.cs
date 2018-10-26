using System;
using BlauwtipjeApp.Core.Helpers;
using BlauwtipjeApp.Core.Interfaces;
using BlauwtipjeApp.Core.Models.Results;
using BlauwtipjeApp.Core.Models.Tree;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.iOS.Classes;
using BlauwtipjeApp.iOS.Helpers;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class InfoViewController : BaseActivity<InfoPresenter<Slug>>, IInfoView , IWKUIDelegate
    {   

        private CustomWebviewHelper webviewHelper;
        private WKWebView tempWebview;

        public InfoViewController() : base("InfoViewController", null)
        {
        }

        [Foundation.Export("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
        public void RunJavaScriptAlertPanel(WebKit.WKWebView webView, string message, WebKit.WKFrameInfo frame, System.Action completionHandler)
        {
            var alertController = UIAlertController.Create("Info", message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alertController, true, null);

            completionHandler();
        }


        public override void ViewDidLoad()
        {   
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);


            tempWebview = new WKWebView(WebviewContainer.Frame, new WKWebViewConfiguration());
            tempWebview.UIDelegate = this;
            WebviewContainer.AddSubview(tempWebview);

             webviewHelper = new CustomWebviewHelper(tempWebview);

            base.ViewDidLoad();


         
            // Perform any additional setup after loading the view, typically from a nib.

        
        }

          


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }



        public void SetInfoText(string text)
        {   
            webviewHelper.SetCustomWebviewText(text);
        }
    }
}

