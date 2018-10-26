using System;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree.Determination;
using BlauwtipjeApp.Core.Services.Update;
using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Core.Test.Fakes
{
    public class FakeUpdateService : IUpdateService
    {
        public string ChangeLog { get; set; }
        public bool EssentialsInPlace { get; set; }
        public bool UpdateAvailable { get; set; }

        public FakeUpdateService()
        {

        }

        public string GetChangelog()
        {
            return ChangeLog;
        }

        public bool AreEssentialsInPlace()
        {
            return EssentialsInPlace;
        }

        public bool HasCheckedForUpdate { get; private set; }
        public bool IsUpdateAvailable()
        {
            HasCheckedForUpdate = true;
            return UpdateAvailable;
        }

        public ProgressInfo UpdateReportsProgressInfo { get; set; }
        public Exception UpdateThrowsExeption { get; set; }
        public bool UpdateWasStarted { get; private set; }
        public Task DoUpdate<TResult>(IProgressReporter progressReporter) where TResult : Result
        {
            UpdateWasStarted = true;

            if (UpdateThrowsExeption != null)
                throw UpdateThrowsExeption;

            if (UpdateReportsProgressInfo != null)
                progressReporter.Report(UpdateReportsProgressInfo);

            OnUpdateCompleted?.Invoke(this, new EventArgs());
            return Task.CompletedTask;
        }

        public event EventHandler OnUpdateCompleted;
    }
}
