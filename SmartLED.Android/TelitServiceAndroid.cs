using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Com.Telit.Terminalio;
using SmartLED.Droid;
using SmartLED.TelitManager;
using Xamarin.Forms;

[assembly: Dependency(typeof(TelitServiceAndroid))]
namespace SmartLED.Droid
{
    public class TelitServiceAndroid : ITelitService
    {
        public static List<ITIOPeripheral> LightPeripherals { get; set; } = new List<ITIOPeripheral>();

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

        public void StopScan()
        {
            TIOManager.Instance.StopScan();
        }

        public async Task<bool> Connect(TelitDevice device)
        {
            bool isSuccess = false;
            try
            {
                var light = LightPeripherals?.FirstOrDefault(x => x.Name == device.Name
                && x.Address == device.Address);
                if (light != null)
                {
                    CancellationTokenSource tokenSource = new CancellationTokenSource();
                    Action<bool> status = (isConnected) =>
                    {
                        isSuccess = isConnected;
                        tokenSource.Cancel();
                    };
                    TIOConnectionCallback callback = new TIOConnectionCallback(status);
                    var connection = light.Connect(callback);
                    await Task.Delay(20000, tokenSource.Token);
                }
            }
            catch { }

            return isSuccess;
        }

        //[Register("STATE_CONNECTED")]
        //public const int StateConnected = 2;

        //[Register("STATE_CONNECTING")]
        //public const int StateConnecting = 1;

        //[Register("STATE_DISCONNECTED")]
        //public const int StateDisconnected = 0;

        //[Register("STATE_DISCONNECTING")]
        //public const int StateDisconnecting = 3;

        public bool Disconnect(TelitDevice device)
        {
            bool isSuccess = false;
            try
            {
                var light = LightPeripherals?.FirstOrDefault(x => x.Name == device.Name
                && x.Address == device.Address);
                if (light != null)
                {
                    var connection = light.Connection;
                    if(connection!= null)
                    {
                        connection.Disconnect();
                        //connection.Dispose();
                    }
                }
            }
            catch { }

            return isSuccess;
        }

        public void SendData(byte[] buffer, TelitDevice device)
        {
            try
            {
                var light = LightPeripherals?.FirstOrDefault(x => x.Name == device.Name
                && x.Address == device.Address);
                if (light != null)
                {
                    var connection = light.Connection;
                    if (connection != null && buffer != null && buffer.Length > 0)
                    {
                        connection.Transmit(buffer);
                        //connection.Dispose();
                    }
                }
            }
            catch { }
        }
    }
}
