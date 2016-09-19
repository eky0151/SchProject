using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopToast;
using GalaSoft.MvvmLight.Ioc;
using SchProject.TechSupportSecure;
using GalaSoft.MvvmLight.Messaging;
using System.Timers;

namespace SchProject.Resources.Layout
{
    /// <summary>
    /// Interaction logic for MainWindowAqua.xaml
    /// </summary>
    public partial class MainWindowAqua : Window
    {
        public MainWindowAqua()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            NotificationActivator.RegisterComType(typeof(NotificationActivator), OnActivated);
            NotificationHelper.RegisterComServer(typeof(NotificationActivator), Assembly.GetExecutingAssembly().Location);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            NotificationActivator.UnregisterComType();
        }
        private async void OnActivated(string arguments, Dictionary<string, string> data)
        {
            if ((arguments?.StartsWith("action=")).GetValueOrDefault())
            {
                var result = arguments.Substring("action=".Length);

                if (data.Count > 0 && result == "Replied")
                {
                    var res = data.FirstOrDefault();
                    await SimpleIoc.Default.GetInstance<TechSupportServer>()
                        .host.SendMessageToTechnicianAsync(res.Key, SimpleIoc.Default.GetInstance<UserData>().UserName, res.Value);
                }
            }
        }

        private void MainWindowAqua_OnClosing(object sender, CancelEventArgs e)
        {
            Messenger.Default.Send<string>("closing");
            var host = SimpleIoc.Default.GetInstance<TechSupportServer>().host;
            host?.ChangeMyStatus(Status.Away);
            if (SimpleIoc.Default.GetInstance<UserData>().Role == Role.Technician)
            {
                host?.ChangeTechnicianStatus(TechnicianStatus.Break);
            }
            SimpleIoc.Default.GetInstance<AzureServiceBus>().DeleteSubs();
        }
    }
}
