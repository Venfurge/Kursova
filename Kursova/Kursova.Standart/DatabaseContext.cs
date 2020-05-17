using Kursova.Models;
using Microsoft.EntityFrameworkCore;

namespace Kursova.Standart
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ActivityItem> ActivityItems { get; set; }

        private readonly string _databasePath;

        public DatabaseContext(string databasePath)
        {
            _databasePath = databasePath;

            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
