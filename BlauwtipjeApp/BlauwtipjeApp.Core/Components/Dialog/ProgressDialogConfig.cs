using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Core.Components.Dialog
{
    public class ProgressDialogConfig : DialogConfig
    {
        public bool Indeterminate { get; set; }
        public int Max { get; set; }
        public ProgressReporter ProgressReporter { get; set; }
    }
}
