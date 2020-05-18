using Kursova.ViewModels.ItemsViewModels;
using Microcharts;
using Prism.Commands;
using Prism.Navigation;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursova.ViewModels
{
    public class StatisticPageViewModel : BaseViewModel
    {
        private ActivityItemViewModel _selectedActivityItem;

        private string _selectedCharacteristicItem;

        private Chart _chart;

        public StatisticPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ActivityItems = new ObservableCollection<ActivityItemViewModel>();
            StatisticItems = new ObservableCollection<StatisticItemViewModel>();
            CharacteristicItems = new ObservableCollection<string>();

            ClosePageCommand = new DelegateCommand(OnClosePage);
        }

        public ObservableCollection<ActivityItemViewModel> ActivityItems { get; set; }

        public ObservableCollection<StatisticItemViewModel> StatisticItems { get; set; }

        public ObservableCollection<string> CharacteristicItems { get; set; }

        public DelegateCommand ClosePageCommand { get; }

        public ActivityItemViewModel SelectedActivityItem
        {
            get => _selectedActivityItem;
            set
            {
                _selectedActivityItem = value;
                UpdateChartValue();
                RaisePropertyChanged();
            }
        }

        public string SelectedCharacteristicItem
        {
            get => _selectedCharacteristicItem;
            set
            {
                _selectedCharacteristicItem = value;
                UpdateChartValue();
                RaisePropertyChanged();
            }
        }

        public Chart Chart
        {
            get => _chart;
            set
            {
                _chart = value;
                RaisePropertyChanged();
            }
        }

        private async void InitializeOptions()
        {
            CharacteristicItems.Clear();
            CharacteristicItems.Add("Result");

            ActivityItems.Clear();
            var activities = await ItemsRepository.GetActivityItemsAsync();
            if (activities != null)
                foreach (var activity in activities)
                    ActivityItems.Add(new ActivityItemViewModel(activity));

            StatisticItems.Clear();
            var statistics = await ItemsRepository.GetStatisticItemsAsync();
            if (statistics != null)
                foreach (var statistic in statistics)
                    StatisticItems.Add(new StatisticItemViewModel(statistic));

            UpdateChartValue();
        }

        private void UpdateChartValue()
        {
            List<Entry> entries = new List<Entry>();
            if (SelectedActivityItem != null && SelectedCharacteristicItem != null)
            {
                foreach (var statisticItem in StatisticItems)
                    if (statisticItem.ActivityId == SelectedActivityItem.Id)
                        entries.Add(new Entry(statisticItem.Result)
                        {
                            Label = statisticItem.Result.ToString(),
                            TextColor = SKColor.Parse("#00521c"),
                            Color = SKColor.Parse("#1eb33d")
                        });
                Chart = new LineChart() { Entries = entries, LabelTextSize = 50, PointSize = 25 };
            }
            else
            {
                Chart = new LineChart() { Entries = entries };
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
        }

        private async void OnClosePage()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
