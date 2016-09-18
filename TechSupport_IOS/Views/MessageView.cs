using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace TechSupport
{
    public partial class MessageView : UIView
    {
        public MessageView (IntPtr handle) : base (handle)
        {
        }
		public override void Draw(CoreGraphics.CGRect rect)
		{
			var color = UIColor.FromRGBA(0.202f, 0.456f, 0.762f, 1.000f);

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new CGRect(0.0f, -0.0f, 768.0f, 64.0f));
			color.SetFill();
			rectanglePath.Fill();

		}
    }
}