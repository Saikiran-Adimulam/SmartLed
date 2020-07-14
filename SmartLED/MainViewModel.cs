using System;
using System.Collections.ObjectModel;
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
            //DeviceList.Add(new TelitDevice { Name = "LAKA1", Address = "CD:EE:FF:GG" });
            //DeviceList.Add(new TelitDevice { Name = "LAKA2", Address = "KJ:EE:FF:GG" });

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
            });
        }

        private void OnDeviceSelected(TelitDevice device)
        {
            Application.Current.MainPage.DisplayAlert("", "Coming soon...", "Ok");
        }

        private void HandleDeviceFound(TelitDevice device)
        {
            if(!DeviceList.Contains(device))
            {
                DeviceList.Add(device);
            }
        }
    }
}
