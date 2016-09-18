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
			var colord = UIColor.FromRGBA(0.255f, 0.098f, 0.698f, 1.000f);

			//// Rectangle Drawi
			var rectanglePath = UIBezierPath.FromRect(new CGRect(0.0f, 0.0f, 475.0f, 220.0f));
			colord.SetFill();
			rectanglePath.Fill();
		}
    }
}