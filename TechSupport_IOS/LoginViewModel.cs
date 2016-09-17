using Foundation;
using System;
using UIKit;
using System.ServiceModel;
using SDWebImage;
using CoreAnimation;

namespace TechSupport
{
	public partial class LoginViewModel : BaseController
	{

		TechSupportService1Client client = new TechSupportService1Client(new BasicHttpBinding(BasicHttpSecurityMode.Transport), new EndpointAddress("https://techsupportserver.azurewebsites.net/TechSupportService.svc"));

		public LoginViewModel(IntPtr handle) : base(handle)
		{

		}

		partial void Exit(UITextField sender)
		{
			client.GetProfilePictureAsync(Username.Text, new Object());
			client.GetProfilePictureCompleted += (object sender2, GetProfilePictureCompletedEventArgs e) => InvokeOnMainThread(() => 
			{
				if (e.Result != null && !string.IsNullOrEmpty(e.Result))
				{
					ProfileImage.SetImage(
		   url: new NSUrl("https://techsupportfiles.blob.core.windows.net/images/512/" + e.Result)
	   );
					CALayer profileImageCircle = ProfileImage.Layer;
					profileImageCircle.CornerRadius = ProfileImage.Frame.Width / 2;
					profileImageCircle.MasksToBounds = true;
				}
			});
		}

		partial void LoginClick(UIButton sender)
		{
			client.TechnicianLoginAsync(Username.Text, Password.Text,new object());
			client.TechnicianLoginCompleted += (sender2, e) => InvokeOnMainThread(() => 
			{
				if (e.Result.Valid)
				{
					this.SidebarController.ChangeContentView(this.Storyboard.InstantiateViewController("MainView"));
				}
			});
		}

		public override void ViewDidLoad()
		{
			ProfileImage.Image = UIImage.FromFile("user.png");
			base.ViewDidLoad();
		}


	}
}