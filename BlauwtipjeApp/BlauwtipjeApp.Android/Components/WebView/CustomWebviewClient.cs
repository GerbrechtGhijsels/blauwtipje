using System;
using Android.Content;

namespace BlauwtipjeApp.Droid.Components.WebView
{
    public class CustomWebviewClient : Android.Webkit.WebViewClient
    {
        //for newer android versions
        public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, Android.Webkit.IWebResourceRequest request)
        {   
            var uri = request.Url;
			var intent = new Intent(Intent.ActionView, uri);
            view.Context.StartActivity(intent);
            return true;
        }

        //for older android versions
        [Obsolete("deprecated")]
        public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, string url)
        {
			var uri = Android.Net.Uri.Parse(url);
			var intent = new Intent(Intent.ActionView, uri);
			view.Context.StartActivity(intent);
            return true;
        }
	    
    }
}
