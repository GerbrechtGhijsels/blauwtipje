using System;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using UIKit;

namespace BlauwtipjeApp.iOS.Dialogs
{
    public class AlertDialogBuilder
    {
        
        private UIViewController context;
        private UIAlertController builder;
        private AlertDialogConfig config;

        public AlertDialogBuilder(UIViewController context)
        {
            this.context = context;
        }

        public Task<DialogResult> ShowAlertDialog(AlertDialogConfig config)
        {
            this.config = config;
            var tcs = new TaskCompletionSource<DialogResult>();
            context.InvokeOnMainThread(() => 
            {
                builder = UIAlertController.Create(config.Title,config.Message,UIAlertControllerStyle.Alert);


                switch (config.Icon)
                {
                    case DialogIcon.Alert:
                        //builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
                        //builder.PreferredStyle = UIAlertControllerStyle.Alert;
                        break;
                    case DialogIcon.Dialog:
                        //builder.SetIconAttribute(Android.Resource.Attribute.DialogIcon);
                        break;
                }

                if (!string.IsNullOrWhiteSpace(config.PositiveButtonText))
                    builder.AddAction(UIAlertAction.Create(config.PositiveButtonText, UIAlertActionStyle.Default, action =>
                    {
                        tcs.SetResult(DialogResult.Positive);
                }));

                if (!string.IsNullOrWhiteSpace(config.NeutralButtonText))
                    builder.AddAction(UIAlertAction.Create(config.NeutralButtonText,UIAlertActionStyle.Default, action =>
                    {
                        tcs.SetResult(DialogResult.Neutral);
                }));

                if (!string.IsNullOrWhiteSpace(config.NegativeButtonText))
                    builder.AddAction(UIAlertAction.Create(config.NegativeButtonText,UIAlertActionStyle.Default, action =>
                    {
                        tcs.SetResult(DialogResult.Negative);
                }));
                //builder.DismissViewController(false,HandleAction(config.OnDismiss));
                context.PresentViewController(builder, animated: true, completionHandler: HandleAction(config.OnDismiss));

            });
            return tcs.Task;
        }

        private Action HandleAction(Action onDismiss)
        {
            return onDismiss;

        }


    }
}
