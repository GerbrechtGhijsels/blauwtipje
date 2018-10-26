using System;
using System.ComponentModel;

namespace BlauwtipjeApp.Core.Components.Dialog
{
    public class DialogConfig
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool NotCancelable { get; set; }
        public Action OnDismiss { get; set; }
        [DefaultValue(DialogIcon.None)]
        public DialogIcon Icon { get; set; }
    }

    public enum DialogResult
    {
        None = 0,
        Positive = 1,
        Neutral = 2,
        Negative = 3
    }

    public enum DialogIcon
    {
        None = 0,
        Alert = 1,
        Dialog = 2
    }
}