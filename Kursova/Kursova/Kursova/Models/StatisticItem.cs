namespace Kursova.Models
{
    public class StatisticItem
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public int ActivityId { get; set; }

        public ActivityItem Activity { get; set; }
    }
}
