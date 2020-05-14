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
            InitializeActivityItems();

            IsClear = ActivityItems.Count > 0 ? false : true;

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
                    //NavigateToSelectedActivity(_selectedActivityItem.Id);
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
            ActivityItems = new ObservableCollection<ActivityItemViewModel>();
            var activities = await _activityStore.GetActivitiesAsync();
            if (activities != null)
                foreach (var activity in activities)
                    ActivityItems.Add(new ActivityItemViewModel(activity));
        }

        private async void NavigateToSelectedActivity(int activityId)
        {
            //Navigation to selected activity with navigation parameter(id)
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
