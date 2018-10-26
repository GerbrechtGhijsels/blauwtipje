using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;

namespace BlauwtipjeApp.Droid.Helpers
{
    public class ImageUtils
    {
        public static byte[] DrawableToBytes(Context context, int drawableId)
        {
            var drawable = BitmapFactory.DecodeResource(context.Resources, drawableId);
            var ms = new MemoryStream();

            drawable.Compress(Bitmap.CompressFormat.Png, 0, ms);
            return ms.ToArray();
        }

        public static Stream DrawableToStream(Context context, int drawableId)
        {
            var drawable = BitmapFactory.DecodeResource(context.Resources, drawableId);
            var ms = new MemoryStream();

            drawable.Compress(Bitmap.CompressFormat.Png, 0, ms);
            return ms;
        }

        public static async Task<Bitmap> BytesToBitmap(byte[] imageBytes)
        {
            return await BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length);
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] content;
            using (var streamReader = new StreamReader(stream))
            {
                using (var memstream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memstream);
                    content = memstream.ToArray();
                }
            }
            return content;
        }

        public static Stream BitmapToStream(Bitmap bitmap)
        {
            Stream stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
            bitmap.Dispose();
            return stream;
        }

        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            return ((MemoryStream)BitmapToStream(bitmap)).ToArray();
        }
    }
}
