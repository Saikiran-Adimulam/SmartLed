using System;
using System.Collections.Generic;
using SmartLED.TelitManager;
using Xamarin.Forms;

namespace SmartLED
{
    public partial class LightPage : ContentPage
    {
        LightViewModel _viewModel;
        TelitDevice _device;

        public LightPage(TelitDevice device)
        {
            InitializeComponent();
            _viewModel = new LightViewModel();
            BindingContext = _viewModel;
            _device = device;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnNavigatedTo(_device);
        }
    }
}
