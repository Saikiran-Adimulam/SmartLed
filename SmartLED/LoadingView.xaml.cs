using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartLED
{
    public partial class LoadingView : ContentView
    {
        public static readonly BindableProperty IndicatorColorProperty =
         BindableProperty.Create("IndicatorColor", typeof(Color), typeof(LoadingView), Color.Black);

        public Color IndicatorColor
        {
            get { return (Color)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }
        public LoadingView()
        {
            InitializeComponent();

            indicator.SetBinding(ActivityIndicator.ColorProperty, new Binding("IndicatorColor", source: this));
        }
    }
}
