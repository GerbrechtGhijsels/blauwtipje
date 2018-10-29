using System;
using Foundation;
using UIKit;
using WebKit;

namespace BlauwtipjeApp.iOS.Helpers
{
    public class ExtendedWebViewDelegate : WKNavigationDelegate
    {
      
        public ExtendedWebViewDelegate()
        {

        }


        public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
        {
            if (navigationAction.NavigationType == WKNavigationType.LinkActivated)
            {
                if (UIApplication.SharedApplication.CanOpenUrl(new NSUrl(navigationAction.Request.Url.ToString())))
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(navigationAction.Request.Url.ToString()));
                    decisionHandler(WKNavigationActionPolicy.Cancel);
                }
            }
            else
            {
                decisionHandler(WKNavigationActionPolicy.Allow);
            }
        }
    }
}