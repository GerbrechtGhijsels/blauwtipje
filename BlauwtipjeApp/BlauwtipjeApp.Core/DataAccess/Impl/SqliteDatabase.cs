using BlauwtipjeApp.Core.Models;
using BlauwtipjeApp.Core.Models.FileManagement;
using SQLite;

namespace BlauwtipjeApp.Core.DataAccess.Impl
{
    public abstract class SqliteDatabase
    {
        protected readonly string dbPath;

        protected SqliteDatabase(string dbPath)
        {
            this.dbPath = dbPath;
            using (var db = new SQLiteConnection(dbPath))
            {
                if (!TableExists("resources", db))
                    db.CreateTable<Resource>();
                if (!TableExists("determinationInProgress", db))
                    db.CreateTable<DeterminationInProgress>();
            }
        }

        private bool TableExists(string tableName, SQLiteConnection connection)
        {
            var cmd = connection.CreateCommand(
                "SELECT COUNT(*) AS tableCount FROM sqlite_master WHERE type = 'table' AND name = ?", tableName);
            return cmd.ExecuteScalar<int>() != 0;
        }
    }
}
