using Kursova.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace Kursova.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private SlideMenuItem _selectedMenuItem;

        public MainPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            InitializeSlideMenuOptions();
        }
        public ObservableCollection<SlideMenuItem> MenuItems { get; set; }

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

        private void InitializeSlideMenuOptions()
        {
            var menuItems = new ObservableCollection<SlideMenuItem>
            {
                new SlideMenuItem(0, "home.png", "Home"),
                new SlideMenuItem(1, "activity.png", "Activity"),
                new SlideMenuItem(2, "statictic.png", "Statistic"),
            };
                
            MenuItems = new ObservableCollection<SlideMenuItem>(menuItems);
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
    }
}
