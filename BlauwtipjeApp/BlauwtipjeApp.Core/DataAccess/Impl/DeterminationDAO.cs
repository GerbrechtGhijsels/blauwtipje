using BlauwtipjeApp.Core.Models;
using SQLite;

namespace BlauwtipjeApp.Core.DataAccess.Impl
{
    public class DeterminationInProgressDAO : SqliteDatabase, IDeterminationInProgressDAO
    {
        public DeterminationInProgressDAO(string dbPath) : base(dbPath)
        {

        }

        public bool HasDeterminationInProgress()
        {
            return GetDeterminationInProgress() != null;
        }

        public DeterminationInProgress GetDeterminationInProgress()
        {
            DeterminationInProgress determinationInProgress;

            using (var db = new SQLiteConnection(dbPath))
            {
                determinationInProgress = db.Table<DeterminationInProgress>().FirstOrDefault();
            }

            return determinationInProgress;
        }
        public void StoreDeterminationInProgress(DeterminationInProgress determinationInProgress)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                DeleteDeterminationInProgress();
                db.Insert(determinationInProgress);
            }
        }
        public void DeleteDeterminationInProgress()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.DeleteAll<DeterminationInProgress>();
            }
        }
    }
}
