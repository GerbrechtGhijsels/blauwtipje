using System;
using System.Net;
using BlauwtipjeApp.Core.Interfaces;
using CoreFoundation;
using SystemConfiguration;

namespace BlauwtipjeApp.iOS.Helpers
{
    public class NetworkHelper : INetworkHelper
    {
        //Code from https://github.com/xamarin/ios-samples/tree/master/ReachabilitySample

        public NetworkHelper()
        {
        }






        //
        // Raised every time there is an interesting reachable event,
        // we do not even pass the info as to what changed, and
        // we lump all three status we probe into one
        //
        public static event EventHandler ReachabilityChanged;

        static void OnChange(NetworkReachabilityFlags flags)
        {
            ReachabilityChanged?.Invoke(null, EventArgs.Empty);
        }



        static NetworkReachability defaultRouteReachability;

        static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (defaultRouteReachability == null)
            {
                var ipAddress = new IPAddress(0);
                defaultRouteReachability = new NetworkReachability(ipAddress.MapToIPv6());
                defaultRouteReachability.SetNotification(OnChange);
                defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            return defaultRouteReachability.TryGetFlags(out flags);
        }

        static NetworkReachability remoteHostReachability;



        public static NetworkStatus InternetConnectionStatus()
        {
            NetworkReachabilityFlags flags;
            bool defaultNetworkAvailable = IsNetworkAvailable(out flags);

            if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
                return NetworkStatus.NotReachable;

            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                return NetworkStatus.ReachableViaCarrierDataNetwork;

            if (flags == 0)
                return NetworkStatus.NotReachable;

            return NetworkStatus.ReachableViaWiFiNetwork;
        }




        public bool HasInternet()
        {
            //NetworkReachabilityFlags flags;
            return  InternetConnectionStatus()== NetworkStatus.NotReachable;

        }
    }
    public enum NetworkStatus
    {
        NotReachable,
        ReachableViaCarrierDataNetwork,
        ReachableViaWiFiNetwork
    }
}
