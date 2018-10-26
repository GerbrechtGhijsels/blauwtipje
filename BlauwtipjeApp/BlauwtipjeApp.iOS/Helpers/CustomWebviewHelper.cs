using System;
using System.IO;
using BlauwtipjeApp.iOS.ViewControllers;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace BlauwtipjeApp.iOS.Helpers
{
    public class CustomWebviewHelper : IWKNavigationDelegate 
    {
        private string text;
        private WKWebView webview;

        public CustomWebviewHelper(WKWebView webview)
        {
            this.webview = webview;
            this.webview.NavigationDelegate = new CustomWKNavigationDelegate();
        }


        public class CustomWKNavigationDelegate : WKNavigationDelegate
        {
            public CustomWKNavigationDelegate()
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


        public IntPtr Handle => throw new NotImplementedException();



        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetCustomWebviewText(String text)
        {
            this.text = text;


            //to do
            //int size = Resources.GetInteger(Resource.Integer.font_size);
            //this.Settings.DefaultFontSize = size;


            //get the html template
            string content;
            var filePath = NSBundle.MainBundle.GetUrlForResource("HtmlTemplate", "html");
            using (StreamReader sr = System.IO.File.OpenText("HtmlTemplate.html"))
            {
                content = sr.ReadToEnd();
            }
            //adds the text to the html template.
            content = String.Format(content, text);
            //this.LoadData(content, "utf-8", null, null);



            var nsurl = NSUrl.CreateFileUrl(new[] { NSBundle.MainBundle.BundlePath });

            webview.LoadData(content, "text/html", "UTF-8", nsurl);

        }

    }
}
