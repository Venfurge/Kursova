using Kursova.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kursova.ViewModels
{
    public class ActivitiesPageViewModel : BaseViewModel
    {
        private ActivityItem _selectedActivityItem;

        private bool _isClear;
        public ActivitiesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            InitializeActivityItems();
            IsClear = ActivityItems.Count > 0 ? false : true;

            ClosePageCommand = new DelegateCommand(OnClosePage);
            AddActivityCommand = new DelegateCommand(OnAddActivity);
        }

        public ObservableCollection<ActivityItem> ActivityItems { get; set; }

        public DelegateCommand ClosePageCommand { get; }
        public DelegateCommand AddActivityCommand { get; }

        public ActivityItem SelectedActivityItem
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

        private void InitializeActivityItems()
        {
            var activityItems = new ObservableCollection<ActivityItem>
            {
                new ActivityItem(1, "First activity", true),
                new ActivityItem(2, "Second activity", false),
                new ActivityItem(3, "Third activity", false),
            };

            ActivityItems = new ObservableCollection<ActivityItem>(activityItems);
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
