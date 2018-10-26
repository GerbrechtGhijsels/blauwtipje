using System;
using BlauwtipjeApp.Core.Models.Results;
using Foundation;
using UIKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    public partial class SpeciesViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("SpeciesViewCell");
        public static readonly UINib Nib;

        static SpeciesViewCell()
        {
            Nib = UINib.FromName("SpeciesViewCell", NSBundle.MainBundle);
        }

        protected SpeciesViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public SpeciesViewCell(){
            
        }
        public void PopulateCell(Slug slugObj){
            LblNormalName.Text = slugObj.DisplayName;
            LblScientificName.Text = slugObj.ScientificName;
            var decoder = new WebP.Touch.WebPCodec();
            var image = decoder.Decode(slugObj.ImageList[0].Content);
            //NSData data = NSData.FromArray(slugObj.ImageList[0].Content);
            //UIImage image = UIImage.LoadFromData(data);
            SpeciesImage.Image = image;
        }

    }
}
