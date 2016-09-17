using Foundation;
using System;
using UIKit;
using CoreLocation;

namespace TechSupport
{
    public partial class MainController : UIViewController
    {
		CLLocationManager locMan = new CLLocationManager();
        public MainController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			locMan.RequestWhenInUseAuthorization();
			ScrollViewer.ContentSize = MainView.Frame.Size;
			Map.MapType = MapKit.MKMapType.SatelliteFlyover;
		}
    }
}