using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Kursova.ViewModels
{
    public class SelectedActivityPopupPageViewModel : BaseViewModel
    {
        private ActivityItem _activity;

        public SelectedActivityPopupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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

        public async void InitializeOptions(int id)
        {
            _activity = await ItemsRepository.GetActivityItemByIdAsync(id);
            Text = _activity.Text;
            Name = _activity.Name;
            SliderValue = _activity.MaxResult;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var id = parameters.GetValue<int>("id");
            InitializeOptions(id);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        private async void OnCorrectAndNavigate()
        {
            if (Name != "" && Text != "" && SliderValue > 0)
            {
                _activity.Name = Name;
                _activity.Text = Text;
                _activity.MaxResult = SliderValue;
                _activity.IsChecked = true;

                await ItemsRepository.UpdateActivityItemAsync(_activity);
            }
            await NavigationService.GoBackAsync();
        }

        private async void OnDeleteAndNavigate()
        {
            await ItemsRepository.RemoveActivityItemAsync(_activity.Id);
            await NavigationService.GoBackAsync();
        }
    }
}
