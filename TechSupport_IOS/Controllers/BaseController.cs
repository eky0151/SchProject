using System;
using UIKit;

namespace TechSupport
{
	public class BaseController : UIViewController
	{
		
		public BaseController(IntPtr handle) : base(handle)
		{
		}
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
    }
}
