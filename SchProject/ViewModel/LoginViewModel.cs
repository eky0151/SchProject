using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SchProject.Resources.Layout;
using SchProject.TechSupportService;

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
            Messenger.Default.Register<UsernameValidationResult>(this,SetProfilePicture);
        }

        private void SetProfilePicture(UsernameValidationResult res)
        {
           this.ProfilePicture=new BitmapImage(new Uri("https://techsupportfiles.blob.core.windows.net/images/original/"+res.ProfilePicture, UriKind.RelativeOrAbsolute));
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
        private async void Login(PasswordBox box)
        {
            LoginEnabled = false;

            var task = Task.Factory.StartNew(() =>
            {
                using (TechSupportService1Client client = new TechSupportService1Client())
                {
                    client.Open();
                    var res = client.Login(UserName, box.Password);
                    return res;
                }
            });

            var result = await task;
            if (result.Valid)
            {
                _navigator.Login();
                Messenger.Default.Send<LoginResult>(result);
            }
            LoginEnabled = true;
        }
    }
}
