using System;
using System.IO;
using WebKit;

namespace BlauwtipjeApp.iOS.Classes
{
    public class CustomWebview : UIKit.UIWebView
    {   

        private string text;

        public CustomWebview(){
            
        }

        public void SetQuestionWebviewText(String text)
        {
            this.text = text;
            //this.Settings.JavaScriptEnabled = true;

            //this.RequestFocusFromTouch();

            //this.SetWebViewClient(new CustomWebviewClient());
            //this.SetWebChromeClient(new WebChromeClient());


            //to do
            //int size = Resources.GetInteger(Resource.Integer.font_size);
            //this.Settings.DefaultFontSize = size;

            //make pop up screen
            //this.AddJavascriptInterface(new WebAppInterface(context), "Android");

            //get the html template
            string content;
            using (StreamReader sr = System.IO.File.OpenText("Resources/HtmlTemplate.html"))
            {
                content = sr.ReadToEnd();
            }
            //adds the text to the html template.
            content = String.Format(content, text);
            this.LoadData(content, "utf-8", null,null);

        }


        public string GetQuestionWebviewText()
        {
            return text;
        }
    }
}
