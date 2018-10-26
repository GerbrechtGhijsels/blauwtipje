using FFImageLoading.Transformations;

namespace BlauwtipjeApp.Droid.Components.Image
{
    /// <summary>
    /// An object that is used to  contain configurations for image transformations.
    /// More transformations can be found here: 
    /// https://github.com/luberda-molinet/FFImageLoading/wiki/Transformations-Guide
    /// </summary>
    public class ImageTransformation
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool DontUseLoadingPlaceHolder { get; set; }
        public RoundedTransformation Rounded { get; set; }
        public CircleTransformation Circle { get; set; }
        public ColorSpaceTransformation ColorSpace { get; set; }
    }
}