using System;
using System.IO;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Webkit;

namespace BlauwtipjeApp.Droid.Components.WebView
{
	public class CustomWebView : Android.Webkit.WebView
	{
	    private string html;
	    private string template;
	    private string content;
	    private bool isInitialized;

        protected CustomWebView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public CustomWebView(Context context) : base(context)
        {
            Initialize();
        }

        public CustomWebView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public CustomWebView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public CustomWebView(Context context, IAttributeSet attrs, int defStyleAttr, bool privateBrowsing) : base(context, attrs, defStyleAttr, privateBrowsing)
        {
            Initialize();
        }

        public CustomWebView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }



        //Makes a webview with a customWebviewcliet and csutom javascriptinterface.
        private void Initialize()
	    {
	        if (isInitialized) return;
	        this.Settings.JavaScriptEnabled = true;
	        this.RequestFocusFromTouch();

	        //this.SetWebViewClient(new CustomWebviewClient());
	        this.SetWebChromeClient(new WebChromeClient());

	        var size = Resources.GetInteger(Resource.Integer.font_size);
	        this.Settings.DefaultFontSize = size;

	        //make pop up screen
	        this.AddJavascriptInterface(new WebAppInterface(Context), "Android");

	        //get the html template
            var assets = Android.App.Application.Context.Assets;
            using (var sr = new StreamReader(assets.Open("HtmlTemplate.html")))
	        {
	            template = sr.ReadToEnd();
	        }

	        isInitialized = true;
	    }


        //Sets the text parameter into the html template to form a html page.
		public void InjectHtml(string text)
		{
            if (!isInitialized) throw new InvalidOperationException("Webview is not initialized, please call Initialize() on it.");

		    html = text;
		    content = string.Format(template, html);
            
			LoadDataWithBaseURL(null, content, "text/html", "utf-8", null);
        }

	    public string GetInjectedHtml()
	    {
	        return html;
	    }

	    public string GetContent()
	    {
	        return content;
	    }
	}
}