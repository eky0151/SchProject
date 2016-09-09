using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Practices.ServiceLocation;
using SchProject.Resources.Layout;
using SchProject.Resources.Layout.CustomControls;
using SchProject.TechSupportSecure;

namespace SchProject.ViewModel
{
    public class RootMenuViewModel : ViewModelBase
    {
        public UserData User { get; private set; }
        private List<MenuButtonData> _menuButtons;
        public UserControl CurrentView { get; private set; }
        public ICommand Navigation { get; private set; }
        public ICommand Logout { get; private set; }
        private Dictionary<string, UserControl> Views { get; } = new Dictionary<string, UserControl>();

        public RootMenuViewModel()
        {
            Views["Dashboard"] = new Dashboard();
            CurrentView = Views["Dashboard"];
            Navigation = new RelayCommand<object>(param => Navigate(param));
            Logout = new RelayCommand(NavLogout);
            User = ServiceLocator.Current.GetInstance<UserData>();
            MenuButtons = MenuButtonFactory.Create(User.Role);
        }

        public List<MenuButtonData> MenuButtons
        {
            get { return _menuButtons; }
            private set { Set(ref _menuButtons, value); }
        }

        private void NavLogout()
        {
            Views.Clear();
            ServiceLocator.Current.GetInstance<NavigatorSingleton>().Navigator.Logout();
        }
        private void Navigate(object o)
        {
            string d = (string)o;
            if (!Views.ContainsKey(d))
            {
                switch (d)
                {
                    case "Settings":
                        Views[d] = new Settings();
                        break;
                    case "Bugreport":
                        Views[d] = new Bugreport();
                        break;
                    case "Home":
                        Views[d] = new Dashboard();
                        break;
                    case "Admins":
                        Views[d] = new Admins();
                        break;
                    case "Management":
                        Views[d] = new Management();
                        break;
                    case "Error":
                        Views[d] = new Demonstration();
                        break;
                    //case "Reports":
                    //    Views[d] = new Demonstration();
                    //    break;
                    default:
                        Views[d] = new Dashboard(); break;
                }

            }
            CurrentView = Views[d];
            RaisePropertyChanged("CurrentView");
        }
    }
}
