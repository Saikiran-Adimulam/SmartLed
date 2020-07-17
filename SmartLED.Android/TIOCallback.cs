using System;
using System.Linq;
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

        public void OnPeripheralFound(ITIOPeripheral peripheral)
        {
            var lightExists = TelitServiceAndroid.LightPeripherals?
                .FirstOrDefault(x => x.Name == peripheral.Name &&
                x.Address == peripheral.Address);
            if (lightExists == null)
            {
                TelitServiceAndroid.LightPeripherals.Add(peripheral);
            }
            //Application.Current.MainPage.DisplayAlert("", "Device found", "Ok");
            TelitDevice device = new TelitDevice
            {
                Name = peripheral.Name,
                Address = peripheral.Address
            };
            _onDevicesFound?.Invoke(device);
        }

        public void OnPeripheralUpdate(ITIOPeripheral peripheral)
        {
            var lightExists = TelitServiceAndroid.LightPeripherals?
                .FirstOrDefault(x => x.Name == peripheral.Name &&
                x.Address == peripheral.Address);
            if(lightExists == null)
            {
                TelitServiceAndroid.LightPeripherals.Add(peripheral);
            }
            //Application.Current.MainPage.DisplayAlert("", "Device updated", "Ok");
            TelitDevice device = new TelitDevice
            {
                Name = peripheral.Name,
                Address = peripheral.Address
            };
            _onDevicesFound?.Invoke(device);
        }
    }
}
