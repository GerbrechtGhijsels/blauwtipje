using BlauwtipjeApp.Core.Models;

namespace BlauwtipjeApp.Core.DataAccess
{
    public interface IDeterminationInProgressDAO
    {
        bool HasDeterminationInProgress();
        DeterminationInProgress GetDeterminationInProgress();
        void StoreDeterminationInProgress(DeterminationInProgress determinationInProgress);
        void DeleteDeterminationInProgress();
    }
}
