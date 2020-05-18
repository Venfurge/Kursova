using Kursova.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Kursova.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    { 
        private bool _isActivitiesChecked;

        private SlideMenuItem _selectedMenuItem;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            MenuItems = new ObservableCollection<SlideMenuItem>();
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

        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }

        private async void InitializeOptions()
        {
            var activities = await ItemsRepository.GetActivityItemsAsync();
            _isActivitiesChecked = activities.FirstOrDefault(v => v.IsChecked) != null ? true : false;

            Text = _isActivitiesChecked ? "Start Activity" : "Select Activity";

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
            base.OnNavigatedTo(parameters);
            InitializeOptions();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);            
        }

        private async void NavigateToSelectedPage(int pageId)
        {
            switch (pageId)
            {
                case 0:
                    var parameters = new NavigationParameters();
                    parameters.Add("repository", ItemsRepository);

                    await NavigationService.NavigateAsync("MainPage", parameters, false);
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
                await NavigationService.NavigateAsync("ActivityCompletingPopupPage", null, true);
            else
                await NavigationService.NavigateAsync("ActivitiesPage", null, true);
        }
    }
}
