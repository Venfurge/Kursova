using Xamarin.Forms;

namespace Kursova.Models
{
    public class SlideMenuItem
    {
        public SlideMenuItem(int id, ImageSource icon, string text)
        {
            Id = id;
            Icon = icon;
            Text = text;
        }

        public int Id { get; set; }
        public ImageSource Icon { get; set; }
        public string Text { get; set; }
    }
}
