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
using GalaSoft.MvvmLight.CommandWpf;
using SchProject.Resources.Layout;
using SchProject.TechSupportServer;

namespace SchProject.ViewModel
{
    public class Navigator : ViewModelBase
    {
        private UserControl rootControl;

        public UserControl RootControl
        {
            get { return rootControl; }
            private set
            {
                rootControl = value;
                RaisePropertyChanged("RootControl");
            }
        }

        public Navigator()
        {
            RootControl=new Login();
        }

        public void Login()
        {
            RootControl = new Dashboard();
        }

        public void Logout()
        {
            RootControl = new Login();
        }
    }
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; private set; }
        public string UserName { get; set; }
        public string PassWd { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(() => { Login(); });
        }

        private void Login()
        {
            TechSupportService1Client client = new TechSupportService1Client();
            var res = client.Login(UserName, PassWd);
            if (res.Valid)
            {
                ViewModelBase.Navigation.Login();
            }
        }
    }
}
