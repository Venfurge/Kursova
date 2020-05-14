using Kursova.Data.SQLiteDB;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDB))]
namespace Kursova.Data.SQLiteDB
{
    public class SQLiteDB : ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "Kursova.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}
