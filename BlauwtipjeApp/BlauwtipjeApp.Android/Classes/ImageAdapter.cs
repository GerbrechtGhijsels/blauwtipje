using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using BlauwtipjeApp.Core.Models.Tree;

namespace BlauwtipjeApp.Droid.Classes
{
    public class ImageAdapter : PagerAdapter
    {
        private Context context;
        public List<Image> imageList;

        public ImageAdapter(Context context, List<Image> imageList)
        {
            this.imageList = imageList;
            this.context = context;
        }
        public override int Count
        {
            get
            {
                return imageList.Count;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == ((ImageView)objectValue);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String((position+1).ToString());
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            ImageView imageView = new ImageView(context);
            imageView.SetScaleType(ImageView.ScaleType.FitCenter);
            var bytes = imageList[position].Content;
            var bitmap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
            imageView.SetImageBitmap(bitmap);
            bitmap.Dispose();
            ((ViewPager)container).AddView(imageView, 0);
            return imageView;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object objectValue)
        {
            ((ViewPager)container).RemoveView((ImageView)objectValue);
        }
    }
}