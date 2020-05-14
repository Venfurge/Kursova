using Kursova.Data.ActivityStore;
using Kursova.Data.SQLiteDB;
using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Kursova.ViewModels
{
    public class ActivityCreationPopupPageViewModel : BaseViewModel
    {
        private ActivityItem _activity;

        private IActivityStore _activityStore;

        public ActivityCreationPopupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _activityStore = new SQLiteActivityStore(DependencyService.Get<ISQLiteDB>());
            AddAndNavigateCommand = new DelegateCommand(OnAddAndNavigate);
        }

        public DelegateCommand AddAndNavigateCommand { get; }

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

        private int _sliderValue;
        public int SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                RaisePropertyChanged();
            }
        }

        private async void OnAddAndNavigate()
        {
            ////drop ActivityItem table
            //SQLiteDB db = new SQLiteDB();
            //SQLiteAsyncConnection connection = db.GetConnection();
            //await connection.DropTableAsync<ActivityItem>();

            if (Name != null && Text != null && SliderValue > 0)
            {
                _activity = new ActivityItem();
                _activity.Name = Name;
                _activity.Text = Text;
                _activity.MaxResult = SliderValue;
                _activity.IsChecked = true;
                _activity.Id = 1;

                await _activityStore.AddActivity(_activity);
            }
            await NavigationService.GoBackAsync();
        }
    }
}
