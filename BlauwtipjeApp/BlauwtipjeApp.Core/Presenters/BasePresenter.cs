using System.Collections.Generic;
using System.Threading.Tasks;
using BlauwtipjeApp.Core.Interfaces;

namespace BlauwtipjeApp.Core.Presenters
{
    public class BasePresenter<TView> : IPresenter where TView : IView
    {
        protected TView View;
        protected List<NavigableScreen> CanNavigateTo;

        public BasePresenter(TView view)
        {
            View = view;
            CanNavigateTo = new List<NavigableScreen>();
        }

        public virtual Task OnViewCreate()
        {
            // Do nothing by default
            return Task.CompletedTask;
        }

        public virtual Task OnViewStart()
        {
            // Do nothing by default
            return Task.CompletedTask;
        }

        public virtual Task OnViewGainsFocus()
        {
            // Do nothing by default
            return Task.CompletedTask;
        }

        public virtual Task OnViewLosesFocus()
        {
            // Do nothing by default
            return Task.CompletedTask;
        }

        public virtual Task OnViewStop()
        {
            // Do nothing by default
            return Task.CompletedTask;
        }

        public virtual Task OnViewDestroy()
        {
            // Do nothing by default
            return Task.CompletedTask;
        }

        public virtual Task<bool> OnBackButtonClicked()
        {
            // Do nothing by default
            return Task.FromResult(false);
        }
    }
}
