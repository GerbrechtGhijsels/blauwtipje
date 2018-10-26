using BlauwtipjeApp.Core.DataAccess;
using BlauwtipjeApp.Core.Models;

namespace BlauwtipjeApp.Core.Test.Fakes.Daos
{
    public class FakeDeterminationInProgressDAO : IDeterminationInProgressDAO
    {
        public DeterminationInProgress StoredDetermination { get; set; }

        public FakeDeterminationInProgressDAO()
        {

        }

        public bool HasDeterminationInProgress()
        {
            return StoredDetermination != null;
        }

        public DeterminationInProgress GetDeterminationInProgress()
        {
            return StoredDetermination;
        }

        public void StoreDeterminationInProgress(DeterminationInProgress determinationInProgress)
        {
            StoredDetermination = determinationInProgress;
        }

        public void DeleteDeterminationInProgress()
        {
            StoredDetermination = null;
        }
    }
}