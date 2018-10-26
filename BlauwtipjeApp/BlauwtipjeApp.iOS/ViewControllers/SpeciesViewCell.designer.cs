// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    [Register ("SpeciesViewCell")]
    partial class SpeciesViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView LblNormalName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView LblScientificName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView SpeciesImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LblNormalName != null) {
                LblNormalName.Dispose ();
                LblNormalName = null;
            }

            if (LblScientificName != null) {
                LblScientificName.Dispose ();
                LblScientificName = null;
            }

            if (SpeciesImage != null) {
                SpeciesImage.Dispose ();
                SpeciesImage = null;
            }
        }
    }
}