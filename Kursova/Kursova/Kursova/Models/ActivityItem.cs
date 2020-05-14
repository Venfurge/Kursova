using SQLite;

namespace Kursova.Models
{
    public class ActivityItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
