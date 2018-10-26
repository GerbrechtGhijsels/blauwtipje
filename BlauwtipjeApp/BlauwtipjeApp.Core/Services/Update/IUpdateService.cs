using System;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Models.Tree.Determination;

namespace BlauwtipjeApp.Core.Services.Update
{
    public interface IUpdateService
    {
        string GetChangelog();
        bool AreEssentialsInPlace();
        bool IsUpdateAvailable();
        Task DoUpdate<TResult>(IProgressReporter progressReporter) where TResult : Result;
        event EventHandler OnUpdateCompleted;
    }
}
