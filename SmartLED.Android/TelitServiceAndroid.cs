using System;
using Com.Telit.Terminalio;
using SmartLED.Droid;
using SmartLED.TelitManager;
using Xamarin.Forms;

[assembly: Dependency(typeof(TelitServiceAndroid))]
namespace SmartLED.Droid
{
    public class TelitServiceAndroid : ITelitService
    {
        public void Initialize()
        {
            TIOManager.Initialize(MainActivity.Instance);
        }

        public void StartScan(Action<TelitDevice> onDevicesFound)
        {
            if (TIOManager.Instance.IsBluetoothEnabled)
            {
                Application.Current.MainPage.DisplayAlert("", "Scan started", "Ok");
                TIOCallback callback = new TIOCallback(onDevicesFound);
                TIOManager.Instance.StartScan(callback);
            }
            else
            {
                App.Current.MainPage.DisplayAlert("", "Please turn on the bluetooth", "Ok");
            }
        }
    }
}
