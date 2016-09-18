using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using TechSupportService.DataContract;

namespace TechSupport
{
	public partial class MessagesController : UIViewController
	{
		List<Message> messages = new List<Message>() { new Message() { Sender = "Tron", Messag = "cnasdijcnasdijnoasdicnasodcnasduincaosducn" } };
		public MessagesController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			EmailView.Source = new MessagesDataSource(this, messages);
			TechSupportServer.Client.GetMyMessagesAsync(UserData.Username);
			TechSupportServer.Client.GetMyMessagesCompleted += SetMessages;
		}

		void SetMessages(object sender, GetMyMessagesCompletedEventArgs e)
		{
			InvokeOnMainThread(() =>
			{
				foreach (var item in e.Result)
				{
					messages.Add(item);
				}
				EmailView.Source = new MessagesDataSource(this, messages);
				EmailView.ReloadData();
			});
		}
	}
}