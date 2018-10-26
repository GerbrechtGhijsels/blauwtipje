using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;
using BlauwtipjeApp.Core.Interfaces;

namespace BlauwtipjeApp.Core.Test.Fakes.Views
{
    public class FakeBaseView : IView
    {
        public NavigableScreen LastNavigatedScreen { get; private set; }
        public void NavigateTo(NavigableScreen screen)
        {
            LastNavigatedScreen = screen;
        }

        public DialogResult DefaultDialogResult = DialogResult.None;
        public AlertDialogConfig LastDialogConfigShown { get; private set; }
        public Task<DialogResult> ShowAlertDialog(AlertDialogConfig config)
        {
            LastDialogConfigShown = config;
            return Task.FromResult(DefaultDialogResult);
        }

        public bool ImageGalleryShown { get; private set; }
        public List<int> LastImageGalleryIdsShown { get; private set; }
        public void ShowImageGallery(List<int> imageIds)
        {
            ImageGalleryShown = true;
            LastImageGalleryIdsShown = imageIds;
        }

        public string LastNotificationShown { get; private set; }
        public void ShowNotification(string text, int durationInMilliSeconds)
        {
            LastNotificationShown = text;
        }

        public bool ViewEnded { get; private set; }
        public void EndView()
        {
            ViewEnded = true;
        }

        public bool ApplicationEnded { get; private set; }
        public void EndApplication()
        {
            ApplicationEnded = true;
        }
    }
}
