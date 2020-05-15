using Kursova.Data.ActivityStore;
using Kursova.Data.SQLiteDB;
using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Kursova.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    { 
        private string _text;

        private DateTime _time;

        private bool _isActivitiesChecked;

        private SlideMenuItem _selectedMenuItem;

        private IActivityStore _activityStore;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            MenuItems = new ObservableCollection<SlideMenuItem>();
            InitializeSlideMenuOptions();
            StartPickActivitiesCommand = new DelegateCommand(OnStartPickActivities);
        }
        public ObservableCollection<SlideMenuItem> MenuItems { get; set; }

        public DelegateCommand StartPickActivitiesCommand { get; }

        public SlideMenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                _selectedMenuItem = value;

                if (_selectedMenuItem != null)
                {
                    NavigateToSelectedPage(_selectedMenuItem.Id);
                    _selectedMenuItem = null;
                }

                RaisePropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }

        private async void InitializeSlideMenuOptions()
        {
            _activityStore = new SQLiteActivityStore(DependencyService.Get<ISQLiteDB>());
            var activities = await _activityStore.GetActivitiesAsync();

            _isActivitiesChecked = false;
            if (activities != null)
                foreach (var item in activities)
                    if (item.IsChecked)
                    {
                        _isActivitiesChecked = true;
                        break;
                    }

            if (_isActivitiesChecked)
                Text = "Start Activity";
            else
                Text = "Select Activity";

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                Time = DateTime.Now);
                return true;
            });

            MenuItems.Clear();
            MenuItems.Add(new SlideMenuItem(0, "home.png", "Home"));
            MenuItems.Add(new SlideMenuItem(1, "activity.png", "Activity"));
            MenuItems.Add(new SlideMenuItem(2, "statictic.png", "Statistic"));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            InitializeSlideMenuOptions();
        }

        private async void NavigateToSelectedPage(int pageId)
        {
            switch (pageId)
            {
                case 0:
                    await NavigationService.NavigateAsync("MainPage", null, false);
                    break;
                case 1:
                    await NavigationService.NavigateAsync("ActivitiesPage", null, true);
                    break;
                case 2:
                    await NavigationService.NavigateAsync("", null, true);
                    break;
            }
        }

        private async void OnStartPickActivities()
        {
            if (_isActivitiesChecked)
                await NavigationService.NavigateAsync("ActivitiesPage", null, true);
            else
                await NavigationService.NavigateAsync("ActivitiesPage", null, true);
        }
    }
}
