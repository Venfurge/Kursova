using Kursova.Data.SQLiteDB;
using Kursova.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kursova.Data.ActivityStore
{
    public class SQLiteActivityStore : IActivityStore
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteActivityStore(ISQLiteDB db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<ActivityItem>();
        }

        public async Task<IEnumerable<ActivityItem>> GetActivitiesAsync()
        {
            try
            {
                return await _connection.Table<ActivityItem>().ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<ActivityItem> GetActivity(int id)
        {
            try
            {
                return await _connection.FindAsync<ActivityItem>(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task DeleteActivity(ActivityItem item)
        {
            await _connection.DeleteAsync(item);
        }

        public async Task AddActivity(ActivityItem item)
        {
            await _connection.InsertAsync(item);
        }

        public async Task UpdateActivity(ActivityItem item)
        {
            await _connection.UpdateAsync(item);
        }
    }
}
