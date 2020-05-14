using Kursova.Data.ActivityStore;
using Kursova.Data.SQLiteDB;
using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Kursova.ViewModels
{
    public class SelectedActivityPopupPageViewModel : BaseViewModel
    {
        private ActivityItem _activity;

        private IActivityStore _activityStore;
        public SelectedActivityPopupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _activityStore = new SQLiteActivityStore(DependencyService.Get<ISQLiteDB>());

            CorrectAndNavigateCommand = new DelegateCommand(OnCorrectAndNavigate);
            DeleteAndNavigateCommand = new DelegateCommand(OnDeleteAndNavigate);
        }
        public DelegateCommand CorrectAndNavigateCommand { get; }
        public DelegateCommand DeleteAndNavigateCommand { get; }

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

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            var id = parameters.GetValue<int>("id");
            _activity = await _activityStore.GetActivity(id);
            Text = _activity.Text;
            Name = _activity.Name;
            SliderValue = _activity.MaxResult;
        }

        private async void OnCorrectAndNavigate()
        {

            if (Name != null && Text != null && SliderValue > 0)
            {
                _activity.Name = Name;
                _activity.Text = Text;
                _activity.MaxResult = SliderValue;
                _activity.IsChecked = true;

                await _activityStore.UpdateActivity(_activity);
            }
            await NavigationService.GoBackAsync();
        }

        private async void OnDeleteAndNavigate()
        {
            _activityStore.DeleteActivity(_activity);
            await NavigationService.GoBackAsync();
        }
    }
}
