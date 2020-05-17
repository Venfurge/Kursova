﻿using Kursova.Data;
using Kursova.Models;
using Kursova.ViewModels.ItemsViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;

namespace Kursova.ViewModels
{
    public class ActivityCompletingPopupPageViewModel : BaseViewModel
    {
        private StatisticItem _statisticItem;

        private IItemsRepository _itemsRepository;

        public ActivityCompletingPopupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Activities = new List<ActivityItemViewModel>();
            StatisticItems = new List<StatisticItem>();

            NextOrEndCommand = new DelegateCommand(OnNextOrEnd);
        }

        public List<ActivityItemViewModel> Activities { get; set; }
        public List<StatisticItem> StatisticItems { get; set; }
        public DelegateCommand NextOrEndCommand { get; }
        public int Counter { get; set; }


        private async void InitializeItems()
        {
            var statisticItems = await _itemsRepository.GetStatisticItemsAsync();
            if (statisticItems != null)
                foreach (var item in statisticItems)
                    StatisticItems.Add(item);

            StatisticItems.Clear();

            Counter = 0;
            var temp = await _itemsRepository.GetActivityItemsAsync();
            foreach (var activity in temp)
                if (activity.IsChecked)
                    Activities.Add(new ActivityItemViewModel(activity));
            UpdatePopupPageData();
        }

        private void UpdatePopupPageData()
        {
            Name = Activities[Counter].Name;
            Text = Activities[Counter].Text;
            MaxValue = Activities[Counter].MaxResult.ToString();
            SliderValue = 0;
            ActivitiesCounter = (Counter + 1).ToString() + "/" + Activities.Count.ToString();
            if (Counter < Activities.Count - 1)
                ButtonText = "Next";
            else
                ButtonText = "End";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _itemsRepository = parameters.GetValue<IItemsRepository>("repository");
            InitializeItems();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("repository", _itemsRepository);
        }

        private async void OnNextOrEnd()
        {
            _statisticItem = new StatisticItem()
            {
                ActivityId = Activities[Counter].Id,
                Result = SliderValue
            };
            StatisticItems.Add(_statisticItem);

            if (Counter < Activities.Count - 1)
            {
                ++Counter;
                UpdatePopupPageData();
            }
            else
            {
                await _itemsRepository.AddStatisticItemsAsync(StatisticItems);
                //if(StatisticItems.Count > 0)
                //    foreach (var statisticItem in StatisticItems)
                //        await _itemsRepository.AddStatisticItemAsync(statisticItem);
                await NavigationService.GoBackAsync();
            }
        }

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

        private string _activitiesCounter;
        public string ActivitiesCounter
        {
            get => _activitiesCounter;
            set
            {
                _activitiesCounter = value;
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

        private string _resultValue;
        public string ResultValue
        {
            get => _resultValue;
            set
            {
                _resultValue = value;
                RaisePropertyChanged();
            }
        }

        private string _maxValue;
        public string MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
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
                ResultValue = value.ToString() + "/" + Activities[Counter].MaxResult.ToString();
                RaisePropertyChanged();
            }
        }

        private string _buttonText;
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                RaisePropertyChanged();
            }
        }
    }
}