using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using SchProject.Resources;
using SchProject.TechSupportSecure;

namespace SchProject
{
    public class UserData : ViewModelBase
    {
        private string _profilePicture;
        private string _fullName;
        public Role Role { get; private set; }
        public void SetData(LoginResult login,string username)
        {
            AzureServiceBus bus = SimpleIoc.Default.GetInstance<AzureServiceBus>();
            bus.MessagesInit(username);
            FullName = login.FullName;
            Role = login.Role;
            if (Role == Role.Admin || Role == Role.Boss)
                bus.StatusHandler += SimpleIoc.Default.GetInstance<Notifications>().ShowStatusUpdateAsync;
            bus.MessageHandler += SimpleIoc.Default.GetInstance<Notifications>().ShowMessageAsync;
        }


        public string FullName
        {
            get { return _fullName; }
            private set { Set(ref _fullName, value); }
        }

        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { Set(ref _profilePicture, value); }
        }
    }
}
