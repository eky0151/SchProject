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
    [Register ("DetailsViewController")]
    partial class DetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField Response { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView SenderMessage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserName { get; set; }

        [Action ("OnDismiss:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnDismiss (UIKit.UIButton sender);

        [Action ("SendClick:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SendClick (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (Response != null) {
                Response.Dispose ();
                Response = null;
            }

            if (SenderMessage != null) {
                SenderMessage.Dispose ();
                SenderMessage = null;
            }

            if (UserName != null) {
                UserName.Dispose ();
                UserName = null;
            }
        }
    }
}