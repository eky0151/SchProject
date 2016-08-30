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

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<PasswordBox>(Login);
            Messenger.Default.Register<Navigator>(this, SetNavigator);
        }

        private void SetNavigator(Navigator e)
        {
            _navigator = e;
            Messenger.Default.Unregister<Navigator>(this,SetNavigator);
        }
        private void Login(PasswordBox box)
        {
            using (TechSupportService1Client client = new TechSupportService1Client())
            {
                client.Open();
                var res = client.Login(UserName, box.Password);
                if (res.Valid)
                {
                    _navigator.Login();
                    Messenger.Default.Send<LoginResult>(res);
                }
            }
        }
    }
}
