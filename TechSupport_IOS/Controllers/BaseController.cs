using System;
using UIKit;

namespace TechSupport
{
	public class BaseController : UIViewController
	{
		protected SidebarNavigation.SidebarController SidebarController
		{
			get
			{
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.SidebarController;
			}
		} 
		public override UIStoryboard Storyboard
		{
			get
			{
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.Storyboard;
			}
		}

		public BaseController(IntPtr handle) : base(handle)
		{
		}
	}
}
