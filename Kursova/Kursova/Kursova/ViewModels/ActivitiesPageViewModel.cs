using Kursova.Data;
using Kursova.ViewModels.ItemsViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace Kursova.ViewModels
{
    public class ActivitiesPageViewModel : BaseViewModel
    {
        private ActivityItemViewModel _selectedActivityItem;

        private IItemsRepository _itemsRepository;

        private bool _isClear;

        public ActivitiesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ActivityItems = new ObservableCollection<ActivityItemViewModel>();

            ClosePageCommand = new DelegateCommand(OnClosePage);
            AddActivityCommand = new DelegateCommand(OnAddActivity);
        }

        public ObservableCollection<ActivityItemViewModel> ActivityItems { get; set; }

        public DelegateCommand ClosePageCommand { get; }

        public DelegateCommand AddActivityCommand { get; }

        public ActivityItemViewModel SelectedActivityItem
        {
            get => _selectedActivityItem;
            set
            {
                _selectedActivityItem = value;

                if (_selectedActivityItem != null)
                {
                    NavigateToSelectedActivity(_selectedActivityItem.Id);
                    _selectedActivityItem = null;
                }

                RaisePropertyChanged();
            }
        }

        public bool IsClear
        {
            get => _isClear;
            set
            {
                _isClear = value;
                RaisePropertyChanged();
            }
        }

        private async void InitializeActivityItems()
        {
            ActivityItems.Clear();
            var activities = await _itemsRepository.GetActivityItemsAsync();
            if (activities != null)
                foreach (var activity in activities)
                    ActivityItems.Add(new ActivityItemViewModel(activity));
            IsClear = ActivityItems.Count > 0 ? false : true;
        }

        private async void UpdateActivityItems()
        {
            foreach (var activity in ActivityItems)
            {
                var currentActivity = await _itemsRepository.GetActivityItemByIdAsync(activity.Id);
                currentActivity.IsChecked = activity.IsChecked;
                await _itemsRepository.UpdateActivityItemAsync(currentActivity);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _itemsRepository = parameters.GetValue<IItemsRepository>("repository");
            InitializeActivityItems();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("repository", _itemsRepository);
        }

        private async void NavigateToSelectedActivity(int activityId)
        {
            UpdateActivityItems();
            var parameters = new NavigationParameters();
            parameters.Add("id", activityId);
            await NavigationService.NavigateAsync("SelectedActivityPopupPage", parameters, true);
        }

        private async void OnClosePage()
        {
            UpdateActivityItems();
            await NavigationService.GoBackAsync();
        }

        private async void OnAddActivity()
        {
            UpdateActivityItems();
            var parameters = new NavigationParameters();
            if (ActivityItems.Count != 0)
                parameters.Add("correctId", ActivityItems[ActivityItems.Count - 1].Id + 1);
            else
                parameters.Add("correctId", 1);
            await NavigationService.NavigateAsync("ActivityCreationPopupPage", parameters, true);
        }
    }
}
