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
        public ICommand LoginCommand { get; private set; }
        public string UserName { get; set; }
        private bool _loginEnabled;
        public LoginViewModel()
        {
            LoginEnabled = true;
            LoginCommand = new RelayCommand<PasswordBox>(Login);
            Messenger.Default.Register<Navigator>(this, SetNavigator);
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

            //for the chatservice connect method we'll need a name, so we send the name, and we register for
            //this message in the ChatViewModel
            Messenger.Default.Send(new SendFullNameMessage { FullName = result.FullName , Image = null });
        }
    }
}
