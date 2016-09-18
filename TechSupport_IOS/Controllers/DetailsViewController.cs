using Foundation;
using System;
using UIKit;
using CoreGraphics;
using System.ComponentModel;

namespace TechSupport
{
	public partial class DetailsViewController : UIViewController
	{
		public string Sender { get; set; }
		public string Message { get; set; }

		public DetailsViewController(IntPtr handle) : base(handle)
		{
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			CGRect frameRect = Response.Frame;
			frameRect.Size = new CGSize() { Height = 222, Width = frameRect.Size.Width };
			Response.Frame = frameRect;
			UserName.Text = Sender;
			SenderMessage.Text = Message;
		}

		partial void SendClick(UIButton sender)
		{
			TechSupportServer.Client.SendMessageToSupportAsync(Sender, Response.Text);
			TechSupportServer.Client.SendMessageToSupportCompleted += CleanMessage;
		}

		void CleanMessage(object sender, AsyncCompletedEventArgs e)
		{
			InvokeOnMainThread(() => Response.Text = "");
		}


		partial void OnDismiss(UIButton sender)
		{
			DismissViewController(true, null);
		}
	}
}