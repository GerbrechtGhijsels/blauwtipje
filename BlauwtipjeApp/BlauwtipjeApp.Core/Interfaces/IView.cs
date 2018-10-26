using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Components.Dialog;

namespace BlauwtipjeApp.Core.Interfaces
{
    public interface IView
    {
        void NavigateTo(NavigableScreen screen);
        Task<DialogResult> ShowAlertDialog(AlertDialogConfig config);
        void ShowImageGallery(List<int> imageIds);
        void ShowNotification(string text = "", int durationInMilliSeconds = 2000);
        void EndView();
        void EndApplication();
    }

    public enum NavigableScreen
    {
        Main = 0,
        Determination = 1,
        SpeciesList = 2,
        Info = 3,
        Other = 4
    }
}
