using Kursova.Data;
using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Kursova.ViewModels
{
    public class SelectedActivityPopupPageViewModel : BaseViewModel
    {
        private ActivityItem _activity;

        private IItemsRepository _itemsRepository;

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

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            var id = parameters.GetValue<int>("id");
            _itemsRepository = parameters.GetValue<IItemsRepository>("repository");
            _activity = await _itemsRepository.GetActivityItemByIdAsync(id);
            Text = _activity.Text;
            Name = _activity.Name;
            SliderValue = _activity.MaxResult;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("repository", _itemsRepository);
        }

        private async void OnCorrectAndNavigate()
        {
            if (Name != "" && Text != "" && SliderValue > 0)
            {
                _activity.Name = Name;
                _activity.Text = Text;
                _activity.MaxResult = SliderValue;
                _activity.IsChecked = true;

                await _itemsRepository.UpdateActivityItemAsync(_activity);
            }
            await NavigationService.GoBackAsync();
        }

        private async void OnDeleteAndNavigate()
        {
            await _itemsRepository.RemoveActivityItemAsync(_activity.Id);
            await NavigationService.GoBackAsync();
        }
    }
}
