using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace TechSupport
{
    public partial class MenuView : UIView
    {
        public MenuView (IntPtr handle) : base (handle)
        {
        }
		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);
			//// Color Declarations
			var colord = UIColor.FromRGBA(0.255f, 0.098f, 0.698f, 1.000f);

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new CGRect(0.0f, 0.0f, 475.0f, 156.0f));
			colord.SetFill();
			rectanglePath.Fill();


			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new CGRect(73.0f, 20.0f, 115.0f, 115.0f));
			UIColor.White.SetStroke();
			ovalPath.LineWidth = 2.0f;
			ovalPath.Stroke();
			base.Draw(rect);
		}
    }
}