using Kursova.Data;
using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Kursova.ViewModels
{
    public class ActivityCreationPopupPageViewModel : BaseViewModel
    {
        private ActivityItem _activity;

        private IItemsRepository _itemsRepository;

        public ActivityCreationPopupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            AddAndNavigateCommand = new DelegateCommand(OnAddAndNavigate);
        }

        public DelegateCommand AddAndNavigateCommand { get; }

        public int CorrectId { get; set; }

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
            _itemsRepository = parameters.GetValue<IItemsRepository>("repository");
            CorrectId = parameters.GetValue<int>("correctId");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("repository", _itemsRepository);
        }

        private async void OnAddAndNavigate()
        {
            if (Name != null && Text != null && SliderValue > 0)
            {
                _activity = new ActivityItem();
                _activity.Name = Name;
                _activity.Text = Text;
                _activity.MaxResult = SliderValue;
                _activity.IsChecked = true;
                _activity.Id = CorrectId;

                await _itemsRepository.AddActivityItemAsync(_activity);
            }
            await NavigationService.GoBackAsync();
        }
    }
}
