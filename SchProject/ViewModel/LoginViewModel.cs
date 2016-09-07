using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using SchProject.TechSupportSecure1;

namespace SchProject.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        
        public ICommand LoginCommand { get; private set; }
        public UserData User { get; private set; }
        public string UserName { get; set; }
        private bool _loginEnabled;
        private bool _valid;

        public LoginViewModel()
        {
            LoginEnabled = true;
            _valid = true;
            User = ServiceLocator.Current.GetInstance<UserData>();
            LoginCommand = new RelayCommand<PasswordBox>(Login);
        }

        public bool Valid
        {
            get { return _valid; }
            set { Set(ref _valid, value); }
        }

        public bool LoginEnabled
        {
            get { return _loginEnabled; }
            set { Set(ref _loginEnabled, value); }
        }

        //void is very bad, it should be Task
        private async void Login(PasswordBox box)
        {
            LoginEnabled = false;
            var server = ServiceLocator.Current.GetInstance<TechSupportServer>();
            bool res = await server.Login(this.UserName, box.Password);
            if (res)
            {
                ServiceLocator.Current.GetInstance<UserData>().SetData(server.host.GetWorkerData());
                ServiceLocator.Current.GetInstance<NavigatorSingleton>().Navigator.Login();
            }
            else
            {
                Valid = false;
            }
            LoginEnabled = true;
        }
    }
}
