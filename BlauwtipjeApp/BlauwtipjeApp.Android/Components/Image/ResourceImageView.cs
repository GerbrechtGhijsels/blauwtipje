using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using BlauwtipjeApp.Droid.Helpers;
using FFImageLoading;
using FFImageLoading.Views;
using FFImageLoading.Work;
using Uri = Android.Net.Uri;

namespace BlauwtipjeApp.Droid.Components.Image
{
    public class ResourceImageView : ImageViewAsync
    {
        private const int DEFAULT_IMAGE = Resource.Drawable.blauwtipjelogo;
        private const string LOADING_PLACEHOLDER = "placeholder.jpg";
        public bool IsImageSet { get; private set; }

        public void SetImageFromBytes(byte[] imageBytes = null, ImageTransformation transformation = null)
        {
            if (imageBytes == null)
            {
                SetImageToDefaultImage(transformation);
                return;
            }

            var stream = new MemoryStream(imageBytes);
            LoadImage(stream, transformation);
        }

        public void SetImageToDefaultImage(ImageTransformation transformation = null)
        {
            SetImageFromDrawable(DEFAULT_IMAGE, transformation);
        }

        public void SetImageFromDrawable(int drawableId = 0, ImageTransformation transformation = null)
        {
            if (drawableId == 0)
            {
                SetImageToDefaultImage(transformation);
                return;
            }

            LoadImage(ImageUtils.DrawableToStream(Context, drawableId), transformation);
        }

        public void SetImageFromUri(Uri uri = null, ImageTransformation transformation = null)
        {
            if (uri == null)
            {
                SetImageToDefaultImage(transformation);
                return;
            }

            var bitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(Context.ContentResolver, uri);
            LoadImage(ImageUtils.BitmapToStream(bitmap), transformation);
        }

        public void SetImageFromStream(Stream stream = null, ImageTransformation transformation = null)
        {
            if (stream == null)
            {
                SetImageToDefaultImage(transformation);
                return;
            }
            LoadImage(stream, transformation);
        }

        private async void LoadImage(Stream stream, ImageTransformation transformation)
        {
            this.SetImageBitmap(null);
            Task<Stream> GetStream(CancellationToken token) => Task.Factory.StartNew(() => stream, token);
            var loadingJob = ImageService.Instance.LoadStream(GetStream);
            ApplyTransformation(loadingJob, transformation);
            loadingJob.Success(() => IsImageSet = true);
            loadingJob.Error((exception) => throw exception);
            loadingJob.Finish((work) => Log.Info("BlauwtipjeApp", work.IsCancelled + "/" + work.IsCompleted));
            await loadingJob.IntoAsync(this);
        }

        private void ApplyTransformation(TaskParameter loadingJob, ImageTransformation transformation)
        {
            if (transformation == null) return;

            loadingJob.DownSample(transformation.Width, transformation.Height);

            if (!transformation.DontUseLoadingPlaceHolder)
                loadingJob.LoadingPlaceholder(LOADING_PLACEHOLDER, ImageSource.CompiledResource);

            if (transformation.Rounded != null)
                loadingJob.Transform(transformation.Rounded);
            if (transformation.Circle != null)
                loadingJob.Transform(transformation.Circle);
            if (transformation.ColorSpace != null)
                loadingJob.Transform(transformation.ColorSpace);
        }

        /// <inheritdoc>
        /// Using this method circumvents FFImageLoading from loading the image, which is not good.
        /// </inheritdoc>
        [Obsolete("Please use the other methods.")]
        public override void SetImageBitmap(Bitmap bm)
        {
            base.SetImageBitmap(bm);
        }

        public ResourceImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public ResourceImageView(Context context) : base(context)
        {
        }

        public ResourceImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public ResourceImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }
    }
}