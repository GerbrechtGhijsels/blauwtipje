using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace BlauwtipjeApp.Droid.Components.NavigationDrawer
{
    public class NavigationAdapter : ArrayAdapter<NavigationItem>
    {
        public NavigationAdapter(Context context, List<NavigationItem> items) : base(context, 0, items)
        {

        }

        public override long GetItemId(int position)
        {
            return (long) GetItem(position).ScreenId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(Context).Inflate(Resource.Layout.item_navigation, parent, false);

            var drawerText = view.FindViewById<TextView>(Resource.Id.drawerListText);
            drawerText.Text = GetItem(position).MenuName;

            var drawerImage = view.FindViewById<ImageView>(Resource.Id.drawerListImage);
            drawerImage.SetImageResource(GetItem(position).Icon);

            return view;
        }
    }
}