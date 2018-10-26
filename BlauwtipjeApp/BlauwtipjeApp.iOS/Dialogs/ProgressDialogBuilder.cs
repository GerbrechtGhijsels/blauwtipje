using System;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Services.Update.Impl;
using CoreGraphics;
using UIKit;

namespace BlauwtipjeApp.iOS.Dialogs
{
    public class ProgressDialogBuilder 
    {
        private UIViewController context;
        public UIAlertController dialog;
        private ProgressDialogConfig config;

        public ProgressDialogBuilder(UIViewController context)
        {
            this.context = context;
        }

        public ProgressDialogConfig GetConfig()
        {
            return config;
        }

        public string Text { get; set; }
        public int Progress { get; set; } = 0;
        public int Max { get; set; }

        public void CreateProgressDialog()
        {
            
            context.InvokeOnMainThread(() =>
            {
            dialog = UIAlertController.Create(title: null, message: "Please wait...", preferredStyle: UIAlertControllerStyle.Alert);

            var loadingIndicator = new UIActivityIndicatorView(new CGRect(10,  5, 50, 50));
            loadingIndicator.HidesWhenStopped = true;
            loadingIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            loadingIndicator.StartAnimating();

                context.PresentViewController(dialog, animated: true, completionHandler: completionHandler);
            });
        }


        public void Show(){
            context.InvokeOnMainThread(() =>
            {
                context.PresentViewController(dialog, animated: true, completionHandler: completionHandler);
            });
        }

        public void Hide() {
            dialog.DismissViewController(false, null);

        }

        private void completionHandler()
        {
            //if(Progress == Max){
            //    Hide();
            //}

        }


    }
}
