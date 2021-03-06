﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartLED.TelitManager;
using Xamarin.Forms;

namespace SmartLED
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isBusy;
        private ITelitService telitService;
        private Action<TelitDevice> _onDevicesFound;
        private ObservableCollection<TelitDevice> _deviceList = new ObservableCollection<TelitDevice>();
        public ObservableCollection<TelitDevice> DeviceList
        {
            get => _deviceList;
            set => SetProperty(ref _deviceList, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand ScanCommand { get; private set; }
        public ICommand DeviceSelectedCommand { get; private set; }

        public MainViewModel()
        {
            telitService = DependencyService.Resolve<ITelitService>();
            ScanCommand = new Command(OnScanTapped);
            DeviceSelectedCommand = new Command<TelitDevice>(OnDeviceSelected);
            _onDevicesFound = HandleDeviceFound;
        }

        public void OnNavigatedTo()
        {
            //DeviceList.Add(new TelitDevice { Name = "LAKA1", Address = "AB:E9:DE:F3" });
            telitService.Initialize();
        }

        private void OnScanTapped()
        {
            telitService.StartScan(_onDevicesFound);
            IsBusy = true;
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                IsBusy = false;
                await Task.Delay(10000);
                telitService.StopScan();
            });
        }

        private async void OnDeviceSelected(TelitDevice device)
        {
            IsBusy = true;
            bool isConnected = await telitService.Connect(device);
            IsBusy = false;

            if(isConnected)
            {
                await App.Navigation.PushAsync(new LightPage(device));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Failed to connect", "Ok");
            }
        }

        private void HandleDeviceFound(TelitDevice device)
        {
            var deviceExists = DeviceList?.FirstOrDefault(x => x.Address == device.Address && x.Name == device.Name);
            if(deviceExists == null)
            {
                DeviceList.Add(device);
            }
        }
    }
}
