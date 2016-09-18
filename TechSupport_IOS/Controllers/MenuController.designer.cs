// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace TechSupport
{
    [Register ("MenuController")]
    partial class MenuController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DashButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FullName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton MessagesButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ProfilePicture { get; set; }

        [Action ("DashboardClick:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DashboardClick (UIKit.UIButton sender);

        [Action ("MessagesClick:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void MessagesClick (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (DashButton != null) {
                DashButton.Dispose ();
                DashButton = null;
            }

            if (FullName != null) {
                FullName.Dispose ();
                FullName = null;
            }

            if (MessagesButton != null) {
                MessagesButton.Dispose ();
                MessagesButton = null;
            }

            if (ProfilePicture != null) {
                ProfilePicture.Dispose ();
                ProfilePicture = null;
            }
        }
    }
}