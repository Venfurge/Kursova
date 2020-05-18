using Kursova.Models;
using Prism.Mvvm;
using System;

namespace Kursova.ViewModels.ItemsViewModels
{
    public class StatisticItemViewModel : BindableBase
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public ActivityItem Activity { get; set; }
        public StatisticItemViewModel(StatisticItem statistic)
        {
            Id = statistic.Id;
            ActivityId = statistic.ActivityId;
            Result = statistic.Result;
            Date = statistic.Date;
            Time = statistic.Time;
        }

        private int _result;
        public int Result
        {
            get => _result;
            set
            {
                _result = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
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
    }
}
