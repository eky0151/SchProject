using System;
using Foundation;
using MapKit;
using UIKit;

namespace TechSupport
{
	public class CustomerMapDelegate : MKMapViewDelegate
	{
		public Action MapViewChanged { get; set; }

		MKPolyline route;

		public CustomerMapDelegate()
		{
		}
		public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
		{
			var annotation = view.Annotation as CustomerAnnotation;

			if (annotation == null)
				return;

			var msg = annotation.Address;

			new UIAlertView(annotation.GetTitle(), msg, null, "OK", null).Show();
		}

		public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			if (overlay is MKPolyline)
			{
				var route = (MKPolyline)overlay;
				var renderer = new MKPolylineRenderer(route) { StrokeColor = UIColor.Red, LineWidth = 3.0f };
				return renderer;
			}
			return null;
		}

		public override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
		{
			var coord = ((CustomerAnnotation)view.Annotation).Coordinate;

			var destination = new MKMapItem(new MKPlacemark(coord, (MKPlacemarkAddress)null));
			ShowDirections(destination, mapView);
		}

		void ShowDirections(MKMapItem destination, MKMapView mapView)
		{

			MKMapItem source; source = new MKMapItem(new MKPlacemark(MainController.LocMan.Location.Coordinate, (MKPlacemarkAddress)null));

			var request = new MKDirectionsRequest()
			{
				Destination = destination,
				Source = source,
				RequestsAlternateRoutes = false,
			};

			var directions = new MKDirections(request);

			directions.CalculateDirections((MKDirectionsResponse response, NSError e) =>
			{

				if (this.route != null)
					mapView.RemoveOverlay(this.route);

				if (response == null || response.Routes.Length == 0)
					return;

				//save the overlay so we can remove it next time we draw
				route = response.Routes[0].Polyline;

				mapView.AddOverlay(route, MKOverlayLevel.AboveRoads);
			});
		}
	}

}
