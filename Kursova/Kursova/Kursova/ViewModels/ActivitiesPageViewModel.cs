using Kursova.ViewModels.ItemsViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace Kursova.ViewModels
{
    public class ActivitiesPageViewModel : BaseViewModel
    {
        private ActivityItemViewModel _selectedActivityItem;

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

        private bool _isClear;
        public bool IsClear
        {
            get => _isClear;
            set
            {
                _isClear = value;
                RaisePropertyChanged();
            }
        }

        private async void InitializeOptions()
        {            
            ActivityItems.Clear();
            var activities = await ItemsRepository.GetActivityItemsAsync();
            if (activities != null)
                foreach (var activity in activities)
                    ActivityItems.Add(new ActivityItemViewModel(activity));

            IsClear = ActivityItems.Count > 0 ? false : true;
        }

        private async void UpdateActivityItems()
        {
            foreach (var activity in ActivityItems)
            {
                var currentActivity = await ItemsRepository.GetActivityItemByIdAsync(activity.Id);
                currentActivity.IsChecked = activity.IsChecked;
                await ItemsRepository.UpdateActivityItemAsync(currentActivity);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            InitializeOptions();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            UpdateActivityItems();
        }

        private async void NavigateToSelectedActivity(int activityId)
        {            
            var parameters = new NavigationParameters();
            parameters.Add("id", activityId);
            await NavigationService.NavigateAsync("SelectedActivityPopupPage", parameters, true);
        }

        private async void OnClosePage()
        {
            await NavigationService.GoBackAsync();
        }

        private async void OnAddActivity()
        {
            await NavigationService.NavigateAsync("ActivityCreationPopupPage", null, true);
        }
    }
}
