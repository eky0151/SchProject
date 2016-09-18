using System;
using TechSupportService.DataContract;

namespace TechSupport
{
	public static class UserData
	{
		public static string FullName { get; set; }
		public static string Username { get; set; }
		public static string Picture { get; set; }
		public static Role Role { get; set; }

		public static void SetData(string fullname, string username, string picture, Role role)
		{
			FullName = fullname;
			Username = username;
			Picture = picture;
			Role = role;
		}
	}
}
