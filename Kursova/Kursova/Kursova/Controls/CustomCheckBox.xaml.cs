using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kursova.Controls
{
    public partial class CustomCheckBox : ContentView
    {
        private static List<CustomCheckBox> _boxes { get; set; }

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CustomCheckBox), default(bool),
                BindingMode.TwoWay,
                propertyChanged: OnIsCheckedChanged);

        public CustomCheckBox()
        {
            InitializeComponent();
            if (_boxes == null)
                _boxes = new List<CustomCheckBox>();
            _boxes.Add(this);
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        private static void OnIsCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            foreach (CustomCheckBox box in _boxes)
                box.IsChecked = false;
            var isChecked = (CustomCheckBox)bindable;
            isChecked.OldCheckBox.IsChecked = isChecked.IsChecked == false ? true : false;
        }
    }
}