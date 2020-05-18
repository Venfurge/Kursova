using Microcharts;

namespace Kursova.Models
{
    public class ChartItem
    {
        public ChartItem(Chart chart)
        {
            Chart = chart;
        }

        public Chart Chart { get; set; }
    }
}
