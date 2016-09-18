using System;
using CoreLocation;
using MapKit;

namespace TechSupport
{
	public class CustomerAnnotation : MKPointAnnotation
	{
		private string title;
		private string subtitle;
		private CLLocationCoordinate2D coordinate;

		public string Address { get { return subtitle;} }
		public override string Title { get { return title; } }
		public override string Subtitle { get { return subtitle; } }
		public override CLLocationCoordinate2D Coordinate { get { return coordinate; } }


		public CustomerAnnotation(string name, string address, CLLocationCoordinate2D coord)
		{
			title = name;
			subtitle = address;
			coordinate = coord;
		}


	}
}
