using SQLite;

namespace Kursova.Data.SQLiteDB
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
