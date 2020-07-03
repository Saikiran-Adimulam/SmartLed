using System;
namespace SmartLED.TelitManager
{
    public interface ITelitService
    {
        void Initialize();
        void StartScan(Action<TelitDevice> OnDevicesFound);
    }
}
