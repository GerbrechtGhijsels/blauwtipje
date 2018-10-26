using System.Threading.Tasks;

namespace BlauwtipjeApp.Core.Interfaces
{
    public interface IPresenter
    {
        Task OnViewCreate();
        Task OnViewStart();
        Task OnViewGainsFocus();
        Task OnViewLosesFocus();
        Task OnViewStop();
        Task OnViewDestroy();
        Task<bool> OnBackButtonClicked();
    }
}