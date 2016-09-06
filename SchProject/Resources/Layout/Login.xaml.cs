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
using SchProject.TechSupportSecure1;
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

        private void UserNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string username = ((TextBox)sender).Text;
            ValidateUsername(username);

        }

        private async void ValidateUsername(string username)
        {
            var task = Task.Factory.StartNew( () =>
           {
               using (TechSupportService1Client host = new TechSupportService1Client())
               {
                   host.Open();
                   var res = host.GetProfilePicture(username);
                   if (!String.IsNullOrEmpty(res))
                   {
                       Dispatcher.Invoke(() => { Messenger.Default.Send<string>(res); });

                   }
               }
           });

        }
    }
}
