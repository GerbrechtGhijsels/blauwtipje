
using Android.Net;
using BlauwtipjeApp.Core.Interfaces;

namespace BlauwtipjeApp.Droid.Helpers
{
    public class NetworkHelper : INetworkHelper
    {
        private ConnectivityManager connectivityManager;

        public NetworkHelper(ConnectivityManager connectivityManager)
        {
            this.connectivityManager = connectivityManager;
        }

        public bool HasInternet()
        {
            if (connectivityManager.ActiveNetworkInfo == null)
                return false;
            else
            {
                return connectivityManager.ActiveNetworkInfo.IsConnected;
            }
        }
    }
}