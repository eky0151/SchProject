using System;
using SidebarNavigation;
using UIKit;

namespace TechSupport
{
	public class RootViewController : UIViewController
	{
		private UIStoryboard _storyboard;


		// the sidebar controller for the app
		public SidebarNavigation.SidebarController SidebarController { get; private set; }

		// the storyboard
		public override UIStoryboard Storyboard
		{
			get
			{
				if (_storyboard == null)
					_storyboard = UIStoryboard.FromName("Main", null);
				return _storyboard;
			}
		}

		public RootViewController() : base(null, null)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var introController = (BaseController)Storyboard.InstantiateViewController("LoginViewModel");
			var menuController = (MenuController)Storyboard.InstantiateViewController("MenuController");

			// create a slideout navigation controller with the top navigation controller and the menu view controller

			SidebarController = new SidebarNavigation.SidebarController(this, introController, menuController);
			SidebarController.MenuLocation = MenuLocations.Left;
			SidebarController.MenuWidth = 250;
			SidebarController.ReopenOnRotate = false;
		}
	}


}

