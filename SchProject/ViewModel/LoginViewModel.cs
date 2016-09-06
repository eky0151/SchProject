using System;
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
        private Navigator _navigator;
        private ImageSource _profilePicture;
        public ICommand LoginCommand { get; private set; }
        public string UserName { get; set; }
        private bool _loginEnabled;

        public LoginViewModel()
        {
            LoginEnabled = true;
            LoginCommand = new RelayCommand<PasswordBox>(Login);
            Messenger.Default.Register<Navigator>(this, SetNavigator);
            Messenger.Default.Register<string>(this,SetProfilePicture);
        }

        private void SetProfilePicture(string res)
        {
           this.ProfilePicture=new BitmapImage(new Uri("https://techsupportfiles.blob.core.windows.net/images/original/"+res, UriKind.RelativeOrAbsolute));
        }

        public ImageSource ProfilePicture
        {
            get { return _profilePicture; }
            set { Set(ref _profilePicture, value); }
        }

        public bool LoginEnabled
        {
            get { return _loginEnabled; }
            set { Set(ref _loginEnabled, value); }
        }

        private void SetNavigator(Navigator e)
        {
            _navigator = e;
            Messenger.Default.Unregister<Navigator>(this, SetNavigator);
        }
        private void Login(PasswordBox box)
        {
            LoginResult res;
            using (TechSupportServiceSecure1Client client=new TechSupportServiceSecure1Client())
            {
                client.ClientCredentials.UserName.UserName = "Flynn";
                client.ClientCredentials.UserName.Password = "sam";
                client.Open();
                res = client.GetWorkerData();
                ServiceLocator.Current.GetInstance<UserData>().SetData(res);
                _navigator.Login();
            }
        }
    }
}
