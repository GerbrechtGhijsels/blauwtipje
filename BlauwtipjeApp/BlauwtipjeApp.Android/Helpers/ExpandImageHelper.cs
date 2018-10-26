using Android.Views;
using Android.Widget;
using Android.Views.Animations;

namespace BlauwtipjeApp.Droid.Helpers
{
    public class ExpandImageHelper
    {   
        private ImageView expandedImageView;
        private Animation zoomInAnimation;
        private Animation zoomOutAnimation;

        public ExpandImageHelper(ImageView expandedImageView)
        {
            this.expandedImageView = expandedImageView;
            this.expandedImageView.Click += (sender, e) =>
            {
                ZoomImageOut();
            };
        }

        public async void SetPicture(byte[] picture)
        {
            expandedImageView.SetImageBitmap(await ImageUtils.BytesToBitmap(picture));
        }

        public void ZoomImageIn()
        {
            expandedImageView.Visibility = ViewStates.Visible;
            expandedImageView.StartAnimation(zoomInAnimation);
        }

        public void ZoomImageOut()
        {
            expandedImageView.StartAnimation(zoomOutAnimation);
        }

        public bool IsVisible()
        {
            return expandedImageView.Visibility == ViewStates.Visible;
        }

        public void SetZoomInAnimation(Animation zoomIn)
        {
            this.zoomInAnimation = zoomIn;
        }

        public void SetZoomOutAnimation(Animation zoomOut)
        {
            this.zoomOutAnimation = zoomOut;
            this.zoomOutAnimation.AnimationEnd += (o, i) =>
            {
                expandedImageView.Visibility = ViewStates.Gone;
            };
        }

       
    }
}
