using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using BlauwtipjeApp.Core.Components.Dialog;

namespace BlauwtipjeApp.Droid.Components.Dialog
{
    public class AlertDialogBuilder
    {
        private Activity context;
        private AlertDialog.Builder builder;
        private AlertDialogConfig config;

        public AlertDialogBuilder(Activity context)
        {
            this.context = context;
        }

        public Task<DialogResult> ShowAlertDialog(AlertDialogConfig config)
        {
            this.config = config;
            var tcs = new TaskCompletionSource<DialogResult>();
            context.RunOnUiThread(() =>
            {
                builder = new AlertDialog.Builder(context);
                builder.SetTitle(config.Title);
                builder.SetMessage(config.Message);
                builder.SetCancelable(!config.NotCancelable);

                switch (config.Icon)
                {
                    case DialogIcon.Alert:
                        builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
                        break;
                    case DialogIcon.Dialog:
                        builder.SetIconAttribute(Android.Resource.Attribute.DialogIcon);
                        break;
                }

                if (!string.IsNullOrWhiteSpace(config.PositiveButtonText))
                    builder.SetPositiveButton(config.PositiveButtonText, (senderAlert, args) =>
                    {
                        tcs.SetResult(DialogResult.Positive);
                    });

                if (!string.IsNullOrWhiteSpace(config.NeutralButtonText))
                    builder.SetNeutralButton(config.NeutralButtonText, (senderAlert, args) =>
                    {
                        tcs.SetResult(DialogResult.Neutral);
                    });

                if (!string.IsNullOrWhiteSpace(config.NegativeButtonText))
                    builder.SetNegativeButton(config.NegativeButtonText, (senderAlert, args) =>
                    {
                        tcs.SetResult(DialogResult.Negative);
                    });

                builder.SetOnDismissListener(new OnDismissListener(config.OnDismiss));
                builder.Show();

            });
            return tcs.Task;
        }

        private sealed class OnDismissListener : Java.Lang.Object, IDialogInterfaceOnDismissListener
        {
            private readonly Action action;

            public OnDismissListener(Action action)
            {
                this.action = action;
            }

            public void OnDismiss(IDialogInterface dialog)
            {
                action?.Invoke();
            }
        }
    }
}