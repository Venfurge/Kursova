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
            Text = activity.Text;
            MaxResult = activity.MaxResult;
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

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private int _maxResult;
        public int MaxResult
        {
            get => _maxResult;
            set
            {
                _maxResult = value;
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
