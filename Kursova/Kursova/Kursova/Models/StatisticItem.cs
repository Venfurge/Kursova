using System;

namespace Kursova.Models
{
    public class StatisticItem
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int ActivityId { get; set; }

        public ActivityItem Activity { get; set; }
    }
}
