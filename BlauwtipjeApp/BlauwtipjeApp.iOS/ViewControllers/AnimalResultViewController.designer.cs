// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace BlauwtipjeApp.iOS.ViewControllers
{
    [Register ("AnimalResultViewController")]
    partial class AnimalResultViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView LblDisplayName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView LblScientificName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView SpeciesImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView WebviewContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LblDisplayName != null) {
                LblDisplayName.Dispose ();
                LblDisplayName = null;
            }

            if (LblScientificName != null) {
                LblScientificName.Dispose ();
                LblScientificName = null;
            }

            if (SpeciesImage != null) {
                SpeciesImage.Dispose ();
                SpeciesImage = null;
            }

            if (WebviewContainer != null) {
                WebviewContainer.Dispose ();
                WebviewContainer = null;
            }
        }
    }
}