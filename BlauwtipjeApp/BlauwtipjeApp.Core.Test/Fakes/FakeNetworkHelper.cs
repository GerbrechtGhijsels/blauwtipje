using BlauwtipjeApp.Core.Interfaces;

namespace BlauwtipjeApp.Core.Test.Fakes
{
    public class FakeNetworkHelper : INetworkHelper
    {
        public bool InternetIsAvailable { get; set; }

        public bool HasInternet()
        {
            return InternetIsAvailable;
        }
    }
}
