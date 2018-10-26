using Android.Content;
using Android.Widget;
using Java.Interop;

namespace BlauwtipjeApp.Droid.Components.WebView
{
    public class WebAppInterface : Java.Lang.Object, Java.Lang.IRunnable
    {
        private Context mContext;

        /** Instantiate the interface and set the context */
        public WebAppInterface(Context c)
        {
            mContext = c;
        }

        /** Show a Message box from the web page  using the abbr touch javascript*/
        [Export("androidAlert")]
        public void AndroidAlert(string message)
        {   

            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(mContext);
            string title = "Info";
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("Ok", (senderAlert, args) =>
            {
            });
            Android.App.Dialog dialog = alert.Create();

            dialog.Show();
            TextView textView = (TextView)dialog.FindViewById(Android.Resource.Id.Message);
            textView.TextSize = mContext.Resources.GetInteger(Resource.Integer.font_size);
         }

        public void Run()
        {
        }
    }
}