using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using SchProject.TechSupportSecure;
using SchProject.TechSupportService;

namespace SchProject.Resources.Layout
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void UserNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string username = ((TextBox)sender).Text;
            await Task.Factory.StartNew(()=> { ValidateUsername(username); });

        }

        private void ValidateUsername(string username)
        {

            using (TechSupportService1Client host = new TechSupportService1Client())
            {
                host.Open();
                var res = host.GetProfilePicture(username);
                if (!String.IsNullOrEmpty(res))
                {
                    Dispatcher.Invoke(() => { ServiceLocator.Current.GetInstance<UserData>().ProfilePicture = res; });

                }
            }


        }
    }
}
