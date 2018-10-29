using System;
using System.IO;
using BlauwtipjeApp.iOS.ViewControllers;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace BlauwtipjeApp.iOS.Helpers
{
    public class CustomWebviewHelper 
    {
        private string text;
        private WKWebView webview;
        private string textSize;

        public CustomWebviewHelper(WKWebView webview)
        {
            this.webview = webview;
        }


        public void SetCustomWebviewText(String text, string textSize)
        {
            if (textSize == null)
            {
                textSize = "4";
            }

            this.text = text;
            //get the html template
            string content;
            var filePath = NSBundle.MainBundle.GetUrlForResource("HtmlTemplate", "html");
            using (StreamReader sr = System.IO.File.OpenText("HtmlTemplate.html"))
            {
                content = sr.ReadToEnd();
            }
            //adds the text to the html template.
            object[] args = new object[] { text, textSize };
            content = String.Format(content,  args);


            var nsurl = NSUrl.CreateFileUrl(new[] { NSBundle.MainBundle.BundlePath });

            webview.LoadData(content, "text/html", "UTF-8", nsurl);

        }

    }
}
