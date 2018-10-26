using System;
using BlauwtipjeApp.Core.Presenters;
using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Core.Test.Fakes.Views
{
    public class FakeUpdateView : FakeBaseView, IUpdateView, IProgress<ProgressInfo>
    {
        public FakeUpdateView()
        {
            ChangeLogText = "";
            ChangeLogIsVisible = true;
            ProgressBarIsVisible = true;
        }

        public string ChangeLogText { get; private set; }
        public void SetChangeLogText(string changelog)
        {
            ChangeLogText = changelog;
        }

        public bool ChangeLogIsVisible { get; private set; }
        public void ShowChangeLog()
        {
            ChangeLogIsVisible = true;
            ProgressBarIsVisible = false;
        }

        public bool ProgressBarIsVisible { get; private set; }
        public void ShowProgressBar()
        {
            ChangeLogIsVisible = false;
            ProgressBarIsVisible = true;
        }

        public void HideEverything()
        {
            ChangeLogIsVisible = false;
            ProgressBarIsVisible = false;
        }

        public ProgressReporter GetProgressReporter()
        {
            return new ProgressReporter(this);
        }

        public ProgressInfo LastProgressInfoValueReported { get; private set; }
        public void Report(ProgressInfo value)
        {
            LastProgressInfoValueReported = value;
        }
    }
}
