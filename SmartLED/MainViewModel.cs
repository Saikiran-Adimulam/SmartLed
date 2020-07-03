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

        public MainViewModel()
        {
            telitService = DependencyService.Resolve<ITelitService>();
            ScanCommand = new Command(OnScanTapped);
            _onDevicesFound = HandleDeviceFound;
        }

        public void OnNavigatedTo()
        {
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

        private void HandleDeviceFound(TelitDevice device)
        {
            if(!DeviceList.Contains(device))
            {
                DeviceList.Add(device);
            }
        }
    }
}
