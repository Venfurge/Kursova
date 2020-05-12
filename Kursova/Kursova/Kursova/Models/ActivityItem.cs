using Xamarin.Forms;

namespace Kursova.Models
{
    public class ActivityItem
    {
        public ActivityItem(int id, string name, bool check)
        {
            Id = id;
            Name = name;
            IsChecked = check;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
