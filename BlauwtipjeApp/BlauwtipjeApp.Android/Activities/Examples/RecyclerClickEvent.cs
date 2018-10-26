using System;
using Android.Views;

namespace BlauwtipjeApp.Droid.Helpers
{
    public class RecyclerClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}

