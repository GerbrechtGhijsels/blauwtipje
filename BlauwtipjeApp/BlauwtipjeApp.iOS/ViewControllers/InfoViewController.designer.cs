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
    [Register ("InfoViewController")]
    partial class InfoViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView WebviewContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (WebviewContainer != null) {
                WebviewContainer.Dispose ();
                WebviewContainer = null;
            }
        }
    }
}