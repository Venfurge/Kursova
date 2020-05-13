using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursova.ViewModels
{
    public class ActivityCreationPopupPageViewModel : BaseViewModel
    {
        private int _sliderValue;

        public ActivityCreationPopupPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public int SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                RaisePropertyChanged();
            }
        }
    }
}
