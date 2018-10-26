using BlauwtipjeApp.Core.Services.Update.Impl;

namespace BlauwtipjeApp.Core.Services.Update
{
    public interface IProgressReporter
    {
        void StartNewTask(string taskText);
        void SetMax(int max);
        void IncreaseProgress(int value);
        void IncreaseProgressByOne();
        void Report(ProgressInfo progressInfo);
    }
}