using System;
using Com.Telit.Terminalio;

namespace SmartLED.Droid
{
    public class TIOConnectionCallback : Java.Lang.Object, ITIOConnectionCallback
    {
        Action<bool> _statusAction;

        public TIOConnectionCallback(Action<bool> statusAction)
        {
            _statusAction = statusAction;
        }

        public TIOConnectionCallback()
        {

        }

        public void OnConnected(ITIOConnection p0)
        {
            _statusAction.Invoke(true);
        }

        public void OnConnectFailed(ITIOConnection p0, string p1)
        {
            _statusAction.Invoke(false);
        }

        public void OnDataReceived(ITIOConnection p0, byte[] p1)
        {
            
        }

        public void OnDataTransmitted(ITIOConnection p0, int p1, int p2)
        {
            
        }

        public void OnDisconnected(ITIOConnection p0, string p1)
        {
            
        }

        public void OnReadRemoteRssi(ITIOConnection p0, int p1, int p2)
        {
            
        }
    }
}
