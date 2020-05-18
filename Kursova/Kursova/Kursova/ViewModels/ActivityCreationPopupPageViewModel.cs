using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Kursova.ViewModels
{
    public class ActivityCreationPopupPageViewModel : BaseViewModel
    {
        private ActivityItem _activity;

        public ActivityCreationPopupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        private async void OnAddAndNavigate()
        {
            if (Name != null && Text != null && SliderValue > 0)
            {
                _activity = new ActivityItem()
                {
                    Name = Name,
                    Text = Text,
                    MaxResult = SliderValue,
                    IsChecked = true
                };

                await ItemsRepository.AddActivityItemAsync(_activity);
            }
            await NavigationService.GoBackAsync();
        }
    }
}
