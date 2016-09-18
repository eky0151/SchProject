using Foundation;
using System;
using UIKit;
using CoreLocation;
using MapKit;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace TechSupport
{
	public partial class MainController : UIViewController
	{

		public static CLLocationManager LocMan = LocationHelper.UpdateLocation();
		public MainController(IntPtr handle) : base(handle)
		{
		}
		public override void ViewDidLoad()
		{
			ScrollViewer.ContentSize = MainView.Frame.Size;
			WelcomeMessage.TextColor = UIColor.White;
			WelcomeMessage.TintColor = UIColor.White;
			WelcomeMessage.Layer.BorderWidth = 2.0f;
			WelcomeMessage.Layer.BorderColor = UIColor.White.CGColor;
			int time = DateTime.Now.Hour;
			if (21 <= time && time <= 5)
				WelcomeMessage.Text = "Jó estét!";
			else
				WelcomeMessage.Text = "Jó napot!";
			LocMan.RequestWhenInUseAuthorization();
			InitMap();
			TechSupportServer.Client.GetMyNewTechworksAsync(UserData.Username);
			TechSupportServer.Client.GetMyNewTechworksCompleted += TechWorksListInit;
			base.ViewDidLoad();

		}

		void TechWorksListInit(object sender, GetMyNewTechworksCompletedEventArgs e)
		{
			var customeList = e.Result;
			foreach (var item in customeList)
			{
				AddCustomerLocation(item.CustomerName, item.Address);
			}
		}
		void AddCustomerLocation(string customerName, string address)
		{

			var customerLocation = GetLocationFromAddress(address);
			InvokeOnMainThread(() =>
			{
				var annot = new CustomerAnnotation(customerName, address, customerLocation);
				Map.AddAnnotation(annot);
			});
		}

		void InitMap()
		{
			Map.MapType = MapKit.MKMapType.HybridFlyover;
			if (LocMan.Location.Coordinate.IsValid())
				Map.Camera.CenterCoordinate = LocMan.Location.Coordinate;
			else
				Map.Camera.CenterCoordinate = new CLLocationCoordinate2D() { Latitude = 47.492398, Longitude = 19.051419 };
			Map.Camera.Altitude = 1000.0;
			Map.Camera.Pitch = 45.0f;
			Map.Camera.Heading = 180.0;
			Map.ShowsBuildings = true;
			Map.PitchEnabled = true;
			Map.ZoomEnabled = true;
			Map.ScrollEnabled = true;
			Map.Delegate = new CustomerMapDelegate();
			var annot = new CustomerAnnotation("Teszt Elek", "2095 sad asdas 5", new CLLocationCoordinate2D() { Latitude = 47.492506, Longitude = 19.052556 });
			Map.AddAnnotation(annot);
		}

		CLLocationCoordinate2D GetLocationFromAddress(string address)
		{
			try
			{
				string escaped = Uri.EscapeDataString(address);
				string url = "http://maps.google.com/maps/api/geocode/json?address=" + escaped + "&sensor=false";
				using (var http = new HttpClient())
				{
					var resp = http.GetStringAsync(url).Result;
					var data = JsonConvert.DeserializeObject<RootObject>(resp);
					if (data.status == "OK")
					{
						var location = data.results[0].geometry.location;
						return new CLLocationCoordinate2D() { Latitude = location.lat, Longitude = location.lng };
					}

				}

			}
			//sorry 
			catch (Exception)
			{

			}
			return new CLLocationCoordinate2D() { Latitude = 0, Longitude = 0 };

		}
	}
}