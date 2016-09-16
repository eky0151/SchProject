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
        public void SetData(LoginResult login)
        {
            FullName = login.FullName;
            Role = login.Role;
            SimpleIoc.Default.GetInstance<AzureServiceBus>().StatusHandler += SimpleIoc.Default.GetInstance<Notifications>().ShowStatusUpdateAsync;
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
