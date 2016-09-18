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
			UserName.Text = Sender;
			Response.Frame = frameRect;
			SenderMessage.Text = Message;
			UserName.TextColor = UIColor.White;
			LableSeender.TextColor = UIColor.White;
			frameRect.Size = new CGSize() { Height = 222, Width = frameRect.Size.Width };
		}

		partial void SendClick(UIButton sender)
		{
			TechSupportServer.Client.SendMessageToSupportAsync(Sender,UserData.Username, Response.Text);
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