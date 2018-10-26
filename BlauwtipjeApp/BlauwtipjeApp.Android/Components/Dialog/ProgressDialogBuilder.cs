using System;
using Android.App;
using Android.Content;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Droid.Components.Dialog
{
    public class ProgressDialogBuilder : IProgress<ProgressInfo>
    {
        private Activity context;
        private ProgressDialog dialog;
        private ProgressDialogConfig config;

        public ProgressDialogBuilder(Activity context)
        {
            this.context = context;
        }

        public ProgressDialogConfig GetConfig()
        {
            return config;
        }

        public ProgressDialog CreateProgressDialog(ProgressDialogConfig config)
        {
            this.config = config;
            dialog = new ProgressDialog(context);
            dialog.SetTitle(config.Title);
            dialog.SetMessage(config.Message);
            dialog.SetCancelable(!config.NotCancelable);

            switch (config.Icon)
            {
                case DialogIcon.Alert:
                    dialog.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
                    break;
                case DialogIcon.Dialog:
                    dialog.SetIconAttribute(Android.Resource.Attribute.DialogIcon);
                    break;
            }

            dialog.Indeterminate = config.Indeterminate;
            dialog.SetProgressStyle(ProgressDialogStyle.Horizontal);
            dialog.Max = config.Max;

            dialog.SetOnDismissListener(new OnDismissListener(config.OnDismiss));
            return dialog;
        }

        public void Report(ProgressInfo progressInfo)
        {
            context.RunOnUiThread(() =>
            {
                if (!dialog.Progress.Equals(progressInfo.Progress))
                    dialog.Progress = progressInfo.Progress;

                if (progressInfo.Max < 1)
                {
                    dialog.Indeterminate = true;
                }
                else
                {
                    dialog.Indeterminate = false;
                    dialog.Max = progressInfo.Max;
                }

                dialog.SetMessage(progressInfo.Text);
            });
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