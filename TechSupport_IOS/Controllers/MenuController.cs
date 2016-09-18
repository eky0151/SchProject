using Foundation;
using System;
using UIKit;
using SDWebImage;
using CoreAnimation;

namespace TechSupport
{
    public partial class MenuController : BaseController
    {

        public MenuController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			DashButton.Hidden = true;
			MessagesButton.Hidden = true;
			ProfilePicture.Image = UIImage.FromFile("user.png");
		}
		public void SetUserData()
		{
			DashButton.Hidden = false;
			MessagesButton.Hidden = false;
			FullName.Text = UserData.FullName;
			ProfilePicture.SetImage(
				new NSUrl("https://techsupportfiles.blob.core.windows.net/images/512/"+UserData.Picture));
			CALayer profileImageCircle = ProfilePicture.Layer;
			profileImageCircle.CornerRadius = ProfilePicture.Frame.Width / 2;
			profileImageCircle.MasksToBounds = true;
		}
		partial void MessagesClick(UIButton sender)
		{
			this.SidebarController.ChangeContentView(this.Storyboard.InstantiateViewController("Messages"));
		}

		partial void DashboardClick(UIButton sender)
		{
			this.SidebarController.ChangeContentView(this.Storyboard.InstantiateViewController("MainView"));
		}
    }
}