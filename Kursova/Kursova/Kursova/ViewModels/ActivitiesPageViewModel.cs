using Kursova.Data.ActivityStore;
using Kursova.Data.SQLiteDB;
using Kursova.ViewModels.ItemsViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Kursova.ViewModels
{
    public class ActivitiesPageViewModel : BaseViewModel
    {
        private ActivityItemViewModel _selectedActivityItem;

        private IActivityStore _activityStore;

        private bool _isClear;
        public ActivitiesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _activityStore = new SQLiteActivityStore(DependencyService.Get<ISQLiteDB>());
            ActivityItems = new ObservableCollection<ActivityItemViewModel>();
            InitializeActivityItems();

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
            var activities = await _activityStore.GetActivitiesAsync();
            if (activities != null)
                foreach (var activity in activities)
                    ActivityItems.Add(new ActivityItemViewModel(activity));
            IsClear = ActivityItems.Count > 0 ? false : true;
        }

        private async void UpdateActivityItems()
        {
            foreach (var activity in ActivityItems)
            {
                var currentActivity = await _activityStore.GetActivity(activity.Id);
                currentActivity.IsChecked = activity.IsChecked;
                await _activityStore.UpdateActivity(currentActivity);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            InitializeActivityItems();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            UpdateActivityItems();
        }

        private async void NavigateToSelectedActivity(int activityId)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("id", activityId);
            await NavigationService.NavigateAsync("SelectedActivityPopupPage", navigationParameters, true);
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
