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


		string profilePicture;
		public LoginViewModel(IntPtr handle) : base(handle)
		{

		}

		partial void Exit(UITextField sender)
		{
			TechSupportServer.Client.GetProfilePictureAsync(Username.Text, new Object());
			TechSupportServer.Client.GetProfilePictureCompleted += (object sender2, GetProfilePictureCompletedEventArgs e) => InvokeOnMainThread(() =>
			{
				if (e.Result != null && !string.IsNullOrEmpty(e.Result))
				{
					profilePicture = e.Result;
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
			TechSupportServer.Client.TechnicianLoginAsync(Username.Text, Password.Text, new object());
			TechSupportServer.Client.TechnicianLoginCompleted += (sender2, e) => InvokeOnMainThread(() =>
			{
				if (e.Result.Valid)
				{
					UserData.SetData(e.Result.FullName, Username.Text, profilePicture, e.Result.Role);
					var menu = (MenuController)SidebarController.MenuAreaController;
					menu.SetUserData();
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