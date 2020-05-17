using Kursova.Models;
using Prism.Mvvm;

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
    }
}
