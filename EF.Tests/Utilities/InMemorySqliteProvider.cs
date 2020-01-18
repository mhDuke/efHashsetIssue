using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EF.Tests.Utilities
{
    public class InMemorySqliteProvider
    {
        #region private fields
        const string _connectionString = "DataSource=:memory:";
        SqliteConnection _connection;
        DbContextOptions _options;
        #endregion

        #region Constructors
        public InMemorySqliteProvider(bool initDb = true)
        {
            _connection = new SqliteConnection(_connectionString);
            _options = new DbContextOptionsBuilder().UseSqlite(_connection).Options;
            if (initDb)
                InitDb();
        }
        #endregion
        public DbContextOptions DbOptions { get => _options; }

        public void InitDb()
        {
            _connection.Open();
            using (var context = new HashsetContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        public void DestroyDb()
        {
            using (var context = new HashsetContext(_options))
            {
                context.Database.EnsureDeleted();
            }
            _connection.Close();
        }
    }
}
