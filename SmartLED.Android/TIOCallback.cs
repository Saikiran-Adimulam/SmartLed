using System;
using Com.Telit.Terminalio;
using SmartLED.TelitManager;
using Xamarin.Forms;

namespace SmartLED.Droid
{
    public class TIOCallback : Java.Lang.Object, ITIOManagerCallback
    {
        Action<TelitDevice> _onDevicesFound;

        public TIOCallback(Action<TelitDevice> onDevicesFound)
        {
            _onDevicesFound = onDevicesFound;
        }

        public void OnPeripheralFound(ITIOPeripheral p0)
        {
            Application.Current.MainPage.DisplayAlert("", "Device found", "Ok");
            TelitDevice device = new TelitDevice
            {
                Name = p0.Name,
                Address = p0.Address
            };
            _onDevicesFound?.Invoke(device);
        }

        public void OnPeripheralUpdate(ITIOPeripheral p0)
        {
            Application.Current.MainPage.DisplayAlert("", "Device updated", "Ok");
        }
    }
}
