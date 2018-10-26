using System;
using System.IO;
using Android.Graphics;

namespace BlauwtipjeApp.Droid.Helpers
{
    /// <summary>
    /// A helper class for loading images without causing OutOfMemory exceptions.
    /// Source: https://developer.android.com/topic/performance/graphics/load-bitmap.html
    /// </summary>
    public class ImageLoader
    {
        public static Bitmap DecodeSampledBitmapFromStream(Stream stream,
            int reqWidth, int reqHeight)
        {
            Bitmap DecodeFromStreamFunction(BitmapFactory.Options options)
            {
                var content = default(byte[]);
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (var memstream = new MemoryStream())
                    {
                        streamReader.BaseStream.CopyTo(memstream);
                        content = memstream.ToArray();
                    }
                }
                var bitmap = BitmapFactory.DecodeByteArray(content, 0, content.Length, options);
                return bitmap;
            }

            return DecodeSampledBitmap(DecodeFromStreamFunction, reqWidth, reqHeight);
        }

        /// <summary>
        /// Decode a sampled bitmap from a byte array.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <param name="reqWidth">The required width.</param>
        /// <param name="reqHeight">The required height.</param>
        /// <returns></returns>
        public static Bitmap DecodeSampledBitmapFromByteArray(byte[] bytes,
            int reqWidth, int reqHeight)
        {
            Bitmap DecodeFromByteArrayFunction(BitmapFactory.Options options) =>
                BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length, options);

            return DecodeSampledBitmap(DecodeFromByteArrayFunction, reqWidth, reqHeight);
        }

        /// <summary>
        /// Decode a sampled bitmap from a resource.
        /// </summary>
        /// <param name="res">The resource.</param>
        /// <param name="resId">The resource identifier.</param>
        /// <param name="reqWidth">The required width.</param>
        /// <param name="reqHeight">The required height.</param>
        /// <returns></returns>
        public static Bitmap DecodeSampledBitmapFromResource(Android.Content.Res.Resources res, int resId,
            int reqWidth, int reqHeight)
        {
            Bitmap DecodeFromResourceFunction(BitmapFactory.Options options) =>
                BitmapFactory.DecodeResource(res, resId, options);

            return DecodeSampledBitmap(DecodeFromResourceFunction, reqWidth, reqHeight);
        }

        private static Bitmap DecodeSampledBitmap(Func<BitmapFactory.Options, Bitmap> decodeFunction,
            int reqWidth, int reqHeight)
        {
            // First decode with InJustDecodeBounds=true to check dimensions
            var options = new BitmapFactory.Options {InJustDecodeBounds = true};
            decodeFunction(options);

            // Calculate InSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with InSampleSize set
            options.InJustDecodeBounds = false;
            return decodeFunction(options);
        }

        private static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            var height = options.OutHeight;
            var width = options.OutWidth;
            var inSampleSize = 1;

            if (height <= reqHeight && width <= reqWidth) return inSampleSize;

            var halfHeight = height / 2;
            var halfWidth = width / 2;

            // Calculate the largest InSampleSize value that is a power of 2 and keeps both
            // height and width larger than the requested height and width.
            while (halfHeight / inSampleSize >= reqHeight
                   && halfWidth / inSampleSize >= reqWidth)
            {
                inSampleSize *= 2;
            }

            return inSampleSize;
        }

    }
}