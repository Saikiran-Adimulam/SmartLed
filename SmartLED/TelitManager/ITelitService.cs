using System;
using System.Threading.Tasks;

namespace SmartLED.TelitManager
{
    public interface ITelitService
    {
        void Initialize();
        void StartScan(Action<TelitDevice> OnDevicesFound);
        void StopScan();
        Task<bool> Connect(TelitDevice device);
        bool Disconnect(TelitDevice device);
        void SendData(byte[] buffer, TelitDevice device);
    }
}
