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
    [Register ("DeterminationViewCell")]
    partial class DeterminationViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnSelect { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView QuestionImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView WebviewContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnSelect != null) {
                BtnSelect.Dispose ();
                BtnSelect = null;
            }

            if (QuestionImage != null) {
                QuestionImage.Dispose ();
                QuestionImage = null;
            }

            if (WebviewContainer != null) {
                WebviewContainer.Dispose ();
                WebviewContainer = null;
            }
        }
    }
}