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
    [Register ("UpdateViewController")]
    partial class UpdateViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DownloadText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView LogText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UpdateNo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UpdateText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton UpdateYes { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DownloadText != null) {
                DownloadText.Dispose ();
                DownloadText = null;
            }

            if (LogText != null) {
                LogText.Dispose ();
                LogText = null;
            }

            if (UpdateNo != null) {
                UpdateNo.Dispose ();
                UpdateNo = null;
            }

            if (UpdateText != null) {
                UpdateText.Dispose ();
                UpdateText = null;
            }

            if (UpdateYes != null) {
                UpdateYes.Dispose ();
                UpdateYes = null;
            }
        }
    }
}