using System;

namespace BlauwtipjeApp.Core.Services.Update.Impl
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly ProgressInfo _progressInfo;
        private readonly IProgress<ProgressInfo> _progress;

        public ProgressReporter(IProgress<ProgressInfo> progress)
        {
            _progress = progress;
            _progressInfo = new ProgressInfo
            {
                Text = "",
                Max = 0,
                Progress = 0
            };
        }

        public void StartNewTask(string taskText)
        {
            _progressInfo.Text = taskText;
            _progressInfo.Max = 0;
            _progressInfo.Progress = 0;
            Report(_progressInfo);
        }

        public void SetMax(int max)
        {
            _progressInfo.Max = max;
            Report(_progressInfo);
        }

        public void IncreaseProgress(int value)
        {
            _progressInfo.Progress += value;
            Report(_progressInfo);
        }

        public void IncreaseProgressByOne()
        {
            _progressInfo.Progress++;
            Report(_progressInfo);
        }

        public void Report(ProgressInfo progressInfo)
        {
            _progress.Report(progressInfo);
        }
    }
}
