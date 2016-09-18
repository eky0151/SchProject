using System;
using System.Collections.Generic;
using Foundation;
using TechSupportService.DataContract;
using UIKit;

namespace TechSupport
{
	public class MessagesDataSource: UITableViewSource
	{
		MessagesController owner;
		List<Message> messages;
		public MessagesDataSource(MessagesController owner,List<Message> messages)
		{
			this.owner = owner;
			this.messages = messages;
		}



		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = new UITableViewCell(UITableViewCellStyle.Subtitle, null);
			var item = messages[indexPath.Row];

			cell.TextLabel.Font = UIFont.FromName("Helvetica Light", 14);
			cell.DetailTextLabel.Font = UIFont.FromName("Helvetica Light", 12);
			cell.DetailTextLabel.TextColor = UIColor.LightGray;

			cell.TextLabel.Text = item.Sender;
			cell.DetailTextLabel.Text = item.Messag + "...";
			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return messages.Count;
		}
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var storyboard = UIStoryboard.FromName("Main", null);
			var detailViewController = (DetailsViewController)storyboard.InstantiateViewController("DetailsViewController");
			var messsage = messages[indexPath.Row];
			detailViewController.Message = messsage.Messag;
			detailViewController.Sender = messsage.Sender;
			owner.ShowDetailViewController(detailViewController, owner);
		}
	}
}
