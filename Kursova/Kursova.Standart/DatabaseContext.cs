using Kursova.Models;
using Microsoft.EntityFrameworkCore;

namespace Kursova.Standart
{
    public class DatabaseContext : DbContext
    {
        private readonly string _databasePath;

        public DbSet<ActivityItem> ActivityItems { get; set; }
        public DbSet<StatisticItem> StatisticItems { get; set; }

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
