using Kursova.Models;
using Prism.Mvvm;

namespace Kursova.ViewModels.ItemsViewModels
{
    public class ActivityItemViewModel : BindableBase
    {
        public int Id { get; set; }
        public ActivityItemViewModel(ActivityItem activity)
        {
            Id = activity.Id;
            Name = activity.Name;
            IsChecked = activity.IsChecked;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
            }
        }
    }
}
