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
    public partial class InfoViewController : BaseActivity<InfoPresenter<Slug>>, IInfoView , IWKUIDelegate, IWKScriptMessageHandler
    {

        const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
        WKUserContentController userController;
        private CustomWebviewHelper webviewHelper;
        private WKWebView webView;

        public InfoViewController() : base("InfoViewController", null)
        {
        }

        [Foundation.Export("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
        public void RunJavaScriptAlertPanel(WebKit.WKWebView webView, string message, WebKit.WKFrameInfo frame, System.Action completionHandler)
        {
            var alertController = UIAlertController.Create("Info", message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            UIApplication.SharedApplication.KeyWindow?.RootViewController.PresentViewController(alertController, true, null);

            completionHandler();
        }

        public override void LoadView()
        {
            base.LoadView();
            userController = new WKUserContentController();
            var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
            userController.AddUserScript(script);

            userController.AddScriptMessageHandler(this, "invokeAction");

            var config = new WKWebViewConfiguration { UserContentController = userController };
            //config.Preferences.MinimumFontSize = 10;
            //config.IgnoresViewportScaleLimits = true;
            webView = new WKWebView(View.Frame, config)
            {
            };
            webView.UIDelegate = this;

            webView.ScrollView.ScrollEnabled = true;
            webView.ScrollView.Bounces = false;

            webView.NavigationDelegate = new ExtendedWebViewDelegate();
            View.AddSubview(webView);
        }

        public override void ViewDidLoad()
        {   
            Presenter = ServiceLocator.GetService<IPresenterFactory<Slug>>().GetPresenterFor(this);


            webviewHelper = new CustomWebviewHelper(webView);

            base.ViewDidLoad();


         
            // Perform any additional setup after loading the view, typically from a nib.

        
        }

          


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }



        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            double height = Convert.ToDouble(message.Body.ToString());

            CGRect fr = webView.Frame;
            fr.Size = new CGSize(webView.Frame.Size.Width, height);
            //webView.Frame = fr;

           //var wv = this.Element as WKWebView;
            //wv.HeightRequest = height;

        }

        public void SetInfoText(string text)
        {
            webviewHelper.SetCustomWebviewText(text,"4");
        }
    }
}

