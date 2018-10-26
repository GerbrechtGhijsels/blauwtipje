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
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnDetermination { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnInfo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnSpeciesList { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnDetermination != null) {
                BtnDetermination.Dispose ();
                BtnDetermination = null;
            }

            if (BtnInfo != null) {
                BtnInfo.Dispose ();
                BtnInfo = null;
            }

            if (BtnSpeciesList != null) {
                BtnSpeciesList.Dispose ();
                BtnSpeciesList = null;
            }
        }
    }
}